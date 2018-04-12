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
    public class ThreadedProfilerTests
    {
        [Test]
        public void ThreadedProfiler_NoThread()
        {
            var result = ProfilerSession.StartSession()
                .Task(i =>
                {
                    Trace.WriteLine($"Iteration {i}");
                })
                .SetIterations(10)
                .RunSessions();

            Assert.That(result.Count() == 1);
            Assert.That(result.Iterations.Count() == 10);
        }

        [Test]
        public void ThreadedProfiler_OneThread()
        {
            var result = ProfilerSession.StartSession()
                .Task(i =>
                {
                    Trace.WriteLine($"Iteration {i}");
                })
                .SetIterations(10)
                .SetThreads(1, false)
                .RunSessions();

            Assert.That(result.Count() == 1);
            Assert.That(result.Iterations.Count() == 10);
        }

        [Test]
        public void ThreadedProfiler_MultipleThreads()
        {
            var result = ProfilerSession.StartSession()
                .Task(i =>
                {
                    Trace.WriteLine($"Iteration {i}");
                })
                .SetIterations(10)
                .SetThreads(10, false)
                .RunSessions();

            Assert.That(result.Count() == 10);
            Assert.That(result.Iterations.Count() == 100);
        }
    }
}
