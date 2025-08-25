
namespace MeasureMap.UnitTest.TaskHandlers
{
    public class ProcessDataTaskHandlerTests
    {
        [Test]
        public void ProcessDataTaskHandler_Result()
        {
            var handler = new ProcessDataTaskHandler();

            var context = new ExecutionContext();
            context.Set(ContextKeys.ThreadId, 2);
            context.Set(ContextKeys.ProcessId, 2);
            context.Set(ContextKeys.Iteration, 2);
            context.Set(ContextKeys.ThreadNumber, 2);

            var result = handler.Run(context);

            result.ThreadId.Should().Be(2);
            result.ProcessId.Should().Be(2);
            result.Iteration.Should().Be(2);
            result.ThreadNumber.Should().Be(2);
        }
    }
}
