using MeasureMap.Tracers;
using MeasureMap.UnitTest.Tracers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MeasureMap.Tracers.Metrics;
using TraceOptions = MeasureMap.Tracers.TraceOptions;
using Microsoft.VisualBasic;
using Polaroider;

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
                TraceThreadDetail = true,
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
                TraceThreadDetail = true,
                TraceFullStack = true,
                Metrics = ProfilerTraceMetrics.GetDefaultTraceMetrics()
            };

            var result = ResultFactory.CreateResult();

            result.Trace(options);

            ((StringResultWriter)options.ResultWriter).Value.MatchSnapshot();
        }
    }
}
