
namespace MeasureMap.Tracers.Metrics
{
    /// <summary>
    /// baseclass used for simple metrics
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseMetric<TType, T> : IMetric<T> where TType : Enumeration
    {
        private readonly TType _metricType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="category"></param>
        /// <param name="align"></param>
        protected BaseMetric(TType type, MetricCategory category, TextAlign align)
        {
            _metricType = type;
            Category = category;
            TextAlign = align;
        }

        /// <summary>
        /// Category of the metric is used for grouping metrics
        /// </summary>
        public string Category { get; }

        /// <summary>
        /// Name of the metric
        /// </summary>
        public string Name => _metricType.Name;

        /// <summary>
        /// Alignment in a grid
        /// </summary>
        public TextAlign TextAlign { get; set; }

        /// <summary>
        /// Get the string value of the metric
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public abstract string GetMetric(T result);
    }
}
