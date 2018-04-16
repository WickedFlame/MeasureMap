using System;
using NUnit.Framework;
using System.Linq;

namespace MeasureMap.UnitTest
{
    [TestFixture]
    public class ProfileSessionTests
    {
        [Test]
        public void ProfileSession_StartSessionTest()
        {
            var session = ProfilerSession.StartSession();

            Assert.IsNotNull(session);
        }

        [Test]
        public void ProfileSession_WithoutSetIterations()
        {
            var session = ProfilerSession.StartSession();

            Assert.AreEqual(1, session.Iterations);
        }

        [Test]
        public void ProfileSession_SetIterations()
        {
            var session = ProfilerSession.StartSession()
                .SetIterations(12);

            Assert.AreEqual(12, session.Iterations);
        }

        [Test]
        public void ProfileSession_AddTask()
        {
            var result = string.Empty;
            var session = ProfilerSession.StartSession()
                .Task(() => result = "passed");

            // TODO: is it neccesary to run the session just to check if a task is set???
            session.RunSession();

            Assert.AreEqual("passed", result);
        }

        [Test]
        public void ProfileSession_RunSessionOnce()
        {
            int count = 0;
            var result = ProfilerSession.StartSession()
                .Task(() => count++)
                .RunSession();

            // the task is rune once more to be able to initialize properly
            Assert.AreEqual(result.Iterations.Count() + 1, count);
        }

        [Test]
        public void ProfileSession_RunSessionMultipleTimes()
        {
            int count = 0;
            var result = ProfilerSession.StartSession()
                .Task(() => count++)
                .SetIterations(20)
                .RunSession();

            // the task is rune once more to be able to initialize properly
            Assert.AreEqual(result.Iterations.Count() + 1, count);
        }

        [Test]
        public void ProfileSession_AverageMillisecond()
        {
            var result = ProfilerSession.StartSession()
                .Task(Task)
                .SetIterations(200)
                .RunSession();

            Assert.IsTrue(result.AverageMilliseconds > 0);
        }

        [Test]
        public void ProfileSession_AverageTicks()
        {
            var result = ProfilerSession.StartSession()
                .Task(Task)
                .SetIterations(200)
                .RunSession();

            Assert.IsTrue(result.AverageMilliseconds > 0);
        }

        [Test]
        public void ProfileSession_TrueCondition()
        {
            ProfilerSession.StartSession()
                .Task(Task)
                .AddCondition(pr => pr.Iterations.Count() == 1)
                .RunSession();
        }

        [Test]
        public void ProfileSession_MultipleTrueCondition()
        {
            ProfilerSession.StartSession()
                .Task(Task)
                .AddCondition(pr => pr.Iterations.Count() == 1)
                .AddCondition(pr => pr.AverageMilliseconds > 0)
                .RunSession();
        }

        [Test]
        public void ProfileSession_FalseCondition()
        {
            Assert.Throws<MeasureMap.AssertionException>(() => ProfilerSession.StartSession()
                .Task(Task)
                .SetIterations(2)
                .AddCondition(pr => pr.Iterations.Count() == 1)
                .RunSession());
        }

        [Test]
        public void ProfileSession_WithoutTask()
        {
            Assert.Throws<ArgumentNullException>(() => ProfilerSession.StartSession()
                .RunSession());
        }

        [Test]
        public void ProfileSession_Trace()
        {
            var result = ProfilerSession.StartSession()
                .Task(Task)
                .SetIterations(10)
                .RunSession()
                .Trace();

            Assert.That(result.Contains("Duration Total"));
            Assert.That(result.Contains("Average Time"));
            Assert.That(result.Contains("Memory Initial size"));
            Assert.That(result.Contains("Memory End size"));
            Assert.That(result.Contains("Memory Increase"));
        }

        [Test]
        public void ProfileSession_Fastest()
        {
            var result = ProfilerSession.StartSession()
                .Task(i =>
                {
                    if (i != 0)
                    {
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    }
                })
                .SetIterations(10)
                .RunSession();

            Assert.That((int)result.Fastest.Data == 0);
        }

        [Test]
        public void ProfileSession_Slowest()
        {
            var result = ProfilerSession.StartSession()
                .Task(i =>
                {
                    if (i == 9)
                    {
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    }
                })
                .SetIterations(10)
                .RunSession();

            Assert.That((int)result.Slowest.Data == 9);
        }

        [Test]
        public void ProfileSession_OptionsRunner()
        {
            var result = ProfilerSession.StartSession()
                .Task((i, o) =>
                {

                    System.Diagnostics.Trace.WriteLine(o.ThreadId);
                    System.Diagnostics.Trace.WriteLine(o.ProcessId);
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.5));
                })
                .SetIterations(2)
                .SetThreads(10)
                .RunSession();

            Assert.That(result.Iterations.All(i => i.Data.GetType() == typeof(ProfilerOptions)));
        }

        private void Task()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.001));
        }
    }
}
