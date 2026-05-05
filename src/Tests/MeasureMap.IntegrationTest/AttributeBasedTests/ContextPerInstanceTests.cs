namespace MeasureMap.IntegrationTest.AttributeBasedTests
{
    public class ContextPerInstanceTests
    {
        [Test]
        public void ContextPerInstance_Benchmark()
        {
            var runner = new BenchmarkRunner();
            var act = () => runner.RunSession<ContextPerInstance>();

            act.Should().NotThrow();
        }
    }

    [Iterations(5)]
    [Threads(3)]
    [RunWarmup(false)]
    public class ContextPerInstance
    {
        private string _value;

        [OnStartPipeline]
        public void Setup()
        {
            if (!string.IsNullOrEmpty(_value))
            {
                throw new Exception("Instance is already called");
            }

            _value = "set";
        }

        [Benchmark]
        public void Test_1()
        {
            // Simulate some work
        }

        [Benchmark]
        public void Test_2()
        {
            // Simulate some work
        }
    }
}
