using System.Diagnostics;

namespace MeasureMap.IntegrationTest
{
    [TestFixture]
    public class ThreadedProfilerTests
    {
        [TestCase(ThreadBehaviour.Thread)]
        [TestCase(ThreadBehaviour.Task)]
        public void ThreadedProfiler_NoThread(ThreadBehaviour behaviour)
        {
            var result = ProfilerSession.StartSession()
                .Task(c => { })
                .SetIterations(10)
                .SetThreadBehaviour(behaviour)
                .RunSession();

            result.Should().BeOfType<ProfilerResult>();

            ((ProfilerResult) result).Count().Should().Be(1);
            result.Iterations.Count().Should().Be(10);
        }

        [TestCase(ThreadBehaviour.Thread)]
        [TestCase(ThreadBehaviour.Task)]
        public void ThreadedProfiler_OneThread(ThreadBehaviour behaviour)
        {
            var result = ProfilerSession.StartSession()
                .Task(c => { })
                .SetIterations(10)
                .SetThreadBehaviour(behaviour)
                .SetThreads(1)
                .RunSession();

            ((ProfilerResult) result).Count().Should().Be(1);
            result.Iterations.Count().Should().Be(10);
        }

        [TestCase(ThreadBehaviour.Thread)]
        [TestCase(ThreadBehaviour.Task)]
        public void ThreadedProfiler_MultipleThreads(ThreadBehaviour behaviour)
        {
            var result = ProfilerSession.StartSession()
                .Task(c => { })
                .SetIterations(10)
                .SetThreadBehaviour(behaviour)
                .SetThreads(10)
                .RunSession();

            ((ProfilerResult) result).Count().Should().Be(10);
            result.Iterations.Count().Should().Be(100);
        }

        [TestCase(ThreadBehaviour.Thread)]
        [TestCase(ThreadBehaviour.Task)]
        public void ThreadedProfiler_MultipleThreads_ReturnValues(ThreadBehaviour behaviour)
        {
            var result = ProfilerSession.StartSession()
                .Task(c => { })
                .SetIterations(10)
                .SetThreadBehaviour(behaviour)
                .SetThreads(10)
                .RunSession();

            ((ProfilerResult) result).All(r => r.AverageTime.Ticks > 0).Should().BeTrue("AverageTime");
            ((ProfilerResult) result).All(r => r.EndSize > 0).Should().BeTrue("EndSize");
            ((ProfilerResult) result).All(r => r.AverageTicks > 0).Should().BeTrue("AverageTicks");
            ((ProfilerResult) result).All(r => r.Fastest != null).Should().BeTrue("Fastest");
            ((ProfilerResult) result).All(r => r.Increase != 0).Should().BeTrue("Increase");
            ((ProfilerResult) result).All(r => r.InitialSize > 0).Should().BeTrue("InitialSize");
            ((ProfilerResult) result).All(r => r.TotalTime.Ticks > 0).Should().BeTrue("TotalTime.Ticks");
        }

        [TestCase(ThreadBehaviour.Thread)]
        [TestCase(ThreadBehaviour.Task)]
        public void ThreadedProfiler_MultipleThreads_Interval_Duration(ThreadBehaviour behaviour)
        {
            var sw = new Stopwatch();
            sw.Start();

            var result = ProfilerSession.StartSession()
                .Task(c => { })
                .SetDuration(TimeSpan.FromSeconds(20))
                .SetThreadBehaviour(behaviour)
                .SetInterval(TimeSpan.FromMilliseconds(50))
                .SetThreads(10)
                .RunSession();


            sw.Stop();
            result.Elapsed().Should().BeGreaterThan(TimeSpan.FromSeconds(20)).And.BeLessThan(sw.Elapsed);
        }

        [TestCase(ThreadBehaviour.Thread)]
        [TestCase(ThreadBehaviour.Task)]
        public void ThreadedProfiler_MultipleThreads_Interval_Iterations(ThreadBehaviour behaviour)
        {
            var sw = new Stopwatch();
            sw.Start();

            var result = ProfilerSession.StartSession()
                .Task(c => { })
                .SetIterations(50)
                .SetThreadBehaviour(behaviour)
                .SetInterval(TimeSpan.FromMilliseconds(50))
                .SetThreads(10)
                .RunSession();

            sw.Stop();
            result.Elapsed().Should().BeLessThan(sw.Elapsed);
        }

        [TestCase(ThreadBehaviour.Thread)]
        [TestCase(ThreadBehaviour.Task)]
        public void ThreadedProfiler_MultipleThreads_Simple_Duration(ThreadBehaviour behaviour)
        {
            var sw = new Stopwatch();
            sw.Start();

            var result = ProfilerSession.StartSession()
                .Task(c => { })
                .SetDuration(TimeSpan.FromSeconds(20))
                .SetThreadBehaviour(behaviour)
                .SetThreads(10)
                .RunSession();

            sw.Stop();
            result.Elapsed().Should().BeGreaterThan(TimeSpan.FromSeconds(20)).And.BeLessThan(sw.Elapsed);
        }

        [TestCase(ThreadBehaviour.Thread)]
        [TestCase(ThreadBehaviour.Task)]
        public void ThreadedProfiler_MultipleThreads_Simple_Iterations(ThreadBehaviour behaviour)
        {
            var sw = new Stopwatch();
            sw.Start();

            var result = ProfilerSession.StartSession()
                .Task(c => { })
                .SetIterations(50)
                .SetThreadBehaviour(behaviour)
                .SetThreads(10)
                .RunSession();

            sw.Stop();
            result.Elapsed().Should().BeLessThan(sw.Elapsed);
        }
    }
}
