using System;
using System.Text;
using MeasureMap.Tracers;
using Polaroider;

namespace MeasureMap.UnitTest.Tracers
{
    [SingleThreaded]
    public class TracerTests
    {
        [Test]
        public void Tracer_Benchmark_SetTracer_SetWriter()
        {
            var result = ResultFactory.CreateBenchmarkResult();

            var writer = new StringResultWriter();

            result.Trace(new MarkDownTracer(), writer);

            writer.Value.MatchSnapshot();
        }

        [Test]
        public void Tracer_Benchmark_DefaultTracer_SetWriter()
        {
            var result = ResultFactory.CreateBenchmarkResult();

            TraceOptions.Default.Tracer = new MarkDownTracer();

            var writer = new StringResultWriter();

            result.Trace(writer);

            writer.Value.MatchSnapshot();
        }

        [Test]
        public void Tracer_Benchmark_SetTracer_DefaultWriter()
        {
            var result = ResultFactory.CreateBenchmarkResult();

            TraceOptions.Default.ResultWriter = new StringResultWriter();

            result.Trace(new MarkDownTracer());

            ((StringResultWriter)TraceOptions.Default.ResultWriter).Value.MatchSnapshot();

            TraceOptions.Default.ResultWriter = new TraceResultWriter();
        }

        [Test]
        public void Tracer_Benchmark_Options()
        {
            var result = ResultFactory.CreateBenchmarkResult();

            var options = new TraceOptions
            {
                ResultWriter = new StringResultWriter(),
                Tracer = new MarkDownTracer()
            };

            result.Trace(options);

            ((StringResultWriter)options.ResultWriter).Value.MatchSnapshot();
        }





        [Test]
        public void Tracer_Profiler_SetTracer_SetWriter()
        {
            var result = ResultFactory.CreateResult();

            var writer = new StringResultWriter();

            result.Trace(new MarkDownTracer(), writer);

            writer.Value.MatchSnapshot();
        }

        [Test]
        public void Tracer_Profiler_DefaultTracer_SetWriter()
        {
            var result = ResultFactory.CreateResult();
            TraceOptions.Default.Tracer = new MarkDownTracer();

            var writer = new StringResultWriter();

            result.Trace(writer);

            writer.Value.MatchSnapshot();
        }

        [Test]
        public void Tracer_Profiler_SetTracer_DefaultWriter()
        {
            var result = ResultFactory.CreateResult();

            TraceOptions.Default.ResultWriter = new StringResultWriter();

            result.Trace(new MarkDownTracer());

            ((StringResultWriter)TraceOptions.Default.ResultWriter).Value.MatchSnapshot();

            TraceOptions.Default.ResultWriter = new TraceResultWriter();
        }

        [Test]
        public void Tracer_Profiler_Options()
        {
            var result = ResultFactory.CreateResult();

            var options = new TraceOptions
            {
                ResultWriter = new StringResultWriter(),
                Tracer = new MarkDownTracer()
            };

            result.Trace(options);

            ((StringResultWriter)options.ResultWriter).Value.MatchSnapshot();
        }

        [Test]
        public void Tracer_Profiler_Options_Delegate()
        {
            TraceOptions options = null;

            var result = ResultFactory.CreateResult();

            result.Trace(o =>
            {
                o.ResultWriter = new StringResultWriter();
                o.Tracer = new MarkDownTracer();
                options = o;
            });

            ((StringResultWriter)options.ResultWriter).Value.MatchSnapshot();
        }

        [Test]
        public void Tracer_Profiler_Options_Delegate_Simple()
        {
            var result = ResultFactory.CreateResult();

            Action action = () => result.Trace(o => o.TraceDetail = TraceDetail.DetailPerThread);

            action.Should().NotThrow();
        }
    }

    public class StringResultWriter : IResultWriter
    {
        private readonly StringBuilder _stringBuilder = new();

        public string Value => _stringBuilder.ToString();
        
        public void Write(string value)
        {
            _stringBuilder.Append(value);
        }

        public void WriteLine(string value)
        {
            _stringBuilder.AppendLine(value);
        }
    }
}
