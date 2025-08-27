using MeasureMap.Tracers.Metrics;
using Polaroider;

namespace MeasureMap.UnitTest.Tracers.Metrics
{
    public class ProfilerMetricFactoryTests
    {
        [Test]
        public void ProfilerMetricFactory_Internal()
        {
            new ProfilerMetricFactory(ProfilerMetric.Throughput, MetricCategory.Duration, r => string.Empty).MatchSnapshot();
        }

        [Test]
        public void ProfilerMetricFactory_GetMetric()
        {
            var result = new Mock<IProfilerResult>();
            var metric = new ProfilerMetricFactory(ProfilerMetric.AverageTicks, MetricCategory.Duration, r => "20");
            metric.GetMetric(result.Object).Should().Be("20");
        }

        [Test]
        public void ProfilerMetricFactory_GetMetric_Result()
        {
            var result = new Mock<IProfilerResult>();
            result.Setup(x => x.AverageTicks).Returns(20);
            var metric = new ProfilerMetricFactory(ProfilerMetric.AverageTicks, MetricCategory.Duration, r => r.AverageTicks);
            metric.GetMetric(result.Object).Should().Be("20");
        }
    }
}
