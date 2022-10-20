using NUnit.Framework;
using MeasureMap.Tracers.Metrics;
using TraceOptions = MeasureMap.Tracers.TraceOptions;
using Polaroider;
using MeasureMap.Tracers;

namespace MeasureMap.UnitTest.Tracers
{
    public class MetricsTests
    {
        [Test]
        public void Metrics_Benchmark()
        {
            var benchmarks = ResultFactory.CreateBenchmarkResult();
            
            var options = new TraceOptions
            {
                ResultWriter = new StringResultWriter(),
                Metrics = BenchmarkTraceMetrics.GetDefaultTraceMetrics()
            };

            benchmarks.Trace(options);

            ((StringResultWriter)options.ResultWriter).Value.MatchSnapshot();
        }

        [Test]
        public void Metrics_Profiler_Detailed()
        {
            var options = new TraceOptions
            {
                ResultWriter = new StringResultWriter(),
                TraceDetail = TraceDetail.DetailPerThread,
                Metrics = ProfilerTraceMetrics.GetDefaultTraceMetrics()
            };

            var result = ResultFactory.CreateResult();

            result.Trace(options);

            ((StringResultWriter)options.ResultWriter).Value.MatchSnapshot();
        }

        [Test]
        public void Metrics_Profiler_Detailed_FullStack()
        {
            var options = new TraceOptions
            {
                ResultWriter = new StringResultWriter(),
                TraceDetail = TraceDetail.FullDetail,
                Metrics = ProfilerTraceMetrics.GetDefaultTraceMetrics()
            };

            var result = ResultFactory.CreateResult();

            result.Trace(options);

            ((StringResultWriter)options.ResultWriter).Value.MatchSnapshot();
        }
    }
}
