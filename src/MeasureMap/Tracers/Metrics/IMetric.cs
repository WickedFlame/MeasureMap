
namespace MeasureMap.Tracers.Metrics
{
    /// <summary>
    /// A metric that is used in the output
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMetric<in T>
    {
        /// <summary>
        /// Name of the metric
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Category of the metric is used for grouping metrics
        /// </summary>
        string Category { get; }

        /// <summary>
        /// Alignment in a grid
        /// </summary>
        TextAlign TextAlign { get; }

        /// <summary>
        /// Get the string value of the metric
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        string GetMetric(T result);
    }
}
