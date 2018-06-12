using NUnit.Framework;
using System.Diagnostics;
using System.Linq;

namespace MeasureMap.UnitTest
{
    [TestFixture]
    public class ThreadRunnerTests
    {
        [Test]
        public void MeasureMap_ThreadRunner()
        {
            var task = new Task(() => Trace.WriteLine("ThreadRunner test"));
            var runner = new ThreadExecutionHandler();
            var handler = new TaskHandler(task);

            var result = runner.Execute(handler, 10);

            Assert.That(result.Count() == 1);
            Assert.That(result.Iterations.Count() == 10);
        }

        [Test]
        public void MeasureMap_MultyThreadRunner()
        {
            var task = new Task(() => Trace.WriteLine("ThreadRunner test"));
            var runner = new MultyThreadExecutionHandler(10);
            var handler = new TaskHandler(task);

            var result = runner.Execute(handler, 10);

            Assert.That(result.Count() == 10);
            Assert.That(result.Iterations.Count() == 100);
        }
    }
}
