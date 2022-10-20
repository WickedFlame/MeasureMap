using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MeasureMap.Tracers;
using MeasureMap.Tracers.Metrics;
using NUnit.Framework;

namespace MeasureMap.UnitTest.Tracers
{
    public class TraceMetricsTests
    {
        [Test]
        public void TraceMetrics_Add_ProfilerMetric()
        {
            var metrics = new TraceMetrics();
            metrics.Add(ProfilerMetric.Throughput);

            metrics.GetProfilerMetrics().Single().Name.Should().Be(ProfilerMetric.Throughput);
        }

        [Test]
        public void TraceMetrics_Add_ProfilerMetric_AddCustom()
        {
            
            var metrics = new TraceMetrics();
            metrics.Add(new ProfilerMetric("custom"));

            metrics.GetProfilerMetrics().Should().BeEmpty();
        }

        [Test]
        public void TraceMetrics_Add_ProfilerMetric_AddNewCustom()
        {
            var type = new ProfilerMetric("custom");

            var metrics = new TraceMetrics();
            metrics.Add(new ProfilerMetricFactory(type, MetricCategory.Duration, r => string.Empty));

            metrics.GetProfilerMetrics().Single().Name.Should().Be(type);
        }

        [Test]
        public void TraceMetrics_Add_ThreadMetric()
        {
            var metrics = new TraceMetrics();
            metrics.Add(ThreadMetric.Throughput);

            metrics.GetThreadMetrics().Single().Name.Should().Be(ThreadMetric.Throughput);
        }

        [Test]
        public void TraceMetrics_Add_ThreadMetric_AddCustom()
        {

            var metrics = new TraceMetrics();
            metrics.Add(new ThreadMetric("custom"));

            metrics.GetThreadMetrics().Should().BeEmpty();
        }

        [Test]
        public void TraceMetrics_Add_ThreadMetric_AddNewCustom()
        {
            var type = new ThreadMetric("custom");

            var metrics = new TraceMetrics();
            metrics.Add(new ThreadMetricFactory(type, MetricCategory.Duration, r => string.Empty));

            metrics.GetThreadMetrics().Single().Name.Should().Be(type);
        }

        [Test]
        public void TraceMetrics_Add_DetailMetric()
        {
            var metrics = new TraceMetrics();
            metrics.Add(DetailMetric.Ticks);

            metrics.GetDetailMetrics().Single().Name.Should().Be(DetailMetric.Ticks);
        }

        [Test]
        public void TraceMetrics_Add_DetailMetric_AddCustom()
        {

            var metrics = new TraceMetrics();
            metrics.Add(new DetailMetric("custom"));

            metrics.GetDetailMetrics().Should().BeEmpty();
        }

        [Test]
        public void TraceMetrics_Add_DetailMetric_AddNewCustom()
        {
            var type = new DetailMetric("custom");

            var metrics = new TraceMetrics();
            metrics.Add(new DetailMetricFactory(type, MetricCategory.Duration, r => string.Empty));

            metrics.GetDetailMetrics().Single().Name.Should().Be(type);
        }
    }
}
