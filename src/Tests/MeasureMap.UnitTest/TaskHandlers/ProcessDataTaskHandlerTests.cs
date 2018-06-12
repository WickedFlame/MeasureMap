using NUnit.Framework;
using System.Diagnostics;

namespace MeasureMap.UnitTest.TaskHandlers
{
    [TestFixture]
    public class ProcessDataTaskHandlerTests
    {
        [Test]
        public void ProcessDataTaskHandler_Context()
        {
            var task = new Task(() =>
            {
                Debug.WriteLine("Process Data");
            });

            var handler = new ProcessDataTaskHandler();
            handler.SetNext(task);

            var context = new ExecutionContext();

            var result = handler.Run(context);

            Assert.That(context.Get<int>(ContextKeys.ThreadId) > 0);
            Assert.That(context.Get<int>(ContextKeys.ProcessId) > 0);
        }

        [Test]
        public void ProcessDataTaskHandler_Result()
        {
            var task = new Task(() =>
            {
                Debug.WriteLine("Process Data");
            });

            var handler = new ProcessDataTaskHandler();
            handler.SetNext(task);

            var context = new ExecutionContext();

            var result = handler.Run(context);

            Assert.That(result.ThreadId > 0);
            Assert.That(result.ProcessId > 0);
        }
    }
}
