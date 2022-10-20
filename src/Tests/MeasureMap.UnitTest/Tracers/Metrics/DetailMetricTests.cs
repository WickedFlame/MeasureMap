using MeasureMap.Tracers.Metrics;
using NUnit.Framework;
using Polaroider;

namespace MeasureMap.UnitTest.Tracers.Metrics
{
    public class DetailMetricTests
    {
        [Test]
        public void DetailMetric_Create_FromString()
        {
            var metric = DetailMetric.Create("custom", r => r.ThreadId);
            metric.MatchSnapshot();
        }
    }
}
