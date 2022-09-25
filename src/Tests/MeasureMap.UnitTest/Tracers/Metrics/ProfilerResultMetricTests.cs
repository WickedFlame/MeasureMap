using FluentAssertions;
using MeasureMap.Tracers.Metrics;
using Moq;
using NUnit.Framework;
using Polaroider;

namespace MeasureMap.UnitTest.Tracers.Metrics
{
    public class ProfilerResultMetricTests
    {
        [Test]
        public void ProfilerResultMetric_Internal()
        {
            new ProfilerResultMetric(ProfilerMetric.Throughput, MetricCategory.Duration, r => string.Empty).MatchSnapshot();
        }

        [Test]
        public void ProfilerResultMetric_GetMetric()
        {
            var result = new Mock<IProfilerResult>();
            var metric = new ProfilerResultMetric(ProfilerMetric.AverageTicks, MetricCategory.Duration, r => "20");
            metric.GetMetric(result.Object).Should().Be("20");
        }

        [Test]
        public void ProfilerResultMetric_GetMetric_Result()
        {
            var result = new Mock<IProfilerResult>();
            result.Setup(x => x.AverageTicks).Returns(20);
            var metric = new ProfilerResultMetric(ProfilerMetric.AverageTicks, MetricCategory.Duration, r => r.AverageTicks);
            metric.GetMetric(result.Object).Should().Be("20");
        }
    }
}
