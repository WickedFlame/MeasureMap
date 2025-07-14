using MeasureMap;

namespace Measuremap.IntegrationTest.AttributeBasedTests
{
    public class DurationAttributeTests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void SingleAttribute_Benchmark()
        {
            var runner = new BenchmarkRunner();
            var result = runner.RunSession<DurationAttributeClass>();

            result.Duration.Should().BeGreaterThanOrEqualTo(TimeSpan.FromSeconds(10));
            // 1 for warmup and 1 for testrun
            // OnEndAttribute.Runs.Should().Be(2);
        }
    }

    [Duration(10)]
    public class DurationAttributeClass
    {
        [Benchmark]
        public void Test_1()
        {
            // Simulate some work
            System.Threading.Thread.Sleep(10);
        }
    }
}
