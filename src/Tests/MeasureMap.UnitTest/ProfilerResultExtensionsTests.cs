using FluentAssertions;
using MeasureMap.Tracers;
using MeasureMap.UnitTest.Tracers;
using Moq;
using NUnit.Framework;
using Polaroider;

namespace MeasureMap.UnitTest
{
    public class ProfilerResultExtensionsTests
    {
        [Test]
        public void ProfilerResultExtensions_Trace()
        {
            var tracer = new Mock<ITracer>();
            TraceOptions.Default.Tracer = tracer.Object;
            TraceOptions.Default.ResultWriter = new Mock<IResultWriter>().Object;

            var result = new ProfilerResult();
            result.Trace();

            tracer.Verify(x => x.Trace(result, TraceOptions.Default.ResultWriter, It.IsAny<TraceOptions>()));
        }

        [Test]
        public void ProfilerResultExtensions_Trace_AllParameters()
        {
            var tracer = new Mock<ITracer>();
            var writer = new Mock<IResultWriter>();
            var options = new TraceOptions();

            var result = new ProfilerResult();
            result.Trace(tracer.Object, writer.Object, options);

            tracer.Verify(x => x.Trace(result, writer.Object, options));
        }

        [Test]
        public void ProfilerResultExtensions_Trace_Tracer()
        {
            var tracer = new Mock<ITracer>();
            TraceOptions.Default.Tracer = tracer.Object;
            TraceOptions.Default.ResultWriter = new Mock<IResultWriter>().Object;

            var result = new ProfilerResult();
            result.Trace(tracer.Object);

            tracer.Verify(x => x.Trace(result, TraceOptions.Default.ResultWriter, It.IsAny<TraceOptions>()));
        }

        [Test]
        public void ProfilerResultExtensions_Trace_Writer()
        {
            var tracer = new Mock<ITracer>();
            var writer = new Mock<IResultWriter>();

            TraceOptions.Default.Tracer = tracer.Object;
            TraceOptions.Default.ResultWriter = writer.Object;

            var result = new ProfilerResult();
            result.Trace(writer.Object);

            tracer.Verify(x => x.Trace(result, writer.Object, It.IsAny<TraceOptions>()));
        }

        [Test]
        public void ProfilerResultExtensions_Trace_Tracer_Writer()
        {
            var tracer = new Mock<ITracer>();
            var writer = new Mock<IResultWriter>();

            TraceOptions.Default.Tracer = tracer.Object;
            TraceOptions.Default.ResultWriter = writer.Object;

            var result = new ProfilerResult();
            result.Trace(tracer.Object, writer.Object);

            tracer.Verify(x => x.Trace(result, writer.Object, It.IsAny<TraceOptions>()));
        }

        [Test]
        public void ProfilerResultExtensions_Trace_SingleOptions()
        {
            var tracer = new Mock<ITracer>();
            var options = new TraceOptions
            {
                ResultWriter = new Mock<IResultWriter>().Object,
                Tracer = tracer.Object
            };

            var result = new ProfilerResult();
            result.Trace(options);

            tracer.Verify(x => x.Trace(result, options.ResultWriter, options));
        }

        [Test]
        public void ProfilerResultExtensions_Trace_SingleOptions_DefaultMetrics()
        {
            var options = new TraceOptions();

            new ProfilerResult().Trace(options);

            options.Metrics.Should().NotBeNull();
        }

        [Test]
        public void ProfilerResultExtensions_Trace_DefaultMetrics()
        {
            var options = new TraceOptions();

            new ProfilerResult().Trace(new MarkDownTracer(), new TraceResultWriter(), options);

            options.Metrics.Should().NotBeNull();
        }

        [Test]
        public void ProfilerResultExtensions_Trace_Empty()
        {
            var options = new TraceOptions
            {
                ResultWriter = new StringResultWriter()
            };

            new ProfilerResult().Trace(options);

            ((StringResultWriter)options.ResultWriter).Value.MatchSnapshot();
        }
    }
}
