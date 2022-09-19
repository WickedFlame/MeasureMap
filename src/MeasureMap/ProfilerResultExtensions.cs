using MeasureMap.Tracers;
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
        public static string Trace(this IProfilerResult profilerResult, bool fullTrace, string header = "### MeasureMap - Profiler result for Profilesession")
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(header))
            {
                sb.AppendLine(header);
            }

            sb.AppendLine($"#### Summary");
            sb.AppendLine($"\tWarmup ========================================");
            sb.AppendLine($"\t\tDuration Warmup:\t\t\t{profilerResult.Warmup().ToString()}");
            sb.AppendLine($"\tSetup ========================================");
            sb.AppendLine($"\t\tThreads:\t\t\t{profilerResult.Threads()}");
            sb.AppendLine($"\t\tIterations:\t\t\t{profilerResult.Iterations.Count()}");
            sb.AppendLine($"\tDuration ========================================");
            sb.AppendLine($"\t\tDuration:\t\t\t{profilerResult.Elapsed()}");
            sb.AppendLine($"\t\tTotal Time:\t\t\t{profilerResult.TotalTime.ToString()}");
            sb.AppendLine($"\t\tAverage Time:\t\t\t{profilerResult.AverageTime}");
            sb.AppendLine($"\t\tAverage Milliseconds:\t\t{profilerResult.AverageMilliseconds}");
            sb.AppendLine($"\t\tAverage Ticks:\t\t\t{profilerResult.AverageTicks}");
            sb.AppendLine($"\t\tFastest:\t\t\t{TimeSpan.FromTicks(profilerResult.Fastest.Ticks)}");
            sb.AppendLine($"\t\tSlowest:\t\t\t{TimeSpan.FromTicks(profilerResult.Slowest.Ticks)}");
            sb.AppendLine($"\tMemory ==========================================");
            sb.AppendLine($"\t\tMemory Initial size:\t\t{profilerResult.InitialSize}");
            sb.AppendLine($"\t\tMemory End size:\t\t{profilerResult.EndSize}");
            sb.AppendLine($"\t\tMemory Increase:\t\t{profilerResult.Increase}");

            if(profilerResult.Threads() > 1)
            {
                sb.AppendLine();
                sb.AppendLine("#### Details per Thread");
                sb.AppendLine("| ThreadId | Iterations | Average time | Slowest | Fastest |");
                sb.AppendLine("| --- | --- | ---: | ---: | ---: |");
                foreach (var thread in profilerResult)
                {
                    sb.AppendLine($"| {thread.ThreadId} | {thread.Iterations.Count()} | {thread.AverageTime} | {thread.Slowest.Duration} | {thread.Fastest.Duration} |");
                }
            }

            if (fullTrace)
            {
                sb.AppendLine();
                sb.AppendLine($"#### Iterations");
                sb.AppendLine("| ThreadId | Iteration | Timestamp | Duration | Init size | End size |");
                sb.AppendLine("| --- | --- | --- | --- | ---: | ---: |");
                foreach (var iteration in profilerResult.Iterations.OrderBy(i => i.TimeStamp))
                {
                    sb.AppendLine($"| {iteration.ThreadId} | {iteration.Iteration} | {iteration.TimeStamp:o} | {iteration.Duration} | {iteration.InitialSize} | {iteration.AfterExecution} |");
                }
            }

            var result = sb.ToString();
#if DEBUG
            System.Diagnostics.Trace.WriteLine(result);
#else
            Console.WriteLine(result);
#endif
            return result;
        }

        /// <summary>
        /// Traces the output of a Profiler
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static void Trace(this IProfilerResult result)
        {
            result.Trace(TraceOptions.Default.Tracer, TraceOptions.Default.ResultWriter);
        }

        /// <summary>
        /// Trace the output of a Profiler
        /// </summary>
        /// <param name="result"></param>
        /// <param name="options"></param>
        public static void Trace(this IProfilerResult result, TraceOptions options)
        {
            result.Trace(options.Tracer, options.ResultWriter);
        }

        /// <summary>
        /// Trace the output of a Profiler
        /// </summary>
        /// <param name="result"></param>
        /// <param name="tracer"></param>
        public static void Trace(this IProfilerResult result, ITracer tracer)
        {
            result.Trace(tracer, TraceOptions.Default.ResultWriter);
        }

        /// <summary>
        /// Trace the output of a Profiler
        /// </summary>
        /// <param name="result"></param>
        /// <param name="writer"></param>
        public static void Trace(this IProfilerResult result, IResultWriter writer)
        {
            result.Trace(TraceOptions.Default.Tracer, writer);
        }

        /// <summary>
        /// Trace the output of a Benchmark Test
        /// </summary>
        /// <param name="result"></param>
        /// <param name="tracer"></param>
        /// <param name="writer"></param>
        public static void Trace(this IProfilerResult result, ITracer tracer, IResultWriter writer)
        {
            tracer.Trace(result, writer);
        }

        /// <summary>
        /// Returns the timespan that the Warmup took
        /// </summary>
        /// <param name="result">The ProfilerResult</param>
        /// <returns>The timespan that the warmup took</returns>
        public static TimeSpan Warmup(this IProfilerResult result)
        {
            if (result.ResultValues.ContainsKey(ResultValueType.Warmup))
            {
                return (TimeSpan)result.ResultValues[ResultValueType.Warmup];
            }

            return new TimeSpan();
        }

        /// <summary>
        /// Returns the timespan that the complete Session took
        /// </summary>
        /// <param name="result">The ProfilerResult</param>
        /// <returns>The timespan that the session took</returns>
        public static TimeSpan Elapsed(this IProfilerResult result)
        {
            if (result.ResultValues.ContainsKey(ResultValueType.Elapsed))
            {
                return (TimeSpan)result.ResultValues[ResultValueType.Elapsed];
            }

            return new TimeSpan();
        }

        /// <summary>
        /// Returns the amount of threads used
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static int Threads(this IProfilerResult result)
        {
            if (result.ResultValues.ContainsKey(ResultValueType.Threads))
            {
                return (int)result.ResultValues[ResultValueType.Threads];
            }

            return 1;
        }

        /// <summary>
        /// Returns the average throughput per second
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static double Throughput(this IProfilerResult result)
        {
            var elapsed = result.Elapsed();
            if (elapsed == TimeSpan.MinValue)
            {
                elapsed = result.TotalTime;
            }

            return result.Iterations.Count() / elapsed.TotalSeconds;
        }
    }
}
