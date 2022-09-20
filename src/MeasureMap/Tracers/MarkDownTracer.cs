using System;
using System.Linq;

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
        public void Trace(IProfilerResult result, IResultWriter writer)
        {
            writer.WriteLine("### MeasureMap - Profiler result for Profilesession");

            writer.WriteLine($"#### Summary");
            writer.WriteLine($"\tWarmup ========================================");
            writer.WriteLine($"\t\tDuration Warmup:\t\t\t{result.Warmup().ToString()}");
            writer.WriteLine($"\tSetup ========================================");
            writer.WriteLine($"\t\tThreads:\t\t\t{result.Threads()}");
            writer.WriteLine($"\t\tIterations:\t\t\t{result.Iterations.Count()}");
            writer.WriteLine($"\tDuration ========================================");
            writer.WriteLine($"\t\tDuration:\t\t\t{result.Elapsed()}");
            writer.WriteLine($"\t\tTotal Time:\t\t\t{result.TotalTime.ToString()}");
            writer.WriteLine($"\t\tAverage Time:\t\t\t{result.AverageTime}");
            writer.WriteLine($"\t\tAverage Milliseconds:\t\t{result.AverageMilliseconds}");
            writer.WriteLine($"\t\tAverage Ticks:\t\t\t{result.AverageTicks}");
            writer.WriteLine($"\t\tFastest:\t\t\t{TimeSpan.FromTicks(result.Fastest.Ticks)}");
            writer.WriteLine($"\t\tSlowest:\t\t\t{TimeSpan.FromTicks(result.Slowest.Ticks)}");
            writer.WriteLine($"\tMemory ==========================================");
            writer.WriteLine($"\t\tMemory Initial size:\t\t{result.InitialSize}");
            writer.WriteLine($"\t\tMemory End size:\t\t{result.EndSize}");
            writer.WriteLine($"\t\tMemory Increase:\t\t{result.Increase}");

            if (result.Threads() > 1)
            {
                writer.WriteLine(string.Empty);
                writer.WriteLine("#### Details per Thread");
                writer.WriteLine("| ThreadId | Iterations | Average time | Slowest | Fastest |");
                writer.WriteLine("| --- | --- | ---: | ---: | ---: |");
                foreach (var thread in result)
                {
                    writer.WriteLine($"| {thread.ThreadId} | {thread.Iterations.Count()} | {thread.AverageTime} | {thread.Slowest.Duration} | {thread.Fastest.Duration} |");
                }
            }

            if (TraceOptions.Default.TraceFullStack)
            {
                writer.WriteLine(string.Empty);
                writer.WriteLine($"#### Iterations");
                writer.WriteLine("| ThreadId | Iteration | Timestamp | Duration | Init size | End size |");
                writer.WriteLine("| --- | --- | --- | --- | ---: | ---: |");
                foreach (var iteration in result.Iterations.OrderBy(i => i.TimeStamp))
                {
                    writer.WriteLine($"| {iteration.ThreadId} | {iteration.Iteration} | {iteration.TimeStamp:o} | {iteration.Duration} | {iteration.InitialSize} | {iteration.AfterExecution} |");
                }
            }
        }

        /// <summary>
        /// Trace the <see cref="IBenchmarkResult"/> as MarkDown to the ouput
        /// </summary>
        /// <param name="result"></param>
        /// <param name="writer"></param>
        public void Trace(IBenchmarkResult result, IResultWriter writer)
        {
            writer.WriteLine("## MeasureMap Benchmark");
            writer.WriteLine($" Iterations:\t\t{result.Iterations}");
            writer.WriteLine($"### Summary");
            writer.WriteLine("| Name | Avg Time | Avg ms | Avg Ticks | Total | Fastest | Slowest | Iterations | Throughput |");
            writer.WriteLine("|--- |---: |---: |---: |---: |---: |---: |---: |---: |");
            foreach (var key in result.Keys)
            {
                writer.WriteLine($"| {key} {TraceLine(result[key])}");
            }
        }

        private static string TraceLine(IProfilerResult result)
        {
            return $"| {result.AverageTime} | {result.AverageMilliseconds} | {result.AverageTicks} | {result.TotalTime.ToString()} | {result.Fastest.Ticks} | {result.Slowest.Ticks} | {result.Iterations.Count()} | {result.Throughput()} |";
        }
    }
}
