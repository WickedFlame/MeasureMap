using MeasureMap.IterationStack;

namespace MeasureMap.UnitTest.IterationStack
{
    public class OnExecutedIterationHandlerTests
    {
        [Test]
        public void MemoryCollectionIterationHandler_AfterExecution()
        {
            IIterationResult result = null;
            var handler = new OnExecutedIterationHandler(r => { result = r; });
            handler.Run(new ExecutionContext());

            //
            // the action is executed async/in another thread
            System.Threading.Tasks.Task.Delay(50).Wait();

            result.Should().NotBeNull();
        }
    }
}
