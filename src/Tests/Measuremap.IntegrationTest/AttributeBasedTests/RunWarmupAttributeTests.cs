
namespace MeasureMap.IntegrationTest.AttributeBasedTests
{
    public class RunWarmupAttributeTests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void RunWarmupAttribute_Benchmark()
        {
            var runner = new BenchmarkRunner();
            var result = runner.RunSession<RunWarmupAttributeClass>();

            runner.Settings.RunWarmup.Should().BeFalse();
        }
    }

    [RunWarmup(false)]
    public class RunWarmupAttributeClass
    {
        [Benchmark]
        public void Test_1()
        {
            // Simulate some work
            System.Threading.Thread.Sleep(10);
        }
    }
}