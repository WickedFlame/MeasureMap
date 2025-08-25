using MeasureMap.Tracers;
using System;
using System.Linq;
using System.Text;
using MeasureMap.Tracers.Metrics;

namespace MeasureMap
{
    /// <summary>
    /// 
    /// </summary>
    public static class ProfilerResultExtensions
    {
        /// <summary>
        /// Traces the output of a Profiler
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static void Trace(this IProfilerResult result)
        {
            result.Trace(TraceOptions.Default.Tracer, TraceOptions.Default.ResultWriter, TraceOptions.Default.Clone());
        }

        /// <summary>
        /// Trace the output of a Profiler
        /// </summary>
        /// <param name="result"></param>
        /// <param name="options"></param>
        public static void Trace(this IProfilerResult result, TraceOptions options)
        {
            result.Trace(options.Tracer, options.ResultWriter, options);
        }

        /// <summary>
        /// Trace the output of a Profiler
        /// </summary>
        /// <param name="result"></param>
        /// <param name="factory"></param>
        public static void Trace(this IProfilerResult result, Action<TraceOptions> factory)
        {
            var options = TraceOptions.Default.Clone();
            factory.Invoke(options);

            result.Trace(options);
        }

        /// <summary>
        /// Trace the output of a Profiler
        /// </summary>
        /// <param name="result"></param>
        /// <param name="tracer"></param>
        public static void Trace(this IProfilerResult result, ITracer tracer)
        {
            result.Trace(tracer, TraceOptions.Default.ResultWriter, TraceOptions.Default.Clone());
        }

        /// <summary>
        /// Trace the output of a Profiler
        /// </summary>
        /// <param name="result"></param>
        /// <param name="writer"></param>
        public static void Trace(this IProfilerResult result, IResultWriter writer)
        {
            result.Trace(TraceOptions.Default.Tracer, writer, TraceOptions.Default.Clone());
        }

        /// <summary>
        /// Trace the output of a Benchmark Test
        /// </summary>
        /// <param name="result"></param>
        /// <param name="tracer"></param>
        /// <param name="writer"></param>
        public static void Trace(this IProfilerResult result, ITracer tracer, IResultWriter writer)
        {
            result.Trace(tracer, writer, TraceOptions.Default.Clone());
        }

        /// <summary>
        /// Trace the output of a Benchmark Test
        /// </summary>
        /// <param name="result"></param>
        /// <param name="tracer"></param>
        /// <param name="writer"></param>
        /// <param name="options"></param>
        public static void Trace(this IProfilerResult result, ITracer tracer, IResultWriter writer, TraceOptions options)
        {
            options.Metrics ??= ProfilerTraceMetrics.GetDefaultTraceMetrics();

            tracer.Trace(result, writer, options);
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

            return result.Count();
        }
    }
}
