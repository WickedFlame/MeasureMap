using MeasureMap.SessionStack;
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
            var runner = new BasicSessionHandler();

            var result = runner.Execute(task, new ProfilerSettings { Iterations = 10 });

            Assert.That(result.Count() == 1);
            Assert.That(result.Iterations.Count() == 10);
        }

        [Test]
        public void MeasureMap_MultyThreadRunner()
        {
            var task = new Task(() => Trace.WriteLine("ThreadRunner test"));
            var runner = new MultiThreadSessionHandler(10);

            var result = runner.Execute(task, new ProfilerSettings { Iterations = 10 });

            Assert.That(result.Count() == 10);
            Assert.That(result.Iterations.Count() == 100);
        }
    }
}
