using MeasureMap;

namespace Measuremap.IntegrationTest.AttributeBasedTests
{
    public class OnEndAttributeTests
    {
        [SetUp]
        public void Setup()
        {
            OnEndAttribute.Runs = 0;
        }
        
        [Test]
        public void SingleAttribute_Benchmark()
        {
            var runner = new BenchmarkRunner();
            runner.RunSession<OnEndAttribute>();

            // 1 for warmup and 1 for testrun
            OnEndAttribute.Runs.Should().Be(2);
        }
    }

    public class OnEndAttribute
    {
        public static int Runs = 0;
        
        [OnEndPipeline]
        public void End()
        {
            Runs++;
        }
        
        [Benchmark]
        public void Test_1()
        {
            // Simulate some work
            System.Threading.Thread.Sleep(10);
        }
    }
}
