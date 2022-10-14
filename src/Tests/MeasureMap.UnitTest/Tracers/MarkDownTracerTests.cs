using System;
using FluentAssertions;
using MeasureMap.Tracers;
using NUnit.Framework;
using Polaroider;

namespace MeasureMap.UnitTest.Tracers
{
    [SingleThreaded]
    public class MarkDownTracerTests
    {
        [Test]
        public void MarkDownTracer_ProfilerResult()
        {
            var writer = new StringResultWriter();

            var tracer = new MarkDownTracer();
            tracer.Trace(ResultFactory.CreateResult(), writer, new TraceOptions());

            writer.Value.MatchSnapshot();
        }

        [Test]
        [UpdateSnapshot]
        public void MarkDownTracer_ProfilerResult_Profile()
        {
            var writer = new StringResultWriter();

            var options = new TraceOptions()
            {
                TraceDetail = TraceDetail.Minimal
            };

            var tracer = new MarkDownTracer();
            tracer.Trace(ResultFactory.CreateResult(), writer, options);

            writer.Value.MatchSnapshot();
        }

        [Test]
        public void MarkDownTracer_ProfilerResult_DetailPerThread()
        {
            var writer = new StringResultWriter();
            
            var options = new TraceOptions()
            {
                TraceDetail = TraceDetail.DetailPerThread
            };
            var tracer = new MarkDownTracer();
            tracer.Trace(ResultFactory.CreateResult(), writer, options);

            writer.Value.MatchSnapshot();
        }

        [Test]
        public void MarkDownTracer_ProfilerResult_FullDetail()
        {
            var writer = new StringResultWriter();

            var options = new TraceOptions()
            {
                TraceDetail = TraceDetail.FullDetail
            };
            var tracer = new MarkDownTracer();
            tracer.Trace(ResultFactory.CreateResult(), writer, options);

            writer.Value.MatchSnapshot();
        }

        [Test]
        public void MarkDownTracer_ProfilerResult_CusotmHeader()
        {
            var writer = new StringResultWriter();

            var tracer = new MarkDownTracer();
            tracer.Trace(ResultFactory.CreateResult(), writer, new TraceOptions { Header = "Custom header" });

            writer.Value.Should().StartWith("# Custom header");
        }

        [Test]
        public void MarkDownTracer_ProfilerResult_DefaultMetrics()
        {
            var options = new TraceOptions();

            var tracer = new MarkDownTracer();
            tracer.Trace(ResultFactory.CreateResult(), new TraceResultWriter(), options);

            options.Metrics.Should().NotBeNull();
        }

        [Test]
        public void MarkDownTracer_ProfilerResult_Empty()
        {
            var writer = new StringResultWriter();

            var tracer = new MarkDownTracer();
            tracer.Trace(new ProfilerResult(), writer, new TraceOptions());

            writer.Value.MatchSnapshot();
        }

        [Test]
        public void MarkDownTracer_BenchmarkResult()
        {
            var writer = new StringResultWriter();

            var tracer = new MarkDownTracer();
            tracer.Trace(ResultFactory.CreateBenchmarkResult(), writer, new TraceOptions());

            writer.Value.MatchSnapshot();
        }

        [Test]
        public void MarkDownTracer_BenchmarkResult_DefaultMetrics()
        {
            var options = new TraceOptions();

            var tracer = new MarkDownTracer();
            tracer.Trace(ResultFactory.CreateBenchmarkResult(), new TraceResultWriter(), options);

            options.Metrics.Should().NotBeNull();
        }

        [Test]
        public void MarkDownTracer_BenchmarkResult_Empty()
        {
            var writer = new StringResultWriter();

            var tracer = new MarkDownTracer();
            tracer.Trace(new BenchmarkResult(new ProfilerSettings()), writer, new TraceOptions());

            writer.Value.MatchSnapshot();
        }

        [Test]
        public void MarkDownTracer_BenchmarkResult_SetHeader()
        {
            var writer = new StringResultWriter();

            var tracer = new MarkDownTracer();
            tracer.Trace(new BenchmarkResult(new ProfilerSettings()), writer, new TraceOptions { Header = "Custom header" });

            writer.Value.Should().StartWith("# Custom header");
        }

        [Test]
        public void MarkDownTracer_BenchmarkResult_Duration()
        {
            var writer = new StringResultWriter();

            var tracer = new MarkDownTracer();
            tracer.Trace(ResultFactory.CreateBenchmarkResult(TimeSpan.FromMinutes(1)), writer, new TraceOptions());

            writer.Value.MatchSnapshot();
        }

        [Test]
        public void MarkDownTracer_BenchmarkResult_Duration_Milliseconds()
        {
            var writer = new StringResultWriter();

            var tracer = new MarkDownTracer();
            tracer.Trace(ResultFactory.CreateBenchmarkResult(TimeSpan.FromMilliseconds(250)), writer, new TraceOptions());

            writer.Value.MatchSnapshot();
        }
    }
}
