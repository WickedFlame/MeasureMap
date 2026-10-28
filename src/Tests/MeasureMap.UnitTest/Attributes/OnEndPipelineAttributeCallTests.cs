using System;

namespace MeasureMap.UnitTest.Attributes
{
    [SingleThreaded]
    public class OnEndPipelineAttributeCallTests
    {
        [Test]
        public void OnEndPipeline_Nothing()
        {
            OnEndPipelineCall_Nothing.Called = false;

            var runner = new BenchmarkRunner();
            runner.RunSession<OnEndPipelineCall_Nothing>();

            OnEndPipelineCall_Nothing.Called.Should().BeTrue();
        }

        [Test]
        public void OnEndPipeline_Duplicate()
        {
            var runner = new BenchmarkRunner();

            // Expect an exception due to multiple methods with OnEndPipelineAttribute
            var action = () => runner.RunSession<OnEndPipelineCall_MultipleMethods>();
            action.Should().Throw<InvalidOperationException>();
        }
    }

    public class OnEndPipelineCall_Nothing
    {
        public static bool Called { get; set; }

        [OnEndPipeline]
        public void OnEnd()
        {
            Called = true;
        }

        [Benchmark]
        public void Parse()
        {
            // do nothing
        }
    }

    public class OnEndPipelineCall_MultipleMethods
    {
        [OnEndPipeline]
        public void OnEnd()
        {
            // just duplicate method
        }

        [OnEndPipeline]
        public void OnEnd2()
        {
            // just duplicate method
        }

        [Benchmark]
        public void Parse()
        {
            // do nothing
        }
    }
}
