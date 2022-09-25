using System;
using NUnit.Framework;
using System.Linq;
using FluentAssertions;

namespace MeasureMap.UnitTest
{
    [TestFixture]
    public class AcceptanceTests
    {
        [Test]
        public void Acceptance_AddTask()
        {
            var result = string.Empty;
            var session = ProfilerSession.StartSession()
                .Task(() => result = "passed");

            // TODO: is it neccesary to run the session just to check if a task is set???
            session.RunSession();

            result.Should().Be("passed");
        }

        [Test]
        public void Acceptance_RunSessionOnce()
        {
            int count = 0;
            var result = ProfilerSession.StartSession()
                .Task(() => count++)
                .RunSession();

            // the task is rune once more to be able to initialize properly
            result.Iterations.Count().Should().Be(count - 1);
        }

        [Test]
        public void Acceptance_RunSessionMultipleTimes()
        {
            int count = 0;
            var result = ProfilerSession.StartSession()
                .Task(() => count++)
                .SetIterations(20)
                .RunSession();

            // the task is rune once more to be able to initialize properly
            result.Iterations.Count().Should().Be(count - 1);
        }

        [Test]
        public void Acceptance_TrueCondition()
        {
            ProfilerSession.StartSession()
                .Task(Task)
                .Assert(pr => pr.Iterations.Count() == 1)
                .RunSession();
        }

        [Test]
        public void Acceptance_MultipleTrueCondition()
        {
            ProfilerSession.StartSession()
                .Task(Task)
                .Assert(pr => pr.Iterations.Count() == 1)
                .Assert(pr => pr.AverageMilliseconds() > 0)
                .RunSession();
        }

        [Test]
        public void Acceptance_FalseCondition()
        {
            Assert.Throws<MeasureMap.AssertionException>(() => ProfilerSession.StartSession()
                .Task(Task)
                .SetIterations(2)
                .Assert(pr => pr.Iterations.Count() == 1)
                .RunSession());
        }

        [Test]
        public void Acceptance_WithoutTask()
        {
            Assert.Throws<ArgumentException>(() => ProfilerSession.StartSession()
                .RunSession());
        }

        [Test]
        public void Acceptance_Trace()
        {
            var result = ProfilerSession.StartSession()
                .Task(Task)
                .SetIterations(10)
                .RunSession()
                .Trace(false);

            Assert.That(result.Contains("Duration"));
            Assert.That(result.Contains("Total Time"));
            Assert.That(result.Contains("Average Time"));
            Assert.That(result.Contains("Memory Initial size"));
            Assert.That(result.Contains("Memory End size"));
            Assert.That(result.Contains("Memory Increase"));
        }

        [Test]
        public void Acceptance_Context()
        {
            var result = ProfilerSession.StartSession()
                .Task(c =>
                {
                    var iteration = c.Get<int>(ContextKeys.Iteration);
                    if (iteration == 9)
                    {
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    }
                })
                .SetIterations(10)
                .RunSession();

            result.Slowest.Iteration.Should().Be(9);
        }

        [Test]
        public void Acceptance_Output()
        {
            var result = ProfilerSession.StartSession()
                .Task(c =>
                {
                    var iteration = c.Get<int>(ContextKeys.Iteration);
                    if (iteration == 9)
                    {
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    }

                    return iteration;
                })
                .SetIterations(10)
                .RunSession();

            Assert.That((int)result.Slowest.Data == 9);
        }

        [Test]
        public void Acceptance_Setup()
        {
            var count = 0;
            
            ProfilerSession.StartSession()
                .Setup(() => count += 1)
                .Task(Task)
                .SetIterations(10)
                .RunSession();

            Assert.That(count == 1);
        }

        [Test]
        public void Acceptance_Duration()
        {
            int count = 0;
            var result = ProfilerSession.StartSession()
                .Task(() => count++)
                .SetDuration(TimeSpan.FromSeconds(1))
                .RunSession();

            // the task is rune once more to be able to initialize properly
            result.Iterations.Count().Should().BeGreaterThan(10);
        }

        [Test]
        public void Acceptance_Duration_Task()
        {
            int count = 0;
            ProfilerSession.StartSession()
                .Task(() => count++)
                .SetDuration(TimeSpan.FromSeconds(1))
                .RunSession();

            // the task is rune once more to be able to initialize properly
            count.Should().BeGreaterThan(10);
        }

        private void Task()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.002));
        }
    }
}
