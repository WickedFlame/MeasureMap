using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
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
    }
}
