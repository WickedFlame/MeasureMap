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
    public class ElapsedTimeSessionHandlerTests
    {
        [Test]
        public void ElapsedTimeSessionHandler_Execute()
        {
            var task = new Task(() => Thread.Sleep(TimeSpan.FromSeconds(0.5)));
            var handler = new ElapsedTimeSessionHandler();

            var result = handler.Execute(task, 10);

            Assert.That(result.Elapsed().Ticks > 0);
        }

        [Test]
        public void ElapsedTimeSessionHandler_Execute_EnsureBaseIsCalled()
        {
            var next = new Mock<ISessionHandler>();
            next.Setup(exp => exp.Execute(It.IsAny<ITask>(), It.IsAny<int>())).Returns(() => new ProfilerResult());

            var task = new Task(() => Thread.Sleep(TimeSpan.FromSeconds(0.5)));
            var handler = new ElapsedTimeSessionHandler();
            handler.SetNext(next.Object);

            var result = handler.Execute(task, 10);

            next.Verify(exp => exp.Execute(task, 10), Times.Once);
        }
    }
}
