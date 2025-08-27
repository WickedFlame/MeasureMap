using MeasureMap.Runners;

namespace MeasureMap.UnitTest.Runners
{
    public class IterationRunnerTests
    {
        [Test]
        public void IterationRunner_Run()
        {
            var cnt = 0;

            var runner = new IterationRunner();
            runner.Run(new ProfilerSettings{ Iterations = 2 }, new ExecutionContext(), c => cnt += 1);

            cnt.Should().Be(2);
        }

        [Test]
        public void IterationRunner_Run_Context_Iterations()
        {
            var context  = new ExecutionContext();

            var runner = new IterationRunner();
            runner.Run(new ProfilerSettings { Iterations = 2 }, context, c => { });

            context.Get(ContextKeys.Iteration).Should().Be(2);
        }
    }
}
