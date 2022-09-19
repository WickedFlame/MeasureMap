using System.Text;
using MeasureMap.Tracers;
using NUnit.Framework;
using Polaroider;

namespace MeasureMap.UnitTest.Tracers
{
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
