using System;
using System.Linq;
using System.Text;

namespace MeasureMap
{
    /// <summary>
    /// 
    /// </summary>
    public static class ProfilerResultExtensions
    {
        /// <summary>
        /// Trace the result to the Console
        /// </summary>
        public static string Trace(this ProfilerResult profilerResult, string header = "### MeasureMap - Profiler result for Profilesession")
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(header))
            {
                sb.AppendLine(header);
            }

            sb.AppendLine($"\tSetup ========================================");
            sb.AppendLine($"\t\tIterations:\t\t\t{profilerResult.Iterations.Count()}");
            sb.AppendLine($"\tDuration ========================================");
            sb.AppendLine($"\t\tDuration Total:\t\t\t{profilerResult.TotalTime.ToString()}");
            sb.AppendLine($"\t\tAverage Time:\t\t\t{profilerResult.AverageTime}");
            sb.AppendLine($"\t\tAverage Milliseconds:\t\t{profilerResult.AverageMilliseconds}");
            sb.AppendLine($"\t\tAverage Ticks:\t\t\t{profilerResult.AverageTicks}");
            sb.AppendLine($"\t\tFastest:\t\t\t{TimeSpan.FromTicks(profilerResult.Fastest.Ticks)}");
            sb.AppendLine($"\t\tSlowest:\t\t\t{TimeSpan.FromTicks(profilerResult.Slowest.Ticks)}");
            sb.AppendLine($"\tMemory ==========================================");
            sb.AppendLine($"\t\tMemory Initial size:\t\t{profilerResult.InitialSize}");
            sb.AppendLine($"\t\tMemory End size:\t\t{profilerResult.EndSize}");
            sb.AppendLine($"\t\tMemory Increase:\t\t{profilerResult.Increase}");

            var result = sb.ToString();
            
            System.Diagnostics.Trace.WriteLine(result);

            return result;
        }
    }
}
