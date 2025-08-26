using MeasureMap.ContextStack;
using MeasureMap.SessionStack;

namespace MeasureMap.UnitTest.SessionStack
{
    public class MultiThreadSessionHandlerTests
    {
        private ISessionExecutor _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new MultiThreadSessionHandler(1);
        }

        [TearDown]
        public void TearDown()
        {
            _handler.Dispose();
        }

        [Test]
        public void MultiThreadSessionHandler_StackBuilder()
        {
            _handler.StackBuilder.Should().BeOfType<DefaultContextStackBuilder>();
        }

        [Test]
        public void MultiThreadSessionHandler_Execute()
        {
            var result = _handler.Execute(Mock.Of<ITask>(), new ProfilerSettings());

            result.Should().NotBeNull();
        }

        [Test]
        public void MultiThreadSessionHandler_Execute_TaskRun()
        {
            var task = new Mock<ITask>();

            _handler.Execute(task.Object, new ProfilerSettings());

            task.Verify(x => x.Run(It.IsAny<IExecutionContext>()));
        }

        [Test]
        public void MultiThreadSessionHandler_Execute_TaskRun_MultipleThreads()
        {
            _handler = new MultiThreadSessionHandler(3);
            var task = new Mock<ITask>();

            _handler.Execute(task.Object, new ProfilerSettings());

            task.Verify(x => x.Run(It.IsAny<IExecutionContext>()), Times.Exactly(3));
        }
    }
}
