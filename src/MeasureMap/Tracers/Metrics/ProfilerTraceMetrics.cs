
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

            Add(ProfileThreadMetric.ThreadId);
            Add(ProfileThreadMetric.Iterations);
            Add(ProfileThreadMetric.AverageTime);
            Add(ProfileThreadMetric.Slowest);
            Add(ProfileThreadMetric.Fastest);
            Add(ProfileThreadMetric.Throughput);

            Add(IterationMetric.ThreadId);
            Add(IterationMetric.Iteration);
            Add(IterationMetric.TimeStamp);
            Add(IterationMetric.Milliseconds);
            Add(IterationMetric.InitialSize);
            Add(IterationMetric.AfterExecution);
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
