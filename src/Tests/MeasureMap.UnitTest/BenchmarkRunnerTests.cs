using System;
using FluentAssertions;
using MeasureMap.Runners;
using NUnit.Framework;

namespace MeasureMap.UnitTest
{
    public class BenchmarkRunnerTests
    {
        [Test]
        public void BenchmarkRunner_SetDuration()
        {
            var runner = new BenchmarkRunner()
                .SetDuration(TimeSpan.FromMinutes(1));
            runner.Settings.Duration.Should().Be(TimeSpan.FromMinutes(1));
        }

        [Test]
        public void BenchmarkRunner_SetDuration_Runner()
        {
            var runner = new BenchmarkRunner()
                .SetDuration(TimeSpan.FromMinutes(1));
            runner.Settings.Runner.Should().BeOfType<DurationRunner>();
        }

        [Test]
        public void BenchmarkRunner_SetIterations()
        {
            var runner = new BenchmarkRunner()
                .SetIterations(10);
            runner.Settings.Iterations.Should().Be(10);
        }

        [Test]
        public void BenchmarkRunner_SetIterations_Runner()
        {
            var runner = new BenchmarkRunner()
                .SetIterations(10);
            runner.Settings.Runner.Should().BeOfType<IterationRunner>();
        }
    }
}
