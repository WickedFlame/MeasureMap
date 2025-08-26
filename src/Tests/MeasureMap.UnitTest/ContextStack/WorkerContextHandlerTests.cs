using MeasureMap.ContextStack;
using System;

namespace MeasureMap.UnitTest.ContextStack
{
    public class WorkerContextHandlerTests
    {
        [Test]
        public void WorkerContextHandler_SetNext()
        {
            var handler = new WorkerContextHandler();
            var action = () => handler.SetNext(Mock.Of<IContextMiddleware>());

            action.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void WorkerContextHandler_Run_ExecutesAction()
        {
            var task = new Mock<ITask>();

            var handler = new WorkerContextHandler();
            handler.Run(task.Object, new ExecutionContext());

            task.Verify(x=> x.Run(It.IsAny<IExecutionContext>()));
        }
    }
}
