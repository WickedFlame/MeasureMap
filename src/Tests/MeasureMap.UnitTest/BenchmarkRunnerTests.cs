using System;
using System.IO;
using FluentAssertions;
using MeasureMap.Runners;
using NUnit.Framework;
using Polaroider;

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

        [Test]
        public void BenchmarkRunner_OnStartPipeline()
        {
            var context = new ExecutionContext();

            var runner = new BenchmarkRunner()
                .OnStartPipeline(s => context);

            runner.Settings.OnStartPipelineEvent(runner.Settings).Should().BeSameAs(context);
        }

        [Test]
        public void BenchmarkRunner_OnStartPipeline_Factory_OnStartPipeline_Settings()
        {
            var runner = new BenchmarkRunner()
                .OnStartPipeline(s => new ExecutionContext(s));

            runner.Settings.OnStartPipeline().Settings.Should().BeSameAs(runner.Settings);
        }

        [TestCase(ThreadBehaviour.Task)]
        [TestCase(ThreadBehaviour.Thread)]
        public void BenchmarkRunner_SetThreadBehaviour(ThreadBehaviour behaviour)
        {
            new BenchmarkRunner()
                .SetThreadBehaviour(behaviour).Settings.ThreadBehaviour.Should().Be(behaviour);
        }

        [TestCase(ThreadBehaviour.Task)]
        [TestCase(ThreadBehaviour.Thread)]
        public void BenchmarkRunner_Run_ThreadBehaviour(ThreadBehaviour behaviour)
        {
            var runner = new BenchmarkRunner()
                .SetThreadBehaviour(behaviour);

            var session = runner.Task("test", () => { });
            runner.RunSessions();

            session.Settings.ThreadBehaviour.Should().Be(behaviour);
        }

        [Test]
        public void BenchmarkRunner_LogToConsole()
        {
            var stdOut = Console.Out;

            var consoleOut = new StringWriter();
            Console.SetOut(consoleOut);

            var runner = new BenchmarkRunner()
                //.LogToConsole()
                .SetMinLogLevel(MeasureMap.Diagnostics.LogLevel.Debug)
                .Task("one", ctx =>
                {
                    ctx.Logger.Write("Test");
                });

            runner.RunSession();

            consoleOut.ToString().TrimEnd().MatchSnapshot();

            Console.SetOut(stdOut);
        }
    }
}
