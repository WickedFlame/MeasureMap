using System;

namespace MeasureMap.Tracers.Metrics
{
    /// <summary>
    /// A metric resolver fof <see cref="IResult"/>
    /// </summary>
    public class ThreadMetricFactory : BaseMetric<ThreadMetric, IResult>, IThreadMetric
    {
        private readonly Func<IResult, object> _metric;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="category"></param>
        /// <param name="metric"></param>
        /// <param name="align"></param>
        public ThreadMetricFactory(ThreadMetric type, MetricCategory category, Func<IResult, object> metric, TextAlign align = TextAlign.Left)
            : base(type, category, align)
        {
            _metric = metric;
        }

        /// <summary>
        /// Get the metric value
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public override string GetMetric(IResult result) => _metric(result)?.ToString();
    }
}
