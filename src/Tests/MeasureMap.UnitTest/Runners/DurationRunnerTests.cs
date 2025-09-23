using System;
using System.Diagnostics;
using MeasureMap.Runners;

namespace MeasureMap.UnitTest.Runners
{
    public class DurationRunnerTests
    {
        [Test]
        public void DurationRunner_Run()
        {
            var cnt = 0;

            var runner = new DurationRunner();
            runner.Run(new ProfilerSettings { Duration = TimeSpan.FromSeconds(1) }, new ExecutionContext(), c => cnt += 1);

            cnt.Should().BeGreaterThan(100);
        }

        [Test]
        public void DurationRunner_Run_Context_Iterations()
        {
            var context = new ExecutionContext();

            var runner = new DurationRunner();
            runner.Run(new ProfilerSettings { Duration = TimeSpan.FromSeconds(1) }, context, c => { });

            ((int)context.Get(ContextKeys.Iteration)).Should().BeGreaterThan(100);
        }

        [Test]
        public void DurationRunner_Run_Duration()
        {
            var runner = new DurationRunner();
            var duration = TimeSpan.FromSeconds(1);

            var sw = new Stopwatch();
            sw.Start();
            
            runner.Run(new ProfilerSettings { Duration = duration }, new ExecutionContext(), c => { });

            sw.Stop();

            sw.ElapsedTicks.Should().BeGreaterThan(duration.Ticks);
        }
    }
}
