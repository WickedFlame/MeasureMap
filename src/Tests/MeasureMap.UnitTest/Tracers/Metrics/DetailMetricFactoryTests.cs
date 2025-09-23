using MeasureMap.Tracers.Metrics;
using Polaroider;

namespace MeasureMap.UnitTest.Tracers.Metrics
{
    public class DetailMetricFactoryTests
    {
        [Test]
        public void DetailMetricFactory_Internal()
        {
            new DetailMetricFactory(DetailMetric.Ticks, MetricCategory.Duration, r => string.Empty).MatchSnapshot();
        }

        [Test]
        public void DetailMetricFactory_GetMetric()
        {
            var result = new Mock<IIterationResult>();
            var metric = new DetailMetricFactory(DetailMetric.Ticks, MetricCategory.Duration, r => "result");
            metric.GetMetric(result.Object).Should().Be("result");
        }

        [Test]
        public void DetailMetricFactory_GetMetric_Result()
        {
            var result = new Mock<IIterationResult>();
            result.Setup(x => x.Ticks).Returns(20);
            var metric = new DetailMetricFactory(DetailMetric.Ticks, MetricCategory.Duration, r => r.Ticks);
            metric.GetMetric(result.Object).Should().Be("20");
        }
    }
}
