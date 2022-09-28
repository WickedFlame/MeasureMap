using MeasureMap.Tracers.Metrics;
using NUnit.Framework;
using Polaroider;

namespace MeasureMap.UnitTest.Tracers.Metrics
{
    public class ProfilerTraceMetricsTests
    {
        [Test]
        public void ProfilerTraceMetrics_GetDefaultMetrics()
        {
            ProfilerTraceMetrics.GetDefaultTraceMetrics().MatchSnapshot();
        }
    }
}
