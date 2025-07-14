using MeasureMap;

namespace Measuremap.IntegrationTest.AttributeBasedTests
{
    public class OnStartAttributeTests
    {
        [SetUp]
        public void Setup()
        {
            OnStartAttribute.Runs = 0;
        }
        
        [Test]
        public void SingleAttribute_Benchmark()
        {
            var runner = new BenchmarkRunner();
            runner.RunSession<OnStartAttribute>();

            // 1 for warmup and 1 for testrun
            OnStartAttribute.Runs.Should().Be(2);
        }
    }

    public class OnStartAttribute
    {
        public static int Runs = 0;
        
        [OnStartPipeline]
        public void Start()
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
