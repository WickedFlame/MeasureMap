using MeasureMap.Tracers.Metrics;
using NUnit.Framework;
using Polaroider;

namespace MeasureMap.UnitTest.Tracers.Metrics
{
    public class ThreadMetricTests
    {
        [Test]
        public void ThreadMetric_Create_FromString()
        {
            var metric = ThreadMetric.Create("custom", r => r.ThreadId);
            metric.MatchSnapshot();
        }
    }
}
