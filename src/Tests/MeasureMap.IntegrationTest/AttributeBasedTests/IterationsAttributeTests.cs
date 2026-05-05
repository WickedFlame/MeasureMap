using MeasureMap;

namespace MeasureMap.IntegrationTest.AttributeBasedTests
{
    public class IterationsAttributeTests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void SingleAttribute_Benchmark()
        {
            var runner = new BenchmarkRunner();
            var result = runner.RunSession<IterationsAttributeClass>();

            result.Iterations.Should().Be(10);
        }
    }

    [Iterations(10)]
    public class IterationsAttributeClass
    {
        [Benchmark]
        public void Test_1()
        {
            // Simulate some work
            System.Threading.Thread.Sleep(10);
        }
    }
}
