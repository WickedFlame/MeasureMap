using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MeasureMap.Runners;

namespace MeasureMap.UnitTest.Runners
{
    public class TimedTaskExecutionBenchmark
    {
        [Test]
        public void TimedTaskExecution_Benchmark()
        {
            TimedTaskExecution execution = null;
            var context = new ExecutionContext();

            var runner = new BenchmarkRunner();
            runner.SetIterations(10);
            runner.Task("1 ms delay", c =>
            {
                execution.Execute(context, d => { });
            }).Setup(() =>
            {
                var time = TimeSpan.FromMilliseconds(1);
                execution = new TimedTaskExecution(time);
            });

            runner.Task("10 ms delay", c =>
            {
                execution.Execute(context, d => { });
            }).Setup(() =>
            {
                var time = TimeSpan.FromMilliseconds(10);
                execution = new TimedTaskExecution(time);
            });

            runner.Task("100 ms delay", c =>
            {
                execution.Execute(context, d => { });
            }).Setup(() =>
            {
                var time = TimeSpan.FromMilliseconds(100);
                execution = new TimedTaskExecution(time);
            });

            var result = runner.RunSessions();
            result.Trace();

            result.Should().HaveCount(3);
        }
    }
}
