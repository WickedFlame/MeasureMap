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
            if (ordered.Count < 2)
            {
                return;
            }

            // compare the relative distance between two consecutive benchmarks
            for (var i = 0; i < ordered.Count - 1; i++)
            {
                var previousFirst = ordered[i];
                var previousSecond = ordered[i + 1];
                var currentFirst = current[i];
                var currentSecond = current[i + 1];

                if (previousFirst.AverageMilliseconds <= 0 || currentFirst.AverageMilliseconds <= 0)
                {
                    report.Add($"{previousFirst.Name} -> {previousSecond.Name}: reference is zero, skipped");
                    continue;
                }

                var baselineDelta = previousSecond.AverageMilliseconds - previousFirst.AverageMilliseconds;
                var currentDelta = currentSecond.AverageMilliseconds - currentFirst.AverageMilliseconds;

                var baselinePercentage = baselineDelta / previousFirst.AverageMilliseconds * 100;
                var currentPercentage = currentDelta / currentFirst.AverageMilliseconds * 100;
                var difference = currentPercentage - baselinePercentage;

                var line = $"{previousFirst.Name} -> {previousSecond.Name}: baseline {baselinePercentage:+0.00;-0.00;0.00}%, current {currentPercentage:+0.00;-0.00;0.00}% (difference {difference:+0.00;-0.00;0.00}%)";

                // ignore differences that are smaller than the measurement noise of the machine
                if (Math.Abs(baselineDelta) < noiseFloorMilliseconds && Math.Abs(currentDelta) < noiseFloorMilliseconds)
                {
                    report.Add($"{line} [within noise floor]");
                    ConsoleWriter.WriteLine($"{line} [within noise floor]");
                    continue;
                }

                report.Add(line);

                // the relative distance has to stay within +- threshold of the baseline distance
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

                message.AppendLine("New measurement:");
                message.AppendLine(result.ToMarkdown());

                message.AppendLine();
                message.AppendLine("Original baseline:");
                message.AppendLine(last.ToMarkdown());

                throw new InvalidOperationException(message.ToString());
            }

            return;
        }
    }
}
