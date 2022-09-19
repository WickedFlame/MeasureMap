using MeasureMap.Tracers;
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
        /// <param name="result"></param>
        /// <returns></returns>
        public static void Trace(this IProfilerResultCollection result)
        {
            result.Trace(TraceOptions.Default.Tracer, TraceOptions.Default.ResultWriter);
        }

        /// <summary>
        /// Trace the output of a Benchmark Test
        /// </summary>
        /// <param name="result"></param>
        /// <param name="options"></param>
        public static void Trace(this IProfilerResultCollection result, TraceOptions options)
        {
            result.Trace(options.Tracer, options.ResultWriter);
        }

        /// <summary>
        /// Trace the output of a Benchmark Test
        /// </summary>
        /// <param name="result"></param>
        /// <param name="tracer"></param>
        public static void Trace(this IProfilerResultCollection result, ITracer tracer)
        {
            result.Trace(tracer, TraceOptions.Default.ResultWriter);
        }

        /// <summary>
        /// Trace the output of a Benchmark Test
        /// </summary>
        /// <param name="result"></param>
        /// <param name="writer"></param>
        public static void Trace(this IProfilerResultCollection result, IResultWriter writer)
        {
            result.Trace(TraceOptions.Default.Tracer, writer);
        }

        /// <summary>
        /// Trace the output of a Benchmark Test
        /// </summary>
        /// <param name="result"></param>
        /// <param name="tracer"></param>
        /// <param name="writer"></param>
        public static void Trace(this IProfilerResultCollection result, ITracer tracer, IResultWriter writer)
        {
            tracer.Trace(result, writer);
        }
    }
}
