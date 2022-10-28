using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FluentAssertions;
using MeasureMap.Runners;
using NUnit.Framework;

namespace MeasureMap.UnitTest.Runners
{
    public class TimedTaskExecutionTests
    {
        [Test]
        public void TimedTaskExecution_ctor()
        {
            Action action = () => new TimedTaskExecution(TimeSpan.FromMilliseconds(1));
            action.Should().NotThrow();
        }

        [Test]
        public void TimedTaskExecution_Execute_Initial()
        {
            var called = false;

            var context = new ExecutionContext();
            var exec = new TimedTaskExecution(TimeSpan.FromSeconds(1));
            exec.Execute(context, c => called = true);

            called.Should().BeTrue();
        }

        [Test]
        public void TimedTaskExecution_Execute_Repeat()
        {
            var called = 0;

            var context = new ExecutionContext();

            var exec = new TimedTaskExecution(TimeSpan.FromMilliseconds(5));
            exec.Execute(context, c => called++);
            exec.Execute(context, c => called++);

            called.Should().Be(2);
        }

        [Test]
        public void TimedTaskExecution_Execute_Delayed()
        {
            var sw = new Stopwatch();
            sw.Start();
            var exec = new TimedTaskExecution(TimeSpan.FromMilliseconds(50));
            exec.Execute(new ExecutionContext(), c => { });
            exec.Execute(new ExecutionContext(), c => { });

            sw.Stop();
            sw.ElapsedMilliseconds.Should().BeGreaterThan(50);
        }

        [Test]
        public void TimedTaskExecution_Execute_Delayed_Wait()
        {
            var called = 0;

            var exec = new TimedTaskExecution(TimeSpan.FromMilliseconds(2));
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                for(var i = 1; i <= 10; i++)
                {
                    exec.Execute(new ExecutionContext(), c => { called = i; });
                }
            });

            System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(200)).Wait();

            called.Should().BeGreaterThan(5);
        }
    }
}
