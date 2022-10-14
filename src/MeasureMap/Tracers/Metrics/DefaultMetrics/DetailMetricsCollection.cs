
namespace MeasureMap.Tracers.Metrics.DefaultMetrics
{
    internal class DetailMetricsCollection : MetricsCollection<DetailMetric, IDetailMetric>
    {
        public DetailMetricsCollection()
        {
            Add(DetailMetric.AfterExecution, new DetailMetricFactory(DetailMetric.AfterExecution, MetricCategory.Duration, r => r.AfterExecution, TextAlign.Right));
            Add(DetailMetric.AfterGarbageCollection, new DetailMetricFactory(DetailMetric.AfterGarbageCollection, MetricCategory.Duration, r => r.AfterGarbageCollection, TextAlign.Right));
            Add(DetailMetric.Duration, new DetailMetricFactory(DetailMetric.Duration, MetricCategory.Duration, r => r.Duration, TextAlign.Right));
            Add(DetailMetric.InitialSize, new DetailMetricFactory(DetailMetric.InitialSize, MetricCategory.Duration, r => r.InitialSize, TextAlign.Right));
            Add(DetailMetric.Ticks, new DetailMetricFactory(DetailMetric.Ticks, MetricCategory.Duration, r => r.Ticks, TextAlign.Right));
            Add(DetailMetric.Nanoseconds, new DetailMetricFactory(DetailMetric.Nanoseconds, MetricCategory.Duration, r => $"{r.Ticks.ToNanoseconds()} ns", TextAlign.Right));
            Add(DetailMetric.Microseconds, new DetailMetricFactory(DetailMetric.Microseconds, MetricCategory.Duration, r => $"{r.Ticks.ToMicroseconds()} μs", TextAlign.Right));
            Add(DetailMetric.Milliseconds, new DetailMetricFactory(DetailMetric.Milliseconds, MetricCategory.Duration, r => $"{r.Ticks.ToMilliseconds()} ms", TextAlign.Right));
            Add(DetailMetric.TimeStamp, new DetailMetricFactory(DetailMetric.TimeStamp, MetricCategory.Duration, r => r.TimeStamp.ToString("o")));
            Add(DetailMetric.Iteration, new DetailMetricFactory(DetailMetric.Iteration, MetricCategory.Duration, r => r.Iteration));
            Add(DetailMetric.ThreadId, new DetailMetricFactory(DetailMetric.ThreadId, MetricCategory.Duration, r => r.ThreadId));
            Add(DetailMetric.ProcessId, new DetailMetricFactory(DetailMetric.ProcessId, MetricCategory.Duration, r => r.ProcessId));
        }
    }
}
