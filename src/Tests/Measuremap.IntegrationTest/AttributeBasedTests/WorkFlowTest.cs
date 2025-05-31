using MeasureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measuremap.IntegrationTest.AttributeBasedTests
{
    public class WorkFlowTest
    {
        [Test]
        public void WorkflowTest_Benchmark()
        {
            var runner = new BenchmarkRunner();
            var result = runner.RunSession<WorkflowBenchmark>();

            result.Should().NotBeNull();
            result.Keys.Should().Contain("Test_1");
            result.Keys.Should().Contain("Test_2");

            result.Trace();
        }
    }

    //[BenchmarkIterations(10)]
    //[BenchmarkThreads(10)]
    //[BenchmarkDuration(10)]
    public class  WorkflowBenchmark
    {
        //[Measuremap.OnStartPipeline]
        public void Setup()
        {
        }

        //[Measuremap.OnEndPipeline]
        public void End()
        {
        }

        [Benchmark]
        public void Test_1()
        {
            // Simulate some work
            System.Threading.Thread.Sleep(10);
        }

        [Benchmark]
        public void Test_2(IExecutionContext ctx)
        {
            // Simulate some work
            System.Threading.Thread.Sleep(100);
        }
    }
}
