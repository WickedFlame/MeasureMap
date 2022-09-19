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
            tracer.Trace(ResultFactory.CreateResult(), writer);

            writer.Value.MatchSnapshot();
        }

        [Test]
        public void MarkDownTracer_ProfilerResult_TraceFullStack()
        {
            var writer = new StringResultWriter();

            TraceOptions.Default.TraceFullStack = true;
            var tracer = new MarkDownTracer();
            tracer.Trace(ResultFactory.CreateResult(), writer);

            writer.Value.MatchSnapshot();

            TraceOptions.Default.TraceFullStack = false;
        }

        [Test]
        public void MarkDownTracer_BenchmarkResult()
        {
            var writer = new StringResultWriter();

            var tracer = new MarkDownTracer();
            tracer.Trace(ResultFactory.CreateBenchmarkResult(), writer);

            writer.Value.MatchSnapshot();
        }
    }
}
