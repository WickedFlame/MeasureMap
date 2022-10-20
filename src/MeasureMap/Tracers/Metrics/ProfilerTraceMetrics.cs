
namespace MeasureMap.Tracers.Metrics
{
    /// <summary>
    /// Defaultmetrics for <see cref="IProfilerResult"/>
    /// </summary>
    public class ProfilerTraceMetrics : TraceMetrics
    {
        private ProfilerTraceMetrics()
        {
            Add(ProfilerMetric.DurationWarmup);
            Add(ProfilerMetric.Threads);
            Add(ProfilerMetric.Iterations);
            Add(ProfilerMetric.Duration);
            Add(ProfilerMetric.TotalTime);
            Add(ProfilerMetric.AverageTime);
            Add(ProfilerMetric.AverageMilliseconds);
            Add(ProfilerMetric.Throughput);
            Add(ProfilerMetric.Fastest);
            Add(ProfilerMetric.Slowest);
            Add(ProfilerMetric.MemoryInitialSize);
            Add(ProfilerMetric.MemoryEndSize);
            Add(ProfilerMetric.MemoryIncrease);

            Add(ThreadMetric.ThreadId);
            Add(ThreadMetric.Iterations);
            Add(ThreadMetric.AverageTime);
            Add(ThreadMetric.Slowest);
            Add(ThreadMetric.Fastest);
            Add(ThreadMetric.Throughput);

            Add(DetailMetric.ThreadId);
            Add(DetailMetric.Iteration);
            Add(DetailMetric.TimeStamp);
            Add(DetailMetric.Milliseconds);
        }

        /// <summary>
        /// Get a set of defaultmetrics used for <see cref="IProfilerResult"/>
        /// </summary>
        /// <returns></returns>
        public static TraceMetrics GetDefaultTraceMetrics()
        {
            return new ProfilerTraceMetrics();
        }
    }
}
