using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeasureMap.Serialization
{
    /// <summary>
    /// 
    /// </summary>
    public class BenchmarkModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int Iterations { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Benchmark> Benchmarks { get; set; } = [];

        /// <summary>
        /// Gets the Markdown representation of the Benchmark
        /// </summary>
        /// <returns></returns>
        public string ToMarkdown()
        {
            var sb = new StringBuilder();
            sb.AppendLine("# MeasureMap - Benchmark result");

            if (Benchmarks == null || Benchmarks.Count == 0)
            {
                sb.AppendLine("No measurements contained in the result");
                return sb.ToString();
            }

            if (Iterations > 1)
            {
                sb.AppendLine($" Iterations: {Iterations}");
            }

            if (Duration > TimeSpan.Zero)
            {
                sb.AppendLine($" Duration: {Duration}");
            }

            sb.AppendLine("## Summary");

            var keylength = Math.Max(4, Benchmarks.Max(b => (b.Name ?? string.Empty).Length));

            sb.AppendLine($"| {"Name".PadRight(keylength)} | {"Avg Time",-16} | {"Avg Milliseconds",16} | {"Fastest",-16} | {"Slowest",-16} | {"Iterations",10} |");
            sb.AppendLine($"| {new string('-', keylength)} | {new string('-', 16)} | {new string('-', 15)}: | {new string('-', 16)} | {new string('-', 16)} | {new string('-', 9)}: |");

            foreach (var b in Benchmarks)
            {
                sb.AppendLine($"| {(b.Name ?? string.Empty).PadRight(keylength)} | {b.AverageTime,-16} | {b.AverageMilliseconds,16} | {b.Fastest,-16} | {b.Slowest,-16} | {Iterations,10} |");
            }

            return sb.ToString();
        }
    }
}
