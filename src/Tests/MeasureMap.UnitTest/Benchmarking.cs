using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace MeasureMap.UnitTest
{
    public class Benchmarking
    {
        [Test]
        public void MultipleTasks_ByProfilerSession_StartSession()
        {
            var runner = new BenchmarkRunner();
            runner.SetIterations(10);

            var session = ProfilerSession.StartSession()
                .Task(c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    if (i == 9)
                    {
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    }

                    return i;
                });

            runner.AddSession("first", session);

            session = ProfilerSession.StartSession()
                .Task(c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    if (i == 9)
                    {
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    }

                    return i;
                });

            runner.AddSession("second", session);
            var results = runner.RunSessions();

            //Assert.That((int) results.Slowest.Data == 9);
            Assert.That(results.Count() == 2);
        }

        [Test]
        public void MultipleTasks_BenchmarkRunner_Task()
        {
            var runner = new BenchmarkRunner();
            runner.SetIterations(10);
            runner.Task("first", c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    if (i == 9)
                    {
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    }

                    return i;
                });

            runner.Task("second", c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    if (i == 9)
                    {
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    }

                    return i;
                });

            var results = runner.RunSessions();

            //Assert.That((int) results.Slowest.Data == 9);
            Assert.That(results.Count() == 2);
        }

        [Test]
        public void MultipleTasks_BenchmarkRunner_DisableWarmup()
        {
            var cnt = 0;

            var runner = new BenchmarkRunner();
            runner.SetSettings(new ProfilerSettings {Iterations = 10});
            runner.Task("first", c => cnt += 1);
            runner.Task("second", c => cnt += 1);

            runner.RunSessions();

            Assert.That(cnt == 22);

            runner = new BenchmarkRunner();
            runner.SetSettings(new ProfilerSettings { Iterations = 10, RunWarmup = false });
            runner.Task("first", c => cnt -= 1);
            runner.Task("second", c => cnt -= 1);

            runner.RunSessions();

            Assert.That(cnt == 2);
        }
    }
}
