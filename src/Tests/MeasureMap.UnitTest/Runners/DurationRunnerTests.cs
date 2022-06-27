using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using MeasureMap.Runners;
using NUnit.Framework;

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
    }
}
