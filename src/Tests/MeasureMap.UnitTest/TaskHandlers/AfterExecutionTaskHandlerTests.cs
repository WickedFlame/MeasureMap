using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MeasureMap.UnitTest.TaskHandlers
{
    [TestFixture]
    public class AfterExecutionTaskHandlerTests
    {
        [Test]
        public void AfterExecutionTaskHandler()
        {
            string two = null;
            string one = null;
            
            var task = new SimpleTask(() => two = "before");

            var handler = new PostExecutionTaskHandler(() =>
            {
                one = two;
                two = "task";
            });
            handler.SetNext(new TaskHandler(task));


            var result = handler.Run(new ExecutionContext());

            Assert.That(two == "task");
            Assert.That(one == "before");
        }

        [Test]
        public void AfterExecutionTaskHandler_WithContext()
        {
            string two = null;
            string one = null;

            var task = new SimpleTask(() => two = "before");

            var handler = new PostExecutionTaskHandler(c =>
            {
                one = two;
                two = "task";
            });
            handler.SetNext(new TaskHandler(task));

            var context = new ExecutionContext();

            var result = handler.Run(context);

            Assert.That(two == "task");
            Assert.That(one == "before");
        }
    }
}
