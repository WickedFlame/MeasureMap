using NUnit.Framework;
using System;
using System.Threading;

namespace MeasureMap.UnitTest.TaskHandlers
{
    [TestFixture]
    public class MemoryCollectionTaskHandlerTests
    {
        [Test]
        public void MemoryCollectionTaskHandler_Initial()
        {
            var task = new Task(() => Thread.Sleep(TimeSpan.FromSeconds(0.5)));

            var handler = new MemoryCollectionTaskHandler();
            handler.SetNext(task);


            var result = handler.Run(new ExecutionContext());

            Assert.That(result.InitialSize > 0);
        }

        [Test]
        public void MemoryCollectionTaskHandler_AfterExecution()
        {
            var task = new Task(() => Thread.Sleep(TimeSpan.FromSeconds(0.5)));

            var handler = new MemoryCollectionTaskHandler();
            handler.SetNext(task);


            var result = handler.Run(new ExecutionContext());

            Assert.That(result.AfterExecution > 0);
        }
    }
}
