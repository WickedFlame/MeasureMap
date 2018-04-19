using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasureMap.UnitTest
{
    [TestFixture]
    public class ThreadRunnerTests
    {
        [Test]
        public void MeasureMap_ThreadRunner()
        {
            var task = new TaskRunner(() => Trace.WriteLine("ThreadRunner test"));
            var runner = new ThreadRunner();

            var result = runner.Execute(task, 10);

            Assert.That(result.Count() == 1);
            Assert.That(result.Iterations.Count() == 10);
        }

        [Test]
        public void MeasureMap_MultyThreadRunner()
        {
            var task = new TaskRunner(() => Trace.WriteLine("ThreadRunner test"));
            var runner = new MultyThreadRunner(10, true);

            var result = runner.Execute(task, 10);

            Assert.That(result.Count() == 10);
            Assert.That(result.Iterations.Count() == 100);
        }
    }
}
