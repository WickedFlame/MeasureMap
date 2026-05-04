namespace MeasureMap.UnitTest.Attributes
{
    [SingleThreaded]
    public class OnStartPipelineAttributeCallTests
    {
        [Test]
        public void OnStartPipeline_Nothing()
        {
            OnStartPipelineCall_Nothing.Called = false;

            var runner = new BenchmarkRunner();
            runner.RunSession<OnStartPipelineCall_Nothing>();

            OnStartPipelineCall_Nothing.Called.Should().BeTrue();
        }

        [Test]
        public void OnStartPipeline_WithParameter()
        {
            OnStartPipelineCall_WithParameter.Called = false;

            var runner = new BenchmarkRunner();
            runner.RunSession<OnStartPipelineCall_WithParameter>();

            OnStartPipelineCall_WithParameter.Called.Should().BeTrue();
        }

        [Test]
        public void OnStartPipeline_WithReturn()
        {
            OnStartPipelineCall_WithReturn.Called = false;

            var runner = new BenchmarkRunner();
            runner.RunSession<OnStartPipelineCall_WithReturn>();

            OnStartPipelineCall_WithReturn.Called.Should().BeTrue();
        }
    }

    public class OnStartPipelineCall_Nothing
    {
        public static bool Called { get; set; }

        [OnStartPipeline]
        public void Setup()
        {
            Called = true;
        }

        [Benchmark]
        public void Parse()
        {
            // do nothing
        }
    }

    public class OnStartPipelineCall_WithParameter
    {
        public static bool Called { get; set; }

        [OnStartPipeline]
        public void Setup(ProfilerSettings settings)
        {
            Called = true;
        }
        
        [Benchmark]
        public void Parse()
        {
            // do nothing
        }
    }

    public class OnStartPipelineCall_WithReturn
    {
        public static bool Called { get; set; }

        [OnStartPipeline]
        public IExecutionContext Setup(ProfilerSettings settings)
        {
            Called = true;
            return settings.CreateContext();
        }

        [Benchmark]
        public void Parse()
        {
            // do nothing
        }
    }
}
