using MeasureMap.Tracers;
using MeasureMap.Tracers.Metrics;

namespace MeasureMap
{
    /// <summary>
    /// Extension methods for the IBenchmarkResult
    /// </summary>
    public static class BenchmarkResultExtensions
    {
        /// <summary>
        /// Traces the output of a Benchmark Test
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static void Trace(this IBenchmarkResult result)
        {
            result.Trace(TraceOptions.Default.Tracer, TraceOptions.Default.ResultWriter, TraceOptions.Default.Clone());
        }

        /// <summary>
        /// Trace the output of a Benchmark Test
        /// </summary>
        /// <param name="result"></param>
        /// <param name="options"></param>
        public static void Trace(this IBenchmarkResult result, TraceOptions options)
        {
            result.Trace(options.Tracer, options.ResultWriter, options);
        }

        /// <summary>
        /// Trace the output of a Benchmark Test
        /// </summary>
        /// <param name="result"></param>
        /// <param name="tracer"></param>
        public static void Trace(this IBenchmarkResult result, ITracer tracer)
        {
            result.Trace(tracer, TraceOptions.Default.ResultWriter, TraceOptions.Default.Clone());
        }

        /// <summary>
        /// Trace the output of a Benchmark Test
        /// </summary>
        /// <param name="result"></param>
        /// <param name="writer"></param>
        public static void Trace(this IBenchmarkResult result, IResultWriter writer)
        {
            result.Trace(TraceOptions.Default.Tracer, writer, TraceOptions.Default.Clone());
        }

        /// <summary>
        /// Trace the output of a Benchmark Test
        /// </summary>
        /// <param name="result"></param>
        /// <param name="tracer"></param>
        /// <param name="writer"></param>
        public static void Trace(this IBenchmarkResult result, ITracer tracer, IResultWriter writer)
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
        public static void Trace(this IBenchmarkResult result, ITracer tracer, IResultWriter writer, TraceOptions options)
        {
            options.Metrics ??= BenchmarkTraceMetrics.GetDefaultTraceMetrics();

            tracer.Trace(result, writer, options);
        }
    }
}
