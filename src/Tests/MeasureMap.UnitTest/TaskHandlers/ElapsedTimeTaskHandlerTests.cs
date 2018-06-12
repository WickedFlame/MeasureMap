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
    public class ElapsedTimeTaskHandlerTests
    {
        [Test]
        public void ElapsedTimeTaskHandler_Ticks()
        {
            var task = new Task(() => Thread.Sleep(TimeSpan.FromSeconds(0.5)));

            var handler = new ElapsedTimeTaskHandler();
            handler.SetNext(new TaskHandler(task));


            var result = handler.Run(new ExecutionContext());

            Assert.That(result.Ticks > 0);
        }

        [Test]
        public void ElapsedTimeTaskHandler_Duration()
        {
            var task = new Task(() => Thread.Sleep(TimeSpan.FromSeconds(0.5)));

            var handler = new ElapsedTimeTaskHandler();
            handler.SetNext(new TaskHandler(task));


            var result = handler.Run(new ExecutionContext());

            Assert.That(result.Duration.Ticks > 0);
        }
    }
}
