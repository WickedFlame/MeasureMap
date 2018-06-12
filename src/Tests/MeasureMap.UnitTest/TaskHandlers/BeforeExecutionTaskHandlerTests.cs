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

            var handler = new PreExecutionTaskHandler(() => two = "before");
            handler.SetNext(task);


            var result = handler.Run(new ExecutionContext());

            Assert.That(two == "task");
            Assert.That(one == "before");
        }
    }
}
