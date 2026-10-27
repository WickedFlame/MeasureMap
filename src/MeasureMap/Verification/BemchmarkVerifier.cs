using MeasureMap.Diagnostics;
using MeasureMap.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace MeasureMap.Verification
{
    /// <summary>
    /// 
    /// </summary>
    public class BemchmarkVerifier
    {
        /// <summary>
        /// Verify if the benchmark result is within the threshold of the baseline result.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="last"></param>
        /// <param name="fileName"></param>
        /// <param name="thresholdPercentage"></param>
        /// <param name="noiseFloorMilliseconds"></param>
        /// <param name="sourceFilePath"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Verify(IBenchmarkResult result, BenchmarkModel last, string fileName = "", double thresholdPercentage = 10, double noiseFloorMilliseconds = 1, [CallerFilePath] string sourceFilePath = "")
        {
            var report = new List<string>();
            var violations = new List<string>();

            // the baseline defines the expected order, starting with the fastest benchmark
            var baseline = last.Benchmarks.OrderBy(b => b.AverageMilliseconds).ToList();
            var items = result.ToBenchmarkModel();

            // the current run is arranged according to the baseline order
            var current = new List<Benchmark>();
            foreach (var previous in baseline)
            {
                var match = items.Benchmarks.FirstOrDefault(b => b.Name == previous.Name);
                if (match == null)
                {
                    report.Add($"{previous.Name}: missing in current run (baseline {previous.AverageMilliseconds:F4} ms)");
                    continue;
                }

                current.Add(match);
            }

            foreach (var added in items.Benchmarks.Where(b => baseline.All(p => p.Name != b.Name)))
            {
                report.Add($"{added.Name}: no baseline, {added.AverageMilliseconds:F4} ms (new)");
            }

            var ordered = baseline.Where(b => current.Any(c => c.Name == b.Name)).ToList();

            // measure each benchmark against its baseline: (100 / baseline) * benchmark
            var measurements = new List<(Benchmark Previous, Benchmark Current, double Percentage, bool WithinNoise)>();
            for (var i = 0; i < ordered.Count; i++)
            {
                var previous = ordered[i];
                var benchmark = current[i];

                if (previous.AverageMilliseconds <= 0)
                {
                    report.Add($"{previous.Name}: baseline is zero, skipped");
                    continue;
                }

                var percentage = 100 / previous.AverageMilliseconds * benchmark.AverageMilliseconds;
                var withinNoise = Math.Abs(benchmark.AverageMilliseconds - previous.AverageMilliseconds) < noiseFloorMilliseconds;
                measurements.Add((previous, benchmark, percentage, withinNoise));
            }

            var relevant = measurements.Where(m => !m.WithinNoise).Select(m => m.Percentage).OrderBy(p => p).ToList();
            if (relevant.Count < 3)
            {
                // a median of less than 3 values cannot identify a single outlier
                report.Add($"Not enough benchmarks to verify ({relevant.Count} outside of noise floor, at least 3 required)");

                var message = new StringBuilder();
                message.AppendLine($"Not enough benchmarks to verify ({relevant.Count} outside of noise floor, at least 3 required)");
                message.AppendLine();
                message.AppendLine("New measurement:");
                message.AppendLine(result.ToMarkdown());

                message.AppendLine();
                message.AppendLine("Baseline:");
                message.AppendLine(last.ToMarkdown());

                ConsoleWriter.WriteLine(message.ToString());
                return;
            }

            // the median percentage represents the overall speed factor of the current machine/run
            var median = relevant.Count % 2 == 1
                ? relevant[relevant.Count / 2]
                : (relevant[relevant.Count / 2 - 1] + relevant[relevant.Count / 2]) / 2;

            report.Add($"Median: {median:0.00}% of baseline");
            ConsoleWriter.WriteLine($"Median: {median:0.00}% of baseline");

            foreach (var (previous, benchmark, percentage, withinNoise) in measurements)
            {
                // relative to the median so the threshold means the same on fast and slow machines
                var difference = percentage / median * 100 - 100;

                var line = $"{previous.Name}: baseline {previous.AverageMilliseconds:F4} ms, current {benchmark.AverageMilliseconds:F4} ms ({percentage:0.00}% of baseline, difference to median {difference:+0.00;-0.00;0.00}%)";

                // ignore differences that are smaller than the measurement noise of the machine
                if (withinNoise)
                {
                    report.Add($"{line} [within noise floor]");
                    ConsoleWriter.WriteLine($"{line} [within noise floor]");
                    continue;
                }

                report.Add(line);

                // all benchmarks have to stay within +- threshold of the median percentage
                if (Math.Abs(difference) > thresholdPercentage)
                {
                    violations.Add(line);
                    ConsoleWriter.WriteLine($"{line} [exceeds {thresholdPercentage:F0}% threshold]");
                }
                else
                {
                    ConsoleWriter.WriteLine($"{line} [ok]");
                }
            }

            if (violations.Count > 0)
            {
                var message = new StringBuilder();
                message.AppendLine($"{violations.Count} benchmark(s) exceeded the allowed {thresholdPercentage:F0}% threshold.");

                foreach (var violation in violations)
                {
                    message.AppendLine(violation);
                }

                message.AppendLine();
                message.AppendLine("Details:");

                foreach (var line in report)
                {
                    message.AppendLine(line);
                }

                message.AppendLine();
                message.AppendLine("New measurement:");
                message.AppendLine(result.ToMarkdown());

                message.AppendLine();
                message.AppendLine("Baseline:");
                message.AppendLine(last.ToMarkdown());

                throw new InvalidOperationException(message.ToString());
            }

            return;
        }
    }
}
