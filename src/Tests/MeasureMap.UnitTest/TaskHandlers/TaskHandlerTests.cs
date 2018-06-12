using NUnit.Framework;
using System;
using System.Threading;

namespace MeasureMap.UnitTest.TaskHandlers
{
    [TestFixture]
    public class TaskHandlerTests
    {
        [Test]
        public void TaskHandler_Data()
        {
            var task = new Task(() => Thread.Sleep(TimeSpan.FromSeconds(0.5)));

            var handler = new TaskHandler(task);

            var result = handler.Run(new ExecutionContext());

            Assert.That((int)result.Data == 0);
        }
    }
}
