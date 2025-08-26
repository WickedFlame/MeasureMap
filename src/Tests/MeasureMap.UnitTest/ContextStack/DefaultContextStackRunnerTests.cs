using MeasureMap.ContextStack;

namespace MeasureMap.UnitTest.ContextStack
{
    public class DefaultContextStackRunnerTests
    {
        [Test]
        public void DefaultContextStackRunner_SetNext()
        {
            var next = new Mock<IContextMiddleware>();

            var handler = new DefaultContextStackRunner();
            handler.SetNext(next.Object);

            handler.Run(Mock.Of<ITask>(), new ExecutionContext());

            next.Verify(x => x.Run(It.IsAny<ITask>(), It.IsAny<IExecutionContext>()));
        }

        [Test]
        public void DefaultContextStackRunner_SetNext_Next()
        {
            var next = new Mock<IContextMiddleware>();

            var handler = new DefaultContextStackRunner();
            handler.SetNext(next.Object);
            handler.SetNext(Mock.Of<IContextMiddleware>());
            
            next.Verify(x => x.SetNext(It.IsAny<IContextMiddleware>()));
        }
    }
}
