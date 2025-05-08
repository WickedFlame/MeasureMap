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
            Add(ProfilerMetric.DurationWarmup, new ProfilerMetricFactory(ProfilerMetric.DurationWarmup, MetricCategory.Warmup, r => r.Warmup(), TextAlign.Right));
            Add(ProfilerMetric.Threads, new ProfilerMetricFactory(ProfilerMetric.Threads, MetricCategory.Setup, r => r.Threads()));
            Add(ProfilerMetric.Iterations, new ProfilerMetricFactory(ProfilerMetric.Iterations, MetricCategory.Setup, r => r.Iterations.Count()));
            Add(ProfilerMetric.Duration, new ProfilerMetricFactory(ProfilerMetric.Duration, MetricCategory.Duration, r => r.Elapsed(), TextAlign.Right));
            Add(ProfilerMetric.TotalTime, new ProfilerMetricFactory(ProfilerMetric.TotalTime, MetricCategory.Duration, r => r.TotalTime.ToString(), TextAlign.Right));
            Add(ProfilerMetric.Throughput, new ProfilerMetricFactory(ProfilerMetric.Throughput, MetricCategory.Duration, r => $"{r.Throughput()}/s", TextAlign.Right));
            Add(ProfilerMetric.AverageTime, new ProfilerMetricFactory(ProfilerMetric.AverageTime, MetricCategory.Duration, r => r.AverageTime, TextAlign.Right));
            Add(ProfilerMetric.AverageNanoseconds, new ProfilerMetricFactory(ProfilerMetric.AverageNanoseconds, MetricCategory.Duration, r => $"{r.AverageTicks.ToNanoseconds()} ns", TextAlign.Right));
            Add(ProfilerMetric.AverageMicroseconds, new ProfilerMetricFactory(ProfilerMetric.AverageMicroseconds, MetricCategory.Duration, r => $"{r.AverageTicks.ToMicroseconds()} μs", TextAlign.Right));
            Add(ProfilerMetric.AverageMilliseconds, new ProfilerMetricFactory(ProfilerMetric.AverageMilliseconds, MetricCategory.Duration, r => $"{r.AverageTicks.ToMilliseconds()} ms", TextAlign.Right));
            Add(ProfilerMetric.AverageTicks, new ProfilerMetricFactory(ProfilerMetric.AverageTicks, MetricCategory.Duration, r => r.AverageTicks, TextAlign.Right));
            Add(ProfilerMetric.Fastest, new ProfilerMetricFactory(ProfilerMetric.Fastest, MetricCategory.Duration, r => TimeSpan.FromMilliseconds(r.Fastest.Ticks.ToMilliseconds()), TextAlign.Right));
            Add(ProfilerMetric.Slowest, new ProfilerMetricFactory(ProfilerMetric.Slowest, MetricCategory.Duration, r => TimeSpan.FromMilliseconds(r.Slowest.Ticks.ToMilliseconds()), TextAlign.Right));

            Add(ProfilerMetric.MemoryInitialSize, new ProfilerMetricFactory(ProfilerMetric.MemoryInitialSize, MetricCategory.Memory, r => r.InitialSize, TextAlign.Right));
            Add(ProfilerMetric.MemoryEndSize, new ProfilerMetricFactory(ProfilerMetric.MemoryEndSize, MetricCategory.Memory, r => r.EndSize, TextAlign.Right));
            Add(ProfilerMetric.MemoryIncrease, new ProfilerMetricFactory(ProfilerMetric.MemoryIncrease, MetricCategory.Memory, r => r.Increase, TextAlign.Right));
        }
    }
}
