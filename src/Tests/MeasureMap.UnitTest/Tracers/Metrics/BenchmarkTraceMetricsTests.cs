using MeasureMap.Tracers.Metrics;
using NUnit.Framework;
using Polaroider;

namespace MeasureMap.UnitTest.Tracers.Metrics
{
    public class BenchmarkTraceMetricsTests
    {
        [Test]
        public void BenchmarkTraceMetrics_GetDefaultMetrics()
        {
            BenchmarkTraceMetrics.GetDefaultTraceMetrics().MatchSnapshot();
        }
    }
}
