using System;

namespace MeasureMap.Tracers.Metrics
{
    /// <summary>
    /// A metric resolver fof <see cref="IIterationResult"/>
    /// </summary>
    public class IterationMetricFactory : BaseMetric<IterationMetric, IIterationResult>, IIterationMetric
    {
        private readonly Func<IIterationResult, object> _metric;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="category"></param>
        /// <param name="metric"></param>
        /// <param name="align"></param>
        public IterationMetricFactory(IterationMetric type, MetricCategory category, Func<IIterationResult, object> metric, TextAlign align = TextAlign.Left)
            : base(type, category, align)
        {
            _metric = metric;
        }

        /// <summary>
        /// Get the metric value
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public override string GetMetric(IIterationResult result) => _metric(result)?.ToString();
    }
}
