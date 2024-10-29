using FluentAssertions;
using NUnit.Framework;

namespace MeasureMap.UnitTest
{
    public class OnStartFactoryTests
    {
        [Test]
        public void OnStart_Factory_Create()
        {
            var context = new ExecutionContext();

            var session = ProfilerSession.StartSession()
                .OnStart(s => context);

            session.Settings.OnStartEvent(session.Settings).Should().BeSameAs(context);
        }

        [Test]
        public void OnStart_Factory_Create_Settings()
        {
            var session = ProfilerSession.StartSession()
                .OnStart(s => new ExecutionContext(s));
            
            session.Settings.OnStartEvent(session.Settings).Settings.Should().BeSameAs(session.Settings);
        }


        [Test]
        public void OnStart_Factory_OnStart()
        {
            var context = new ExecutionContext();

            var session = ProfilerSession.StartSession()
                .OnStart(s => context);

            session.Settings.OnStart().Should().BeSameAs(context);
        }

        [Test]
        public void OnStart_Factory_CreateContext_Settings()
        {
            var session = ProfilerSession.StartSession()
                .OnStart(s => new ExecutionContext(s));

            session.Settings.OnStart().Settings.Should().BeSameAs(session.Settings);
        }
    }
}
