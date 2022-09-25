using FluentAssertions;
using MeasureMap.Tracers;
using Moq;
using NUnit.Framework;

namespace MeasureMap.UnitTest
{
    public class BenchmarkResultExtensionsTests
    {
        [Test]
        public void BenchmarkResultExtensions_Trace()
        {
            var tracer = new Mock<ITracer>();
            TraceOptions.Default.Tracer = tracer.Object;
            TraceOptions.Default.ResultWriter = new Mock<IResultWriter>().Object;

            var result = new BenchmarkResult(1);
            result.Trace();

            tracer.Verify(x => x.Trace(result, TraceOptions.Default.ResultWriter, It.IsAny<TraceOptions>()));
        }

        [Test]
        public void BenchmarkResultExtensions_Trace_AllParameters()
        {
            var tracer = new Mock<ITracer>();
            var writer = new Mock<IResultWriter>();
            var options = new TraceOptions();

            var result = new BenchmarkResult(1);
            result.Trace(tracer.Object, writer.Object, options);

            tracer.Verify(x => x.Trace(result, writer.Object, options));
        }

        [Test]
        public void BenchmarkResultExtensions_Trace_SingleOptions()
        {
            var tracer = new Mock<ITracer>();
            var options = new TraceOptions
            {
                ResultWriter = new Mock<IResultWriter>().Object,
                Tracer = tracer.Object
            };

            var result = new BenchmarkResult(1);
            result.Trace(options);

            tracer.Verify(x => x.Trace(result, options.ResultWriter, options));
        }

        [Test]
        public void BenchmarkResultExtensions_Trace_SingleOptions_DefaultMetrics()
        {
            var options = new TraceOptions();

            new BenchmarkResult(1).Trace(options);

            options.Metrics.Should().NotBeNull();
        }

        [Test]
        public void BenchmarkResultExtensions_Trace_DefaultMetrics()
        {
            var options = new TraceOptions();

            new BenchmarkResult(1).Trace(new MarkDownTracer(), new TraceResultWriter(), options);

            options.Metrics.Should().NotBeNull();
        }
    }
}
