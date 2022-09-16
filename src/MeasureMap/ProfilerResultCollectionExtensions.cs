using System.Linq;
using System.Text;

namespace MeasureMap
{
    /// <summary>
    /// Extension methods for the IProfilerResultCollection
    /// </summary>
    public static class ProfilerResultCollectionExtensions
    {
        /// <summary>
        /// Traces the output of a Benchmark Test
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static string Trace(this IProfilerResultCollection results)
        {
            var sb = new StringBuilder();

            sb.AppendLine("## MeasureMap Benchmark");
            sb.AppendLine($" Iterations:\t\t{results.Iterations}");
            sb.AppendLine($"### Summary");
            sb.AppendLine("| Name | Avg Time | Avg ms | Avg Ticks | Total | Fastest | Slowest | Iterations |");
            sb.AppendLine("|--- |---: |---: |---: |---: |---: |---: |---: |");
            foreach (var key in results.Keys)
            {
                sb.AppendLine($"| {key} {TraceLine(results[key])}");
            }

            var result = sb.ToString();

            System.Diagnostics.Trace.WriteLine(result);

            return result;
        }

        private static string TraceLine(IProfilerResult result)
        {
            return $"| {result.AverageTime} | {result.AverageMilliseconds} | {result.AverageTicks} | {result.TotalTime.ToString()} | {result.Fastest.Ticks} | {result.Slowest.Ticks} | {result.Iterations.Count()} |";
        }
    }
}
