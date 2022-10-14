using System.Linq;
using System;

namespace MeasureMap.Tracers.Metrics.DefaultMetrics
{
    internal class ThreadMetricsCollection : MetricsCollection<ThreadMetric, IThreadMetric>
    {
        public ThreadMetricsCollection()
        {
            Add(ThreadMetric.ThreadId, new ThreadMetricFactory(ThreadMetric.ThreadId, MetricCategory.Warmup, r => r.ThreadId));
            Add(ThreadMetric.Iterations, new ThreadMetricFactory(ThreadMetric.Iterations, MetricCategory.Setup, r => r.Iterations.Count()));
            Add(ThreadMetric.TotalTime, new ThreadMetricFactory(ThreadMetric.TotalTime, MetricCategory.Duration, r => r.TotalTime.ToString(), TextAlign.Right));
            Add(ThreadMetric.Throughput, new ThreadMetricFactory(ThreadMetric.Throughput, MetricCategory.Duration, r => $"{r.Throughput()}/s", TextAlign.Right));
            Add(ThreadMetric.AverageTime, new ThreadMetricFactory(ThreadMetric.AverageTime, MetricCategory.Duration, r => r.AverageTime, TextAlign.Right));
            Add(ThreadMetric.AverageNanoseconds, new ThreadMetricFactory(ThreadMetric.AverageNanoseconds, MetricCategory.Duration, r => $"{r.AverageTicks.ToNanoseconds()} ns", TextAlign.Right));
            Add(ThreadMetric.AverageMicroseconds, new ThreadMetricFactory(ThreadMetric.AverageMicroseconds, MetricCategory.Duration, r => $"{r.AverageTicks.ToMicroseconds()} μs", TextAlign.Right));
            Add(ThreadMetric.AverageMilliseconds, new ThreadMetricFactory(ThreadMetric.AverageMilliseconds, MetricCategory.Duration, r => $"{r.AverageTicks.ToMilliseconds()} ms", TextAlign.Right));
            Add(ThreadMetric.AverageTicks, new ThreadMetricFactory(ThreadMetric.AverageTicks, MetricCategory.Duration, r => r.AverageTicks, TextAlign.Right));
            Add(ThreadMetric.Fastest, new ThreadMetricFactory(ThreadMetric.Fastest, MetricCategory.Duration, r => TimeSpan.FromTicks(r.Fastest.Ticks), TextAlign.Right));
            Add(ThreadMetric.Slowest, new ThreadMetricFactory(ThreadMetric.Slowest, MetricCategory.Duration, r => TimeSpan.FromTicks(r.Slowest.Ticks), TextAlign.Right));
            Add(ThreadMetric.MemoryInitialSize, new ThreadMetricFactory(ThreadMetric.MemoryInitialSize, MetricCategory.Memory, r => r.InitialSize, TextAlign.Right));
            Add(ThreadMetric.MemoryEndSize, new ThreadMetricFactory(ThreadMetric.MemoryEndSize, MetricCategory.Memory, r => r.EndSize, TextAlign.Right));
            Add(ThreadMetric.MemoryIncrease, new ThreadMetricFactory(ThreadMetric.MemoryIncrease, MetricCategory.Memory, r => r.Increase, TextAlign.Right));
        }
    }
}
