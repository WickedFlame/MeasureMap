using MeasureMap.SessionStack;

namespace MeasureMap.UnitTest.SessionStack
{
    public class BasicSessionHandlerTests
    {
        [Test]
        public void BasicSessionHandler_Execute()
        {
            var handler = new BasicSessionHandler();

            var result = handler.Execute(Mock.Of<ITask>(), new ProfilerSettings());

            result.Should().NotBeNull();
        }

        [Test]
        public void BasicSessionHandler_Execute_TaskRun()
        {
            var handler = new BasicSessionHandler();
            var task = new Mock<ITask>();

            handler.Execute(task.Object, new ProfilerSettings());

            task.Verify(x => x.Run(It.IsAny<IExecutionContext>()));
        }
    }
}
