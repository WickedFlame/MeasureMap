using System.Linq;
using System;

namespace MeasureMap.Tracers.Metrics.DefaultMetrics
{
    internal class ProfileThreadMetricsCollection : MetricsCollection<ProfileThreadMetric, IProfileThreadResultMetric>
    {
        public ProfileThreadMetricsCollection()
        {
            Add(ProfileThreadMetric.ThreadId, new ProfileThreadMetricFactory(ProfileThreadMetric.ThreadId, MetricCategory.Warmup, r => r.ThreadId));
            Add(ProfileThreadMetric.Iterations, new ProfileThreadMetricFactory(ProfileThreadMetric.Iterations, MetricCategory.Setup, r => r.Iterations.Count()));
            Add(ProfileThreadMetric.TotalTime, new ProfileThreadMetricFactory(ProfileThreadMetric.TotalTime, MetricCategory.Duration, r => r.TotalTime.ToString(), TextAlign.Right));
            Add(ProfileThreadMetric.Throughput, new ProfileThreadMetricFactory(ProfileThreadMetric.Throughput, MetricCategory.Duration, r => $"{r.Throughput()}/s", TextAlign.Right));
            Add(ProfileThreadMetric.AverageTime, new ProfileThreadMetricFactory(ProfileThreadMetric.AverageTime, MetricCategory.Duration, r => r.AverageTime, TextAlign.Right));
            Add(ProfileThreadMetric.AverageNanoseconds, new ProfileThreadMetricFactory(ProfileThreadMetric.AverageNanoseconds, MetricCategory.Duration, r => $"{r.AverageTicks.ToNanoseconds()} ns", TextAlign.Right));
            Add(ProfileThreadMetric.AverageMicroseconds, new ProfileThreadMetricFactory(ProfileThreadMetric.AverageMicroseconds, MetricCategory.Duration, r => $"{r.AverageTicks.ToMicroseconds()} μs", TextAlign.Right));
            Add(ProfileThreadMetric.AverageMilliseconds, new ProfileThreadMetricFactory(ProfileThreadMetric.AverageMilliseconds, MetricCategory.Duration, r => $"{r.AverageTicks.ToMilliseconds()} ms", TextAlign.Right));
            Add(ProfileThreadMetric.AverageTicks, new ProfileThreadMetricFactory(ProfileThreadMetric.AverageTicks, MetricCategory.Duration, r => r.AverageTicks, TextAlign.Right));
            Add(ProfileThreadMetric.Fastest, new ProfileThreadMetricFactory(ProfileThreadMetric.Fastest, MetricCategory.Duration, r => TimeSpan.FromTicks(r.Fastest.Ticks), TextAlign.Right));
            Add(ProfileThreadMetric.Slowest, new ProfileThreadMetricFactory(ProfileThreadMetric.Slowest, MetricCategory.Duration, r => TimeSpan.FromTicks(r.Slowest.Ticks), TextAlign.Right));
            Add(ProfileThreadMetric.MemoryInitialSize, new ProfileThreadMetricFactory(ProfileThreadMetric.MemoryInitialSize, MetricCategory.Memory, r => r.InitialSize, TextAlign.Right));
            Add(ProfileThreadMetric.MemoryEndSize, new ProfileThreadMetricFactory(ProfileThreadMetric.MemoryEndSize, MetricCategory.Memory, r => r.EndSize, TextAlign.Right));
            Add(ProfileThreadMetric.MemoryIncrease, new ProfileThreadMetricFactory(ProfileThreadMetric.MemoryIncrease, MetricCategory.Memory, r => r.Increase, TextAlign.Right));
        }
    }
}
