using MeasureMap;

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
    [Duration(10)]
    //[DisableWarmup]
    public class  WorkflowBenchmark
    {
        [OnStartPipeline]
        public void Setup()
        {
        }

        [OnEndPipeline]
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
