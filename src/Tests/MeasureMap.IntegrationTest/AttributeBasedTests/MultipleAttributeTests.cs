using MeasureMap;

namespace MeasureMap.IntegrationTest.AttributeBasedTests
{
    public class MultipleAttributeTests
    {
        [Test]
        public void MultipleAttribute_Benchmark()
        {
            var runner = new BenchmarkRunner();
            var result = runner.RunSession<MultipleAttribute>();

            result.Should().NotBeNull();
            result.Keys.Should().Contain(["Test_1", "Test_2", "Test_3"]);
        }
    }

    public class MultipleAttribute
    {
        [Benchmark]
        public void Test_1()
        {
            // Simulate some work
            System.Threading.Thread.Sleep(10);
        }

        [Benchmark]
        public void Test_2()
        {
            // Simulate some work
            System.Threading.Thread.Sleep(10);
        }

        [Benchmark]
        public void Test_3()
        {
            // Simulate some work
            System.Threading.Thread.Sleep(10);
        }
    }
}
