using MeasureMap.ContextStack;
using MeasureMap.SessionStack;

namespace MeasureMap.UnitTest.SessionStack
{
    public class MainThreadSessionHandlerTests
    {
        private ISessionExecutor _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new MainThreadSessionHandler();
        }

        [TearDown]
        public void TearDown()
        {
            _handler.Dispose();
        }

        [Test]
        public void MainThreadSessionHandler_StackBuilder()
        {
            _handler.StackBuilder.Should().BeOfType<DefaultContextStackBuilder>();
        }

        [Test]
        public void MainThreadSessionHandler_Execute()
        {
            var result = _handler.Execute(Mock.Of<ITask>(), new ProfilerSettings());

            result.Should().NotBeNull();
        }

        [Test]
        public void MainThreadSessionHandler_Execute_TaskRun()
        {
            var task = new Mock<ITask>();

            _handler.Execute(task.Object, new ProfilerSettings());

            task.Verify(x => x.Run(It.IsAny<IExecutionContext>()));
        }
    }
}
