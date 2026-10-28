
namespace MeasureMap.IntegrationTest.AttributeBasedTests
{
    public class RunWarmupAttributeTests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void RunWarmupAttribute_False_Benchmark()
        {
            var runner = new BenchmarkRunner();
            var result = runner.RunSession<RunWarmupAttributeFalseClass>();

            runner.Settings.RunWarmup.Should().BeFalse();
        }

        [Test]
        public void RunWarmupAttribute_True_Benchmark()
        {
            var runner = new BenchmarkRunner();
            var result = runner.RunSession<RunWarmupAttributeTrueClass>();

            runner.Settings.RunWarmup.Should().BeTrue();
        }

        [Test]
        public void RunWarmupAttribute_Iterations_Benchmark()
        {
            RunWarmupAttributeIterations.Count = 0;

            var runner = new BenchmarkRunner();
            var result = runner.RunSession<RunWarmupAttributeIterations>();

            result.Iterations.Should().Be(10);
            result.First().Iterations.Count().Should().Be(10);
            RunWarmupAttributeIterations.Count.Should().Be(40);
        }
    }

    [RunWarmup(false)]
    public class RunWarmupAttributeFalseClass
    {
        [Benchmark]
        public void Test_1()
        {
            // Simulate some work
            System.Threading.Thread.Sleep(10);
        }
    }

    [RunWarmup(true)]
    public class RunWarmupAttributeTrueClass
    {
        [Benchmark]
        public void Test_1()
        {
            // Simulate some work
            System.Threading.Thread.Sleep(10);
        }
    }

    [Iterations(10)]
    public class RunWarmupAttributeIterations
    {
        public static int Count = 0;

        [Benchmark]
        public void Test_1()
        {
            lock (typeof(RunWarmupAttributeIterations))
            {
                Count++;
            }
        }
    }
}