
namespace MeasureMap.Tracers.Metrics
{
    /// <summary>
    /// Defaultmetrics for <see cref="IBenchmarkResult"/>
    /// </summary>
    public class BenchmarkTraceMetrics : TraceMetrics
    {
        private BenchmarkTraceMetrics()
        {
            Add(ProfilerMetric.AverageTime);
            Add(ProfilerMetric.AverageMilliseconds);
            Add(ProfilerMetric.Fastest);
            Add(ProfilerMetric.Slowest);
            Add(ProfilerMetric.Iterations);
            Add(ProfilerMetric.Throughput);
        }

        /// <summary>
        /// Get a set of defaultmetrics used for <see cref="IBenchmarkResult"/>
        /// </summary>
        /// <returns></returns>
        public static TraceMetrics GetDefaultTraceMetrics()
        {
            return new BenchmarkTraceMetrics();
        }
    }
}
