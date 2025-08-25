using MeasureMap.ContextStack;

namespace MeasureMap.UnitTest.ContextStack
{
    public class OnStartPipelineContextHandlerTests
    {
        [Test]
        public void OnStartPipelineContextHandler_SetNext()
        {
            var next = new Mock<IContextMiddleware>();

            var handler = new OnStartPipelineContextHandler(0, new ProfilerSettings(), s => new ExecutionContext());
            handler.SetNext(next.Object);

            handler.Run(Mock.Of<ITask>(), new ExecutionContext());

            next.Verify(x => x.Run(It.IsAny<ITask>(), It.IsAny<IExecutionContext>()));
        }

        [Test]
        public void OnStartPipelineContextHandler_SetNext_Next()
        {
            var next = new Mock<IContextMiddleware>();

            var handler = new OnStartPipelineContextHandler(0, new ProfilerSettings(), s => new ExecutionContext());
            handler.SetNext(next.Object);
            handler.SetNext(Mock.Of<IContextMiddleware>());

            next.Verify(x => x.SetNext(It.IsAny<IContextMiddleware>()));
        }

        [Test]
        public void OnStartPipelineContextHandler_Run_UseNewContext()
        {
            var ctx = new ExecutionContext();
            var next = new Mock<IContextMiddleware>();

            var handler = new OnStartPipelineContextHandler(0, new ProfilerSettings(), s => ctx);
            handler.SetNext(next.Object);

            handler.Run(Mock.Of<ITask>(), new ExecutionContext());

            next.Verify(x => x.Run(It.IsAny<ITask>(), ctx));
        }

        [Test]
        public void OnStartPipelineContextHandler_Run_NullContext()
        {
            var next = new Mock<IContextMiddleware>();

            var handler = new OnStartPipelineContextHandler(0, new ProfilerSettings(), s => null);
            handler.SetNext(next.Object);

            var ctx = new ExecutionContext();
            handler.Run(Mock.Of<ITask>(), ctx);

            next.Verify(x => x.Run(It.IsAny<ITask>(), ctx));
        }

        [Test]
        public void OnStartPipelineContextHandler_Run_ThreadNumber()
        {
            var next = new Mock<IContextMiddleware>();

            var handler = new OnStartPipelineContextHandler(10, new ProfilerSettings(), s => null);
            handler.SetNext(next.Object);

            var ctx = new ExecutionContext();

            handler.Run(Mock.Of<ITask>(), ctx);

            ctx.Get<int>("threadnumber").Should().Be(10);
        }
    }
}
