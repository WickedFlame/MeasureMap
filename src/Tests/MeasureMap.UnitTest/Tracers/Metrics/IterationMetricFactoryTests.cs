using FluentAssertions;
using MeasureMap.Tracers.Metrics;
using Moq;
using NUnit.Framework;
using Polaroider;

namespace MeasureMap.UnitTest.Tracers.Metrics
{
    public class IterationMetricFactoryTests
    {
        [Test]
        public void IterationMetricFactory_Internal()
        {
            new IterationMetricFactory(IterationMetric.Ticks, MetricCategory.Duration, r => string.Empty).MatchSnapshot();
        }

        [Test]
        public void IterationMetricFactory_GetMetric()
        {
            var result = new Mock<IIterationResult>();
            var metric = new IterationMetricFactory(IterationMetric.Ticks, MetricCategory.Duration, r => "result");
            metric.GetMetric(result.Object).Should().Be("result");
        }

        [Test]
        public void IterationMetricFactory_GetMetric_Result()
        {
            var result = new Mock<IIterationResult>();
            result.Setup(x => x.Ticks).Returns(20);
            var metric = new IterationMetricFactory(IterationMetric.Ticks, MetricCategory.Duration, r => r.Ticks);
            metric.GetMetric(result.Object).Should().Be("20");
        }
    }
}
