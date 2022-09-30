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
        public void MarkDownTracer_ProfilerResult_Detailed()
        {
            var writer = new StringResultWriter();

            var options = new TraceOptions()
            {
                TraceThreadDetail = true
            };

            var tracer = new MarkDownTracer();
            tracer.Trace(ResultFactory.CreateResult(), writer, options);

            writer.Value.MatchSnapshot();
        }

        [Test]
        public void MarkDownTracer_ProfilerResult_TraceFullStack()
        {
            var writer = new StringResultWriter();
            
            var options = new TraceOptions()
            {
                TraceFullStack = true
            };
            var tracer = new MarkDownTracer();
            tracer.Trace(ResultFactory.CreateResult(), writer, options);

            writer.Value.MatchSnapshot();
        }

        [Test]
        public void MarkDownTracer_ProfilerResult_Detailed_TraceFullStack()
        {
            var writer = new StringResultWriter();

            var options = new TraceOptions()
            {
                TraceThreadDetail = true,
                TraceFullStack = true
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
