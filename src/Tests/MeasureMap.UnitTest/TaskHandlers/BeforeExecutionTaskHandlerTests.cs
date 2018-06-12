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
    public class BeforeExecutionTaskHandlerTests
    {
        [Test]
        public void BeforeExecutionTaskHandler()
        {
            string two = null;
            string one = null;
            
            var task = new Task(() =>
            {
                one = two;
                two = "task";
            });

            var handler = new BeforeExecutionTaskHandler(() => two = "before");
            handler.SetNext(new TaskHandler(task));


            var result = handler.Run(0);

            Assert.That(two == "task");
            Assert.That(one == "before");
        }
    }
}
