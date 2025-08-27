using MeasureMap.Tracers.Metrics;
using Polaroider;

namespace MeasureMap.UnitTest.Tracers.Metrics
{
    public class ThreadMetricFactoryTests
    {
        [Test]
        public void ThreadMetricFactory_Internal()
        {
            new ThreadMetricFactory(ThreadMetric.Throughput, MetricCategory.Duration, r => string.Empty).MatchSnapshot();
        }

        [Test]
        public void ThreadMetricFactory_GetMetric()
        {
            var result = new Mock<IResult>();
            var metric = new ThreadMetricFactory(ThreadMetric.ThreadId, MetricCategory.Duration, r => "result");
            metric.GetMetric(result.Object).Should().Be("result");
        }

        [Test]
        public void ThreadMetricFactory_GetMetric_Result()
        {
            var result = new Mock<IResult>();
            result.Setup(x => x.ThreadId).Returns(1);
            var metric = new ThreadMetricFactory(ThreadMetric.ThreadId, MetricCategory.Duration, r => r.ThreadId);
            metric.GetMetric(result.Object).Should().Be("1");
        }
    }
}
