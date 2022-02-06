﻿using System;
using System.Diagnostics;
using NUnit.Framework;
using System.Linq;
using FluentAssertions;
using MeasureMap.Runners;

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

            Assert.AreEqual(1, session.Settings.Iterations);
        }

        [Test]
        public void ProfileSession_SetIterations()
        {
            var session = ProfilerSession.StartSession()
                .SetIterations(12);

            Assert.AreEqual(12, session.Settings.Iterations);
        }

        [Test]
        public void ProfileSession_SetDuration()
        {
            var session = ProfilerSession.StartSession()
                .SetDuration(TimeSpan.FromSeconds(1));

            Assert.AreEqual(TimeSpan.FromSeconds(1), session.Settings.Duration);
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

            Assert.IsTrue(result.AverageTicks > 0);
        }

        [Test]
        public void ProfileSession_TrueCondition()
        {
            ProfilerSession.StartSession()
                .Task(Task)
                .Assert(pr => pr.Iterations.Count() == 1)
                .RunSession();
        }

        [Test]
        public void ProfileSession_MultipleTrueCondition()
        {
            ProfilerSession.StartSession()
                .Task(Task)
                .Assert(pr => pr.Iterations.Count() == 1)
                .Assert(pr => pr.AverageMilliseconds > 0)
                .RunSession();
        }

        [Test]
        public void ProfileSession_FalseCondition()
        {
            Assert.Throws<MeasureMap.AssertionException>(() => ProfilerSession.StartSession()
                .Task(Task)
                .SetIterations(2)
                .Assert(pr => pr.Iterations.Count() == 1)
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
                .Task(c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    if (i > 1)
                    {
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    }

                    return i;
                })
                .SetIterations(10)
                .RunSession();

            Assert.That((int)result.Fastest.Data == 1);
        }

        [Test]
        public void ProfileSession_Slowest()
        {
            var result = ProfilerSession.StartSession()
                .Task(c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    if (i == 9)
                    {
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    }

                    return i;
                })
                .SetIterations(10)
                .RunSession();

            Assert.That((int)result.Slowest.Data == 9);
        }

        [Test]
        public void ProfileSession_ContextTask()
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

            Assert.That(result.Slowest.Iteration == 9);
        }

        [Test]
        public void ProfileSession_OutputTask()
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
        public void ProfileSession_SetIterations_Runner()
        {
            var session = ProfilerSession.StartSession()
                .SetIterations(1);

            session.Settings.Runner.Should().BeOfType<IterationRunner>();
        }

        [Test]
        public void ProfileSession_SetDuration_Runner()
        {
            var session = ProfilerSession.StartSession()
                .SetDuration(TimeSpan.FromSeconds(1));

            session.Settings.Runner.Should().BeOfType<TimeRunner>();
        }

        [Test]
        public void ProfileSession_SetDuration_CheckTime()
        {
            var sw = new Stopwatch();
            sw.Start();
            
            ProfilerSession.StartSession()
                .SetDuration(TimeSpan.FromSeconds(1))
                .Task(() => { })
                .RunSession();
            
            sw.Stop();

            sw.Elapsed.Should().BeGreaterThan(TimeSpan.FromSeconds(1)).And.BeLessThan(TimeSpan.FromSeconds(1.5));
        }

        [Test]
        public void ProfileSession_SetDuration_TotalTime()
        {
            var result = ProfilerSession.StartSession()
                .SetDuration(TimeSpan.FromSeconds(1))
                .Task(() => { })
                .RunSession();

            result.Elapsed().Should().BeGreaterThan(TimeSpan.FromSeconds(1)).And.BeLessThan(TimeSpan.FromSeconds(1.5));
        }

        [Test]
        public void ProfileSession_SetDuration_Iterations()
        {
            var cnt = 0;
            var result = ProfilerSession.StartSession()
                .SetDuration(TimeSpan.FromSeconds(1))
                .Task(() => { cnt++; })
                .RunSession();

            result.Iterations.Count().Should().BeGreaterThan(100);
        }

        [Test]
        public void ProfileSession_SetDuration_TaskExecutions()
        {
            var cnt = 0;
            ProfilerSession.StartSession()
                .SetDuration(TimeSpan.FromSeconds(1))
                .Task(() => { cnt++; })
                .RunSession();

            cnt.Should().BeGreaterThan(100);
        }

        private void Task()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.002));
        }
    }
}
