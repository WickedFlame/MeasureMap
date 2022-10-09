using System;
using System.Linq;
using MeasureMap.Tracers.Metrics;

namespace MeasureMap.Tracers
{
    /// <summary>
    /// Trace the Result of the Profilier and Benchmarker as MarkDown
    /// </summary>
    public class MarkDownTracer : ITracer
    {
        /// <summary>
        /// Trace the <see cref="IProfilerResult"/> as MarkDown to the ouput
        /// </summary>
        /// <param name="result"></param>
        /// <param name="writer"></param>
        /// <param name="options"></param>
        public void Trace(IProfilerResult result, IResultWriter writer, TraceOptions options)
        {
            options.Metrics ??= ProfilerTraceMetrics.GetDefaultTraceMetrics();

            writer.WriteLine(string.IsNullOrEmpty(options.Header) ? "# MeasureMap - Profiler result" : $"# {options.Header}");
            if (result == null || !result.Any())
            {
                writer.WriteLine("No measurements contained in the result");
                return;
            }

            writer.WriteLine("## Summary");

            writer.WriteLine("| Category | Metric            |            Value |");
            writer.WriteLine("| :------- | :---------------- | ---------------: |");

            foreach (var group in options.Metrics.GetProfilerMetrics().GroupBy(m => m.Category).OrderByDescending(g => g.Key == MetricCategory.Warmup).ThenByDescending(g => g.Key == MetricCategory.Setup).ThenByDescending(g => g.Key == MetricCategory.Duration).ThenBy(g => g.Key == MetricCategory.Memory))
            {
                var key = group.Key;
                foreach (var metric in group)
                {
                    writer.WriteLine($"| {key,-8} | {metric.Name,-17} | {metric.GetMetric(result),16} |");
                    key = string.Empty;
                }
            }

            if (options.TraceThreadDetail)
            {
                writer.WriteLine(string.Empty);
                writer.WriteLine("## Details per Thread");

                var metrics = options.Metrics.GetProfileThreadMetrics();

                //var headers = string.Join(" | ", metrics.Select(m => m.Name.Pad(m.TextAlign, 16)));
                var headers = string.Join(" | ", metrics.Select(m => m.Name.Pad(m.TextAlign, m.Name.TraceLength())));
                writer.WriteLine($"| {headers} |");

                writer.Write("|");

                foreach (var metric in metrics)
                {
                    // reverse the alignment
                    var align = metric.TextAlign == TextAlign.Right ? TextAlign.Left : TextAlign.Right;
                    //writer.Write($" {GetAlignment(metric.TextAlign).Pad(align, 16, '-')} |");
                    writer.Write($" {GetAlignment(metric.TextAlign).Pad(align, metric.Name.TraceLength(), '-')} |");
                }

                writer.WriteLine(string.Empty);

                foreach (var thread in result)
                {
                    writer.Write("|");

                    foreach (var metric in metrics)
                    {
                        //writer.Write($" {metric.GetMetric(thread).Pad(metric.TextAlign, 16)} |");
                        writer.Write($" {metric.GetMetric(thread).Pad(metric.TextAlign, metric.Name.TraceLength())} |");
                    }

                    writer.WriteLine(string.Empty);
                }
            }

            if (options.TraceFullStack)
            {
                writer.WriteLine(string.Empty);
                writer.WriteLine("## Details per Iteration and Thread");
                var metrics = options.Metrics.GetIterationMetrics();
                var headers = string.Join(" | ", metrics.Select(m => m.Name.Pad(m.TextAlign, m.Name.TraceLength())));
                writer.WriteLine($"| {headers} |");

                writer.Write("|");
                foreach (var metric in metrics)
                {
                    var align = metric.TextAlign == TextAlign.Right ? TextAlign.Left : TextAlign.Right;
                    writer.Write($" {GetAlignment(metric.TextAlign).Pad(align, metric.Name.TraceLength(), '-')} |");
                }

                writer.WriteLine(string.Empty);

                foreach (var iteration in result.Iterations.OrderBy(i => i.TimeStamp))
                {
                    writer.Write("|");

                    foreach (var metric in metrics)
                    {
                        writer.Write($" {metric.GetMetric(iteration).Pad(metric.TextAlign, metric.Name.TraceLength())} |");
                    }

                    writer.WriteLine(string.Empty);
                }
            }
        }

        /// <summary>
        /// Trace the <see cref="IBenchmarkResult"/> as MarkDown to the ouput
        /// </summary>
        /// <param name="result"></param>
        /// <param name="writer"></param>
        /// <param name="options"></param>
        public void Trace(IBenchmarkResult result, IResultWriter writer, TraceOptions options)
        {
            options.Metrics ??= BenchmarkTraceMetrics.GetDefaultTraceMetrics();

            writer.WriteLine(string.IsNullOrEmpty(options.Header) ? "# MeasureMap - Benchmark result" : $"# {options.Header}");
            if (result == null || !result.Any())
            {
                writer.WriteLine("No measurements contained in the result");
                return;
            }

            if(result.Iterations > 1)
            {
                writer.WriteLine($" Iterations: {result.Iterations}");
            }

            if (result.Duration > TimeSpan.Zero)
            {
                writer.WriteLine($" Duration: {result.Duration}");
            }

            writer.WriteLine("## Summary");
            
            var keylength = result.Keys.Select(k => k.Length).OrderByDescending(k => k).First();
            keylength = keylength < 4 ? 4 : keylength;

            var metrics = options.Metrics.GetProfilerMetrics();
            var headers = string.Join(" | ", metrics.Select(m => m.Name.Pad(m.TextAlign, m.Name.TraceLength())));

            var keyHeader = "Name".Pad(TextAlign.Left, keylength);
            writer.WriteLine($"| {keyHeader} | {headers} |");

            writer.Write($"| {GetAlignment(TextAlign.Left).Pad(TextAlign.Left, keylength, '-')} |");
            foreach (var metric in metrics)
            {
                var align = metric.TextAlign == TextAlign.Right ? TextAlign.Left : TextAlign.Right;
                writer.Write($" {GetAlignment(metric.TextAlign).Pad(align, metric.Name.TraceLength(), '-')} |");
            }

            writer.WriteLine(string.Empty);

            foreach (var key in result.Keys)
            {
                writer.Write($"| {key.Pad(TextAlign.Left, keylength)} |");

                var profile = result[key];
                foreach (var metric in metrics)
                {
                    writer.Write($" {metric.GetMetric(profile).Pad(metric.TextAlign, metric.Name.TraceLength())} |");
                }

                writer.WriteLine(string.Empty);
            }
        }

        private string GetAlignment(TextAlign align)
        {
            return align switch
            {
                TextAlign.Right => ":",
                TextAlign.Left => "",
                _ => ""
            };
        }
    }
}
