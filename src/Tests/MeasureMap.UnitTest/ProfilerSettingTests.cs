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

            session.Settings.Should().BeEquivalentTo(new {Iterations = 10});
        }
    }
}
