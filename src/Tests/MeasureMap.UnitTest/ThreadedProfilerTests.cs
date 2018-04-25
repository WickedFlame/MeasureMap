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
                    Trace.WriteLine($"Iteration {(int)i}");
                })
                .SetIterations(10)
                .RunSession();

            Assert.IsInstanceOf<ProfilerResult>(result);

            Assert.That(((ProfilerResult)result).Count() == 1);
            Assert.That(result.Iterations.Count() == 10);
        }

        [Test]
        public void ThreadedProfiler_OneThread()
        {
            var result = ProfilerSession.StartSession()
                .Task(i =>
                {
                    Trace.WriteLine($"Iteration {(int)i}");
                })
                .SetIterations(10)
                .SetThreads(1)
                .RunSession();

            Assert.That(((ProfilerResult)result).Count() == 1);
            Assert.That(result.Iterations.Count() == 10);
        }

        [Test]
        public void ThreadedProfiler_MultipleThreads()
        {
            var result = ProfilerSession.StartSession()
                .Task(i =>
                {
                    Trace.WriteLine($"Iteration {(int)i}");
                })
                .SetIterations(10)
                .SetThreads(10)
                .RunSession();

            Assert.That(((ProfilerResult)result).Count() == 10);
            Assert.That(result.Iterations.Count() == 100);
        }

        [Test]
        public void ThreadedProfiler_MultipleThreads_ReturnValues()
        {
            var result = ProfilerSession.StartSession()
                .Task(i =>
                {
                    Trace.WriteLine($"Iteration {(int)i}");
                })
                .SetIterations(10)
                .SetThreads(10)
                .RunSession();

            Assert.That(((ProfilerResult)result).All(r => r.AverageTime.Ticks > 0), () => "AverageTime");
            Assert.That(((ProfilerResult)result).All(r => r.EndSize > 0), () => "EndSize");
            Assert.That(((ProfilerResult)result).All(r => r.AverageTicks > 0), () => "AverageTicks");
            // milliseconds can be 0 when ticks are too low
            //Assert.That(((ProfilerResultCollection)result).Any(r => r.AverageMilliseconds != 0), () => "AverageMilliseconds");
            Assert.That(((ProfilerResult)result).All(r => r.Fastest != null), () => "Fastest");
            Assert.That(((ProfilerResult)result).All(r => r.Increase != 0), () => "Increase");
            Assert.That(((ProfilerResult)result).All(r => r.InitialSize > 0), () => "InitialSize");
            Assert.That(((ProfilerResult)result).All(r => r.TotalTime.Ticks > 0), () => "TotalTime.Ticks");
        }
    }
}
