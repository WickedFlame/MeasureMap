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
            var session = PerformanceProfileSession.StartSession();

            Assert.IsNotNull(session);
        }

        [Test]
        public void ProfileSession_WithoutSetIterations()
        {
            var session = PerformanceProfileSession.StartSession();

            Assert.AreEqual(1, session.Iterations);
        }

        [Test]
        public void ProfileSession_SetIterations()
        {
            var session = PerformanceProfileSession.StartSession()
                .SetIterations(12);

            Assert.AreEqual(12, session.Iterations);
        }

        [Test]
        public void ProfileSession_AddTask()
        {
            var result = string.Empty;
            var session = PerformanceProfileSession.StartSession()
                .Task(() => result = "passed");

            // TODO: is it neccesary to run the session just to check if a task is set???
            session.RunSession();

            Assert.AreEqual("passed", result);
        }

        [Test]
        public void ProfileSession_RunSessionOnce()
        {
            int count = 0;
            var result = PerformanceProfileSession.StartSession()
                .Task(() => count++)
                .RunSession();

            // the task is rune once more to be able to initialize properly
            Assert.AreEqual(result.Iterations.Count() + 1, count);
        }

        [Test]
        public void ProfileSession_RunSessionMultipleTimes()
        {
            int count = 0;
            var result = PerformanceProfileSession.StartSession()
                .Task(() => count++)
                .SetIterations(20)
                .RunSession();

            // the task is rune once more to be able to initialize properly
            Assert.AreEqual(result.Iterations.Count() + 1, count);
        }

        [Test]
        public void ProfileSession_AverageMillisecond()
        {
            var result = PerformanceProfileSession.StartSession()
                .Task(Task)
                .SetIterations(200)
                .RunSession();

            Assert.IsTrue(result.AverageMilliseconds > 0);
        }

        [Test]
        public void ProfileSession_AverageTicks()
        {
            var result = PerformanceProfileSession.StartSession()
                .Task(Task)
                .SetIterations(200)
                .RunSession();

            Assert.IsTrue(result.AverageMilliseconds > 0);
        }

        [Test]
        public void ProfileSession_TrueCondition()
        {
            PerformanceProfileSession.StartSession()
                .Task(Task)
                .AddCondition(pr => pr.Iterations.Count() == 1)
                .RunSession();
        }

        [Test]
        public void ProfileSession_MultipleTrueCondition()
        {
            PerformanceProfileSession.StartSession()
                .Task(Task)
                .AddCondition(pr => pr.Iterations.Count() == 1)
                .AddCondition(pr => pr.AverageMilliseconds > 0)
                .RunSession();
        }

        [Test]
        public void ProfileSession_FalseCondition()
        {
            Assert.Throws<MeasureMap.AssertionException>(() => PerformanceProfileSession.StartSession()
                .Task(Task)
                .SetIterations(2)
                .AddCondition(pr => pr.Iterations.Count() == 1)
                .RunSession());
        }

        [Test]
        public void ProfileSession_WithoutTask()
        {
            Assert.Throws<ArgumentNullException>(() => PerformanceProfileSession.StartSession()
                .RunSession());
        }

        private void Task()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.001));
        }
    }
}
