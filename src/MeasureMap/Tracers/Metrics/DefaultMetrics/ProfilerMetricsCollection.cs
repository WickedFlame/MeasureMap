using System.Linq;
using System;

namespace MeasureMap.Tracers.Metrics.DefaultMetrics
{
    /// <summary>
    /// 
    /// </summary>
    internal class ProfilerMetricsCollection : MetricsCollection<ProfilerMetric, IProfilerMetric>
    {
        /// <summary>
        /// Get a default set of ProfilerMetrics
        /// </summary>
        public ProfilerMetricsCollection()
        {
            Add(ProfilerMetric.DurationWarmup, new ProfilerResultMetric(ProfilerMetric.DurationWarmup, MetricCategory.Warmup, r => r.Warmup(), TextAlign.Right));
            Add(ProfilerMetric.Threads, new ProfilerResultMetric(ProfilerMetric.Threads, MetricCategory.Setup, r => r.Threads()));
            Add(ProfilerMetric.Iterations, new ProfilerResultMetric(ProfilerMetric.Iterations, MetricCategory.Setup, r => r.Iterations.Count()));
            Add(ProfilerMetric.Duration, new ProfilerResultMetric(ProfilerMetric.Duration, MetricCategory.Duration, r => r.Elapsed(), TextAlign.Right));
            Add(ProfilerMetric.TotalTime, new ProfilerResultMetric(ProfilerMetric.TotalTime, MetricCategory.Duration, r => r.TotalTime.ToString(), TextAlign.Right));
            Add(ProfilerMetric.Throughput, new ProfilerResultMetric(ProfilerMetric.Throughput, MetricCategory.Duration, r => $"{r.Throughput()}/s", TextAlign.Right));
            Add(ProfilerMetric.AverageTime, new ProfilerResultMetric(ProfilerMetric.AverageTime, MetricCategory.Duration, r => r.AverageTime, TextAlign.Right));
            Add(ProfilerMetric.AverageNanoseconds, new ProfilerResultMetric(ProfilerMetric.AverageNanoseconds, MetricCategory.Duration, r => $"{r.AverageTicks.ToNanoseconds()} ns", TextAlign.Right));
            Add(ProfilerMetric.AverageMicroseconds, new ProfilerResultMetric(ProfilerMetric.AverageMicroseconds, MetricCategory.Duration, r => $"{r.AverageTicks.ToMicroseconds()} μs", TextAlign.Right));
            Add(ProfilerMetric.AverageMilliseconds, new ProfilerResultMetric(ProfilerMetric.AverageMilliseconds, MetricCategory.Duration, r => $"{r.AverageTicks.ToMilliseconds()} ms", TextAlign.Right));
            Add(ProfilerMetric.AverageTicks, new ProfilerResultMetric(ProfilerMetric.AverageTicks, MetricCategory.Duration, r => r.AverageTicks, TextAlign.Right));
            Add(ProfilerMetric.Fastest, new ProfilerResultMetric(ProfilerMetric.Fastest, MetricCategory.Duration, r => TimeSpan.FromTicks(r.Fastest.Ticks), TextAlign.Right));
            Add(ProfilerMetric.Slowest, new ProfilerResultMetric(ProfilerMetric.Slowest, MetricCategory.Duration, r => TimeSpan.FromTicks(r.Slowest.Ticks), TextAlign.Right));

            Add(ProfilerMetric.MemoryInitialSize, new ProfilerResultMetric(ProfilerMetric.MemoryInitialSize, MetricCategory.Memory, r => r.InitialSize, TextAlign.Right));
            Add(ProfilerMetric.MemoryEndSize, new ProfilerResultMetric(ProfilerMetric.MemoryEndSize, MetricCategory.Memory, r => r.EndSize, TextAlign.Right));
            Add(ProfilerMetric.MemoryIncrease, new ProfilerResultMetric(ProfilerMetric.MemoryIncrease, MetricCategory.Memory, r => r.Increase, TextAlign.Right));
        }
    }
}
