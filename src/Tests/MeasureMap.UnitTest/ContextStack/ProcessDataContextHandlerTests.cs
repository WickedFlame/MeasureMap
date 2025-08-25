using MeasureMap.ContextStack;

namespace MeasureMap.UnitTest.ContextStack
{
    public class ProcessDataContextHandlerTests
    {
        [Test]
        public void ProcessDataContextHandler_Context()
        {
            var handler = new ProcessDataContextHandler();

            var context = new ExecutionContext();

            handler.Run(Mock.Of<ITask>(), context);

            context.Get<int>(ContextKeys.ThreadId).Should().BeGreaterThan(0);
            context.Get<int>(ContextKeys.ProcessId).Should().BeGreaterThan(0);
        }
    }
}
