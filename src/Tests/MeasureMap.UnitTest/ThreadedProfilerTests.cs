using FluentAssertions;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;

namespace MeasureMap.UnitTest
{
    [TestFixture]
    public class ThreadedProfilerTests
    {
        [TestCase(ThreadBehaviour.Thread)]
        [TestCase(ThreadBehaviour.Task)]
        public void ThreadedProfiler_NoThread(ThreadBehaviour behaviour)
        {
            var result = ProfilerSession.StartSession()
                .Task(c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    Trace.WriteLine($"Iteration {(int)i}");
                })
                .SetIterations(10)
                .SetThreadBehaviour(behaviour)
                .RunSession();

            result.Should().BeOfType<ProfilerResult>();

            Assert.That(((ProfilerResult)result).Count() == 1);
            Assert.That(result.Iterations.Count() == 10);
        }

        [TestCase(ThreadBehaviour.Thread)]
        [TestCase(ThreadBehaviour.Task)]
        public void ThreadedProfiler_OneThread(ThreadBehaviour behaviour)
        {
            var result = ProfilerSession.StartSession()
                .Task(c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    Trace.WriteLine($"Iteration {(int)i}");
                })
                .SetIterations(10)
                .SetThreadBehaviour(behaviour)
                .SetThreads(1)
                .RunSession();

            Assert.That(((ProfilerResult)result).Count() == 1);
            Assert.That(result.Iterations.Count() == 10);
        }

        [TestCase(ThreadBehaviour.Thread)]
        [TestCase(ThreadBehaviour.Task)]
        public void ThreadedProfiler_MultipleThreads(ThreadBehaviour behaviour)
        {
            var result = ProfilerSession.StartSession()
                .Task(c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    Trace.WriteLine($"Iteration {(int)i}");
                })
                .SetIterations(10)
                .SetThreadBehaviour(behaviour)
                .SetThreads(10)
                .RunSession();

            Assert.That(((ProfilerResult)result).Count() == 10);
            Assert.That(result.Iterations.Count() == 100);
        }

        [TestCase(ThreadBehaviour.Thread)]
        [TestCase(ThreadBehaviour.Task)]
        public void ThreadedProfiler_MultipleThreads_ReturnValues(ThreadBehaviour behaviour)
        {
            var result = ProfilerSession.StartSession()
                .Task(c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    Trace.WriteLine($"Iteration {(int)i}");
                })
                .SetIterations(10)
                .SetThreadBehaviour(behaviour)
                .SetThreads(10)
                .RunSession();

            Assert.That(((ProfilerResult)result).All(r => r.AverageTime.Ticks > 0), () => "AverageTime");
            Assert.That(((ProfilerResult)result).All(r => r.EndSize > 0), () => "EndSize");
            Assert.That(((ProfilerResult)result).All(r => r.AverageTicks > 0), () => "AverageTicks");
            Assert.That(((ProfilerResult)result).All(r => r.Fastest != null), () => "Fastest");
            Assert.That(((ProfilerResult)result).All(r => r.Increase != 0), () => "Increase");
            Assert.That(((ProfilerResult)result).All(r => r.InitialSize > 0), () => "InitialSize");
            Assert.That(((ProfilerResult)result).All(r => r.TotalTime.Ticks > 0), () => "TotalTime.Ticks");
        }


        [TestCase(ThreadBehaviour.Thread)]
        [TestCase(ThreadBehaviour.Task)]
        public void ThreadedProfiler_MultipleThreads_Interval_Duration(ThreadBehaviour behaviour)
        {
            var sw = new Stopwatch();
            sw.Start();

            var result = ProfilerSession.StartSession()
                .Task(c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    Trace.WriteLine($"Iteration {i}");
                })
                .SetDuration(TimeSpan.FromSeconds(20))
                .SetThreadBehaviour(behaviour)
                .SetInterval(TimeSpan.FromMilliseconds(50))
                .SetThreads(10)
                .RunSession();


            sw.Stop();
            Debug.WriteLine($"Stopwatch: {sw.Elapsed}");
            result.Trace();

            result.Elapsed().Should().BeGreaterThan(TimeSpan.FromSeconds(20)).And.BeLessThan(sw.Elapsed);
        }

        [TestCase(ThreadBehaviour.Thread)]
        [TestCase(ThreadBehaviour.Task)]
        public void ThreadedProfiler_MultipleThreads_Interval_Iterations(ThreadBehaviour behaviour)
        {
            var sw = new Stopwatch();
            sw.Start();

            var result = ProfilerSession.StartSession()
                .Task(c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    Trace.WriteLine($"Iteration {i}");
                })
                .SetIterations(50)
                .SetThreadBehaviour(behaviour)
                .SetInterval(TimeSpan.FromMilliseconds(50))
                .SetThreads(10)
                .RunSession();

            sw.Stop();
            Debug.WriteLine($"Stopwatch: {sw.Elapsed}");
            result.Trace();

            result.Elapsed().Should().BeLessThan(sw.Elapsed);
        }

        [TestCase(ThreadBehaviour.Thread)]
        [TestCase(ThreadBehaviour.Task)]
        public void ThreadedProfiler_MultipleThreads_Simple_Duration(ThreadBehaviour behaviour)
        {
            var sw = new Stopwatch();
            sw.Start();

            var result = ProfilerSession.StartSession()
                .Task(c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    Trace.WriteLine($"Iteration {i}");
                })
                .SetDuration(TimeSpan.FromSeconds(20))
                .SetThreadBehaviour(behaviour)
                .SetThreads(10)
                .RunSession();


            sw.Stop();
            Debug.WriteLine($"Stopwatch: {sw.Elapsed}");
            result.Trace();

            result.Elapsed().Should().BeGreaterThan(TimeSpan.FromSeconds(20)).And.BeLessThan(sw.Elapsed);
        }

        [TestCase(ThreadBehaviour.Thread)]
        [TestCase(ThreadBehaviour.Task)]
        public void ThreadedProfiler_MultipleThreads_Simple_Iterations(ThreadBehaviour behaviour)
        {
            var sw = new Stopwatch();
            sw.Start();

            var result = ProfilerSession.StartSession()
                .Task(c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    Trace.WriteLine($"Iteration {i}");
                })
                .SetIterations(50)
                .SetThreadBehaviour(behaviour)
                .SetThreads(10)
                .RunSession();

            sw.Stop();
            Debug.WriteLine($"Stopwatch: {sw.Elapsed}");
            result.Trace();

            result.Elapsed().Should().BeLessThan(sw.Elapsed);
        }
    }
}
