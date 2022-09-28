
namespace MeasureMap.Tracers.Metrics
{
    /// <summary>
    /// Categorize the metrics
    /// </summary>
    public class MetricCategory : Enumeration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public MetricCategory(string name) : base(name)
        {
        }

        /// <summary>
        /// Warmup
        /// </summary>
        public static readonly MetricCategory Warmup = new(nameof(Warmup));

        /// <summary>
        /// Setup
        /// </summary>
        public static readonly MetricCategory Setup = new(nameof(Setup));

        /// <summary>
        /// Duration
        /// </summary>
        public static readonly MetricCategory Duration = new(nameof(Duration));

        /// <summary>
        /// Memory
        /// </summary>
        public static readonly MetricCategory Memory = new(nameof(Memory));
    }
}
