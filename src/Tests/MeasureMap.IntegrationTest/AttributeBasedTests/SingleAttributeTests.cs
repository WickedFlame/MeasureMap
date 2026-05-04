using MeasureMap;

namespace MeasureMap.IntegrationTest.AttributeBasedTests
{
    public class SingleAttributeTests
    {
        [Test]
        public void SingleAttribute_Benchmark()
        {
            var runner = new BenchmarkRunner();
            var result = runner.RunSession<SingleAttribute>();

            result.Should().NotBeNull();
            result.Keys.Should().Contain("Test_1");
        }
    }

    public class SingleAttribute
    {
        [Benchmark]
        public void Test_1()
        {
            // Simulate some work
            System.Threading.Thread.Sleep(10);
        }
    }
}
