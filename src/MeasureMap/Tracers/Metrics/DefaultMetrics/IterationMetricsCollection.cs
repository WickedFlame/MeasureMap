
namespace MeasureMap.Tracers.Metrics.DefaultMetrics
{
    internal class IterationMetricsCollection : MetricsCollection<IterationMetric, IIterationMetric>
    {
        public IterationMetricsCollection()
        {
            Add(IterationMetric.AfterExecution, new IterationMetricFactory(IterationMetric.AfterExecution, MetricCategory.Duration, r => r.AfterExecution, TextAlign.Right));
            Add(IterationMetric.AfterGarbageCollection, new IterationMetricFactory(IterationMetric.AfterGarbageCollection, MetricCategory.Duration, r => r.AfterGarbageCollection, TextAlign.Right));
            Add(IterationMetric.Duration, new IterationMetricFactory(IterationMetric.Duration, MetricCategory.Duration, r => r.Duration, TextAlign.Right));
            Add(IterationMetric.InitialSize, new IterationMetricFactory(IterationMetric.InitialSize, MetricCategory.Duration, r => r.InitialSize, TextAlign.Right));
            Add(IterationMetric.Ticks, new IterationMetricFactory(IterationMetric.Ticks, MetricCategory.Duration, r => r.Ticks, TextAlign.Right));
            Add(IterationMetric.Nanoseconds, new IterationMetricFactory(IterationMetric.Nanoseconds, MetricCategory.Duration, r => $"{r.Ticks.ToNanoseconds()} ns", TextAlign.Right));
            Add(IterationMetric.Microseconds, new IterationMetricFactory(IterationMetric.Microseconds, MetricCategory.Duration, r => $"{r.Ticks.ToMicroseconds()} μs", TextAlign.Right));
            Add(IterationMetric.Milliseconds, new IterationMetricFactory(IterationMetric.Milliseconds, MetricCategory.Duration, r => $"{r.Ticks.ToMilliseconds()} ms", TextAlign.Right));
            Add(IterationMetric.TimeStamp, new IterationMetricFactory(IterationMetric.TimeStamp, MetricCategory.Duration, r => r.TimeStamp.ToString("o")));
            Add(IterationMetric.Iteration, new IterationMetricFactory(IterationMetric.Iteration, MetricCategory.Duration, r => r.Iteration));
            Add(IterationMetric.ThreadId, new IterationMetricFactory(IterationMetric.ThreadId, MetricCategory.Duration, r => r.ThreadId));
            Add(IterationMetric.ProcessId, new IterationMetricFactory(IterationMetric.ProcessId, MetricCategory.Duration, r => r.ProcessId));
        }
    }
}
