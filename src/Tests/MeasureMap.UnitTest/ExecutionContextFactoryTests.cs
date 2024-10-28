using FluentAssertions;
using NUnit.Framework;

namespace MeasureMap.UnitTest
{
    public class ExecutionContextFactoryTests
    {
        [Test]
        public void ExecutionContext_Factory_Create()
        {
            var context = new ExecutionContext();

            var session = ProfilerSession.StartSession()
                .CreateExecutionContext(s => context);

            session.Settings.ExecutionContextFactory(session.Settings).Should().BeSameAs(context);
        }

        [Test]
        public void ExecutionContext_Factory_Create_Settings()
        {
            var session = ProfilerSession.StartSession()
                .CreateExecutionContext(s => new ExecutionContext(s));
            
            session.Settings.ExecutionContextFactory(session.Settings).Settings.Should().BeSameAs(session.Settings);
        }


        [Test]
        public void ExecutionContext_Factory_CreateContext()
        {
            var context = new ExecutionContext();

            var session = ProfilerSession.StartSession()
                .CreateExecutionContext(s => context);

            session.Settings.GetContext().Should().BeSameAs(context);
        }

        [Test]
        public void ExecutionContext_Factory_CreateContext_Settings()
        {
            var session = ProfilerSession.StartSession()
                .CreateExecutionContext(s => new ExecutionContext(s));

            session.Settings.GetContext().Settings.Should().BeSameAs(session.Settings);
        }
    }
}
