using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using MeasureMap.Runners;
using NUnit.Framework;

namespace MeasureMap.UnitTest.Runners
{
    public class SimpleTaskExecutionTests
    {
        [Test]
        public void SimpleTaskExecution_ctor()
        {
            Action action = () => new SimpleTaskExecution();
            action.Should().NotThrow();
        }

        [Test]
        public void SimpleTaskExecution_Execute()
        {
            var called = false;

            var exec = new SimpleTaskExecution();
            exec.Execute(new ExecutionContext(), c => called = true);
            
            called.Should().BeTrue();
        }
    }
}
