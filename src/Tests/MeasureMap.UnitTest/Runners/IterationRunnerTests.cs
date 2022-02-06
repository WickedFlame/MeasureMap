using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using MeasureMap.Runners;
using NUnit.Framework;

namespace MeasureMap.UnitTest.Runners
{
    public class IterationRunnerTests
    {
        [Test]
        public void IterationRunner_Run()
        {
            var cnt = 0;

            var runner = new IterationRunner();
            runner.Run(new ProfilerSettings{ Iterations = 2 }, new ExecutionContext(), () => cnt += 1);

            cnt.Should().Be(2);
        }

        [Test]
        public void IterationRunner_Run_Context_Iterations()
        {
            var context  = new ExecutionContext();

            var runner = new IterationRunner();
            runner.Run(new ProfilerSettings { Iterations = 2 }, context, () => { });

            context.Get(ContextKeys.Iteration).Should().Be(2);
        }
    }
}
