using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace MeasureMap.UnitTest
{
    [TestFixture]
    public class BenchmarkingSessions
    {
        [Test]
        public void BenchmarkingSessions_AddWarmup()
        {
            var firstPre = 0;
            var secondPre = 0;

            var runner = new BenchmarkRunner();
            runner.SetIterations(10);
            runner.Task("first", c =>
            {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.05));
                firstPre = c.Get<int>("first");
            }).PreExecute(c =>
            {
                var iteration = c.Get<int>(ContextKeys.Iteration);
                c.Set("first", iteration + 1);
            });

            runner.Task("second", c =>
            {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.05));
                secondPre = c.Get<int>("second");
            }).PreExecute(c =>
            {
                var iteration = c.Get<int>(ContextKeys.Iteration);
                c.Set("second", iteration + 2);
            });

            runner.RunSessions();

            firstPre.Should().Be(11);
            secondPre.Should().Be(12);
        }
    }
}
