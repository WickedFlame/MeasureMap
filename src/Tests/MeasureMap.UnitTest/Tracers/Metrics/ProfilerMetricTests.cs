using MeasureMap.Tracers.Metrics;
using NUnit.Framework;
using Polaroider;

namespace MeasureMap.UnitTest.Tracers.Metrics
{
    public class ProfilerMetricTests
    {
        [Test]
        public void ProfilerMetric_Create_FromString()
        {
            var metric = ProfilerMetric.Create("custom", r => r.ThreadId);
            metric.MatchSnapshot();
        }
    }
}
