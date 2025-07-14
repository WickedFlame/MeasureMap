using MeasureMap;

namespace Measuremap.IntegrationTest.AttributeBasedTests
{
    public class ThreadsAttributeTests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void SingleAttribute_Benchmark()
        {
            var runner = new BenchmarkRunner();
            var result = runner.RunSession<ThreadsAttributeClass>();

            result.Single().Count().Should().Be(10);
        }
    }

    [Threads(10)]
    public class ThreadsAttributeClass
    {
        [Benchmark]
        public void Test_1()
        {
            // Simulate some work
            System.Threading.Thread.Sleep(10);
        }
    }
}
