using MeasureMap.ContextStack;

namespace MeasureMap.UnitTest.ContextStack
{
    public class OnEndPipelineContextHandlerTests
    {
        [Test]
        public void OnEndPipelineContextHandler_SetNext()
        {
            var next = new Mock<IContextMiddleware>();

            var handler = new OnEndPipelineContextHandler(e => { });
            handler.SetNext(next.Object);

            handler.Run(Mock.Of<ITask>(), new ExecutionContext());

            next.Verify(x => x.Run(It.IsAny<ITask>(), It.IsAny<IExecutionContext>()));
        }

        [Test]
        public void OnEndPipelineContextHandler_SetNext_Next()
        {
            var next = new Mock<IContextMiddleware>();

            var handler = new OnEndPipelineContextHandler(e => { });
            handler.SetNext(next.Object);
            handler.SetNext(Mock.Of<IContextMiddleware>());

            next.Verify(x => x.SetNext(It.IsAny<IContextMiddleware>()));
        }

        [Test]
        public void OnEndPipelineContextHandler_Run()
        {
            var run = false;

            var handler = new OnEndPipelineContextHandler(e => { run = true; });
            handler.Run(Mock.Of<ITask>(), new ExecutionContext());

            run.Should().BeTrue();
        }
    }
}
