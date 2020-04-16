using System;
using System.Collections.Generic;
using System.Text;

namespace MeasureMap
{
    public static class ProfilerResultCollectionExtensions
    {
        public static string Trace(this IProfilerResultCollection results)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"##### Summary");
            sb.AppendLine("| Name | Avg Time | Avg Ticks | Total            | Fastest | Slowest | Memory Increase |");
            sb.AppendLine("|----- |--------: |---------: |----------------: |-------: |-------: |---------------: |");
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
            return $"| {result.AverageTime} | {result.AverageTicks} | {result.TotalTime.ToString()} | {result.Fastest.Ticks} | {result.Slowest.Ticks} | {result.Increase} |";
        }
    }
}
