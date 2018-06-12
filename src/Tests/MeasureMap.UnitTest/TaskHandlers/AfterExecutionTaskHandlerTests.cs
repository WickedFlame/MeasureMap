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
            
            var task = new Task(() => two = "before");

            var handler = new AfterExecutionTaskHandler(() =>
            {
                one = two;
                two = "task";
            });
            handler.SetNext(new TaskHandler(task));


            var result = handler.Run(0);

            Assert.That(two == "task");
            Assert.That(one == "before");
        }
    }
}
