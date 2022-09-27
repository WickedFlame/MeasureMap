
using MeasureMap.Tracers.Metrics;

namespace MeasureMap.Benchmark
{
    /// <summary>
    /// Defaultmetrics for <see cref="IBenchmarkResult"/>
    /// </summary>
    public class TraceMetrics : Tracers.TraceMetrics
    {
        public TraceMetrics()
        {
            Add(ProfilerMetric.AverageTime);
            Add(ProfilerMetric.AverageMilliseconds);
            Add(ProfilerMetric.Fastest);
            Add(ProfilerMetric.Slowest);
            Add(ProfilerMetric.Threads);
            Add(ProfilerMetric.Throughput);
        }
    }
}
