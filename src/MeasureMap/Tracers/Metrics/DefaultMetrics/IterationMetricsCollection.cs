
namespace MeasureMap.Tracers.Metrics.DefaultMetrics
{
    internal class IterationMetricsCollection : MetricsCollection<IterationMetric, IIterationMetric>
    {
        public IterationMetricsCollection()
        {
            Add(IterationMetric.AfterExecution, new IterationResultMetric(IterationMetric.AfterExecution, MetricCategory.Duration, r => r.AfterExecution, TextAlign.Right));
            Add(IterationMetric.AfterGarbageCollection, new IterationResultMetric(IterationMetric.AfterGarbageCollection, MetricCategory.Duration, r => r.AfterGarbageCollection, TextAlign.Right));
            Add(IterationMetric.Duration, new IterationResultMetric(IterationMetric.Duration, MetricCategory.Duration, r => r.Duration, TextAlign.Right));
            Add(IterationMetric.InitialSize, new IterationResultMetric(IterationMetric.InitialSize, MetricCategory.Duration, r => r.InitialSize, TextAlign.Right));
            Add(IterationMetric.Ticks, new IterationResultMetric(IterationMetric.Ticks, MetricCategory.Duration, r => r.Ticks, TextAlign.Right));
            Add(IterationMetric.Nanoseconds, new IterationResultMetric(IterationMetric.Nanoseconds, MetricCategory.Duration, r => $"{r.Ticks.ToNanoseconds()} ns", TextAlign.Right));
            Add(IterationMetric.Microseconds, new IterationResultMetric(IterationMetric.Microseconds, MetricCategory.Duration, r => $"{r.Ticks.ToMicroseconds()} μs", TextAlign.Right));
            Add(IterationMetric.Milliseconds, new IterationResultMetric(IterationMetric.Milliseconds, MetricCategory.Duration, r => $"{r.Ticks.ToMilliseconds()} ms", TextAlign.Right));
            Add(IterationMetric.TimeStamp, new IterationResultMetric(IterationMetric.TimeStamp, MetricCategory.Duration, r => r.TimeStamp.ToString("o")));
            Add(IterationMetric.Iteration, new IterationResultMetric(IterationMetric.Iteration, MetricCategory.Duration, r => r.Iteration));
            Add(IterationMetric.ThreadId, new IterationResultMetric(IterationMetric.ThreadId, MetricCategory.Duration, r => r.ThreadId));
            Add(IterationMetric.ProcessId, new IterationResultMetric(IterationMetric.ProcessId, MetricCategory.Duration, r => r.ProcessId));
        }
    }
}
