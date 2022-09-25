using FluentAssertions;
using MeasureMap.Tracers.Metrics;
using Moq;
using NUnit.Framework;
using Polaroider;

namespace MeasureMap.UnitTest.Tracers.Metrics
{
    public class ProfileThreadResultMetricTests
    {
        [Test]
        public void ProfileThreadResultMetric_Internal()
        {
            new ProfileThreadResultMetric(ProfileThreadMetric.Throughput, MetricCategory.Duration, r => string.Empty).MatchSnapshot();
        }

        [Test]
        public void ProfileThreadResultMetric_GetMetric()
        {
            var result = new Mock<IResult>();
            var metric = new ProfileThreadResultMetric(ProfileThreadMetric.ThreadId, MetricCategory.Duration, r => "result");
            metric.GetMetric(result.Object).Should().Be("result");
        }

        [Test]
        public void ProfileThreadResultMetric_GetMetric_Result()
        {
            var result = new Mock<IResult>();
            result.Setup(x => x.ThreadId).Returns(1);
            var metric = new ProfileThreadResultMetric(ProfileThreadMetric.ThreadId, MetricCategory.Duration, r => r.ThreadId);
            metric.GetMetric(result.Object).Should().Be("1");
        }
    }
}
