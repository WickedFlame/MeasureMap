using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasureMap.UnitTest
{
    public class OnEndPipelineTests
    {
        [Test]
        public void OnEndPipeline_OnEndPipeline()
        {
            var run = false;
            var context = new ExecutionContext();

            var session = ProfilerSession.StartSession()
                .OnEndPipeline(e =>
                {
                    run = true;
                    e.Should().BeSameAs(context);
                });

            session.Settings.OnEndPipeline(context);
            run.Should().BeTrue();
        }

        [Test]
        public void OnEndPipeline_Event()
        {
            var run = false;
            var context = new ExecutionContext();

            var session = ProfilerSession.StartSession()
                .OnEndPipeline(e =>
                {
                    run = true;
                    e.Should().BeSameAs(context);
                });

            session.Settings.OnEndPipelineEvent(context);
            run.Should().BeTrue();
        }

        [Test]
        public void OnEndPipeline_Event_NotSet()
        {
            var settings = new ProfilerSettings();
            var act = () => settings.OnEndPipeline(new ExecutionContext());

            act.Should().NotThrow();
        }



        [Test]
        public void OnEndPipeline_BenchmarkRunner()
        {
            var run = false;
            var context = new ExecutionContext();

            var runner = new BenchmarkRunner()
                .OnEndPipeline(e =>
                {
                    run = true;
                    e.Should().BeSameAs(context);
                });

            runner.Settings.OnEndPipeline(context);
            run.Should().BeTrue();
        }

        [Test]
        public void OnEndPipeline_BenchmarkRunner_Event()
        {
            var run = false;
            var context = new ExecutionContext();

            var runner = new BenchmarkRunner()
                .OnEndPipeline(e =>
                {
                    run = true;
                    e.Should().BeSameAs(context);
                });


            runner.Settings.OnEndPipelineEvent(context);
            run.Should().BeTrue();
        }
    }
}
