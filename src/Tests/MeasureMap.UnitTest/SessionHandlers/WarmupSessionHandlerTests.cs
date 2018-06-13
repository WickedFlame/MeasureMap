using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MeasureMap.UnitTest.SessionHandlers
{
    [TestFixture]
    public class WarmupSessionHandlerTests
    {
        [Test]
        public void WarmupSessionHandler_Execute()
        {
            var task = new Task(() => Thread.Sleep(TimeSpan.FromSeconds(0.5)));
            var handler = new WarmupSessionHandler();

            var result = handler.Execute(task, 10);

            Assert.That(result.Warmup().Ticks > 0);
        }

        [Test]
        public void ElapsedTimeSessionHandler_Execute_EnsureBaseIsCalled()
        {
            var next = new Mock<ISessionHandler>();
            next.Setup(exp => exp.Execute(It.IsAny<ITask>(), It.IsAny<int>())).Returns(() => new ProfilerResult());

            var task = new Task(() => Thread.Sleep(TimeSpan.FromSeconds(0.5)));
            var handler = new WarmupSessionHandler();
            handler.SetNext(next.Object);

            var result = handler.Execute(task, 10);

            next.Verify(exp => exp.Execute(task, 10), Times.Once);
        }

        [Test]
        public void ElapsedTimeSessionHandler_Execute_EnsureTaskIsRun()
        {
            var task = new Mock<ITask>();
            var handler = new WarmupSessionHandler();

            var result = handler.Execute(task.Object, 10);

            task.Verify(exp => exp.Run(It.IsAny<IExecutionContext>()), Times.Once);
        }
    }
}
