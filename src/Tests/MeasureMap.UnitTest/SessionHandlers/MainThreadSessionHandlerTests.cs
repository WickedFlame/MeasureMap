using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace MeasureMap.UnitTest.SessionHandlers
{
    public class MainThreadSessionHandlerTests
    {
        [Test]
        public void MainThreadSessionHandler_Execute()
        {
            var handler = new MainThreadSessionHandler();

            var result = handler.Execute(new Mock<ITask>().Object, new ProfilerSettings());

            result.Should().NotBeNull();
        }

        [Test]
        public void MainThreadSessionHandler_Execute_TaskRun()
        {
            var handler = new MainThreadSessionHandler();
            var task = new Mock<ITask>();

            handler.Execute(task.Object, new ProfilerSettings());

            task.Verify(x => x.Run(It.IsAny<IExecutionContext>()));
        }
    }
}
