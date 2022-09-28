using System;

namespace MeasureMap.Tracers.Metrics
{
    /// <summary>
    /// A metric resolver fof <see cref="IProfilerResult"/>
    /// </summary>
    public class ProfilerMetricFactory : BaseMetric<ProfilerMetric, IProfilerResult>, IProfilerMetric
    {
        private readonly Func<IProfilerResult, object> _metric;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="category"></param>
        /// <param name="metric"></param>
        /// <param name="align"></param>
        public ProfilerMetricFactory(ProfilerMetric type, MetricCategory category, Func<IProfilerResult, object> metric, TextAlign align = TextAlign.Left)
        : base(type, category, align)
        {
            _metric = metric;
        }

        /// <summary>
        /// Get the metric value
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public override string GetMetric(IProfilerResult result) => _metric(result)?.ToString();
    }
}
