
namespace MeasureMap.Tracers.Metrics
{
    public class MetricCategory : Enumeration
    {
        public MetricCategory(string name) : base(name)
        {
        }

        public static readonly MetricCategory Warmup = new(nameof(Warmup));

        public static readonly MetricCategory Setup = new(nameof(Setup));

        public static readonly MetricCategory Duration = new(nameof(Duration));

        public static readonly MetricCategory Memory = new(nameof(Memory));
    }
}
