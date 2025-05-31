using MeasureMap;

namespace Measuremap.IntegrationTest.AttributeBasedTests
{
    public class AttributeWithContextTests
    {
        [Test]
        public void AttributeWithContext_Benchmark()
        {
            var runner = new BenchmarkRunner();
            runner.RunSession<AttributeWithContext>();

            AttributeWithContext.ContextSet.Should().BeTrue();
        }
    }

    public class AttributeWithContext
    {
        public static bool ContextSet { get; private set; }

        [Benchmark]
        public void Test_1(IExecutionContext ctx)
        {
            // Simulate some work
            ContextSet = ctx != null;
        }
    }
}
