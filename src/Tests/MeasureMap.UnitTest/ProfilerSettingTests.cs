using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using MeasureMap.Runners;
using MeasureMap.Threading;
using NUnit.Framework;

namespace MeasureMap.UnitTest
{
    [TestFixture]
    public class ProfilerSettingTests
    {
        [Test]
        public void ProfilerSettings_Merge()
        {
            var session = ProfilerSession.StartSession();
            session.Settings.Iterations = 200;

            session.SetSettings(new ProfilerSettings {Iterations = 10});

            session.Settings.Should().BeEquivalentTo(new {Iterations = 10, RunWarmup = true});

            session.SetSettings(new ProfilerSettings {RunWarmup = false});

            session.Settings.Should().BeEquivalentTo(new {Iterations = 10, RunWarmup = false});
        }

        [Test]
        public void ProfilerSettings_SetFromSession()
        {
	        var session = ProfilerSession.StartSession()
		        .Settings(s=>
		        {
			        s.Iterations = 120;
		        });

	        session.Settings.Iterations.Should().Be(120);
        }

        [Test]
        public void ProfilerSettings_Runner_Default()
        {
            var settings = new ProfilerSettings();
            settings.Runner.Should().BeOfType<IterationRunner>();
        }

        [Test]
        public void ProfilerSettings_ProfilerSession_Runner_Default()
        {
            var session = ProfilerSession.StartSession();
            session.Settings.Runner.Should().BeOfType<IterationRunner>();
        }

        [Test]
        public void ProfilerSettings_Runner_SetIterartions()
        {
            var settings = new ProfilerSettings();

            // this sets a new iterationrunner
            settings.Iterations = 1;

            settings.Runner.Should().BeOfType<IterationRunner>();
        }

        [Test]
        public void ProfilerSettings_Runner_SetIterartions_CheckNew()
        {
            var settings = new ProfilerSettings();
            var runner = settings.Runner;
            
            // this sets a new iterationrunner
            settings.Iterations = 1;

            settings.Runner.Should().NotBeSameAs(runner);
        }

        [Test]
        public void ProfilerSettings_Runner_SetDuration()
        {
            var settings = new ProfilerSettings();

            // this sets a new iterationrunner
            settings.Duration = TimeSpan.FromSeconds(1);

            settings.Runner.Should().BeOfType<DurationRunner>();
        }

        [Test]
        public void ProfilerSettings_Runner_SetDuration_CheckNew()
        {
            var settings = new ProfilerSettings();
            var runner = settings.Runner;

            // this sets a new iterationrunner
            settings.Duration = TimeSpan.FromSeconds(1);

            settings.Runner.Should().NotBeSameAs(runner);
        }

        [Test]
        public void ProfilerSettings_GetThreadFactory_Default()
        {
            var settings = new ProfilerSettings();

            settings.GetThreadFactory().Invoke(1, () => new Result()).Should().BeOfType<WorkerThread>();
        }

        [Test]
        public void ProfilerSettings_GetThreadFactory_Thread()
        {
            var settings = new ProfilerSettings
            {
                ThreadBehaviour = ThreadBehaviour.Thread
            };

            settings.GetThreadFactory().Invoke(1, () => new Result()).Should().BeOfType<WorkerThread>();
        }

        [Test]
        public void ProfilerSettings_GetThreadFactory_Task()
        {
            var settings = new ProfilerSettings
            {
                ThreadBehaviour = ThreadBehaviour.Task
            };

            settings.GetThreadFactory().Invoke(1, () => new Result()).Should().BeOfType<WorkerTask>();
        }
    }
}
