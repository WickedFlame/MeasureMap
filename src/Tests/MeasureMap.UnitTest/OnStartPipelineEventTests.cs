using FluentAssertions;
using NUnit.Framework;

namespace MeasureMap.UnitTest
{
    public class OnStartPipelineEventTests
    {
        //[Test]
        //public void OnStartPipeline_Event_Create()
        //{
        //    var context = new ExecutionContext();

        //    var session = ProfilerSession.StartSession()
        //        .OnStartPipeline(s => context);

        //    session.Settings.OnStartPipelineEvent(session.Settings).Should().BeSameAs(context);
        //}

        //[Test]
        //public void OnStartPipeline_Event_Create_Settings()
        //{
        //    var session = ProfilerSession.StartSession()
        //        .OnStartPipeline(s => new ExecutionContext(s));
            
        //    session.Settings.OnStartPipelineEvent(session.Settings).Settings.Should().BeSameAs(session.Settings);
        //}


        //[Test]
        //public void OnStartPipeline_Event_OnStartPipeline()
        //{
        //    var context = new ExecutionContext();

        //    var session = ProfilerSession.StartSession()
        //        .OnStartPipeline(s => context);

        //    session.Settings.OnStartPipeline().Should().BeSameAs(context);
        //}

        //[Test]
        //public void OnStartPipeline_Event_CreateContext_Settings()
        //{
        //    var session = ProfilerSession.StartSession()
        //        .OnStartPipeline(s => new ExecutionContext(s));

        //    session.Settings.OnStartPipeline().Settings.Should().BeSameAs(session.Settings);
        //}
    }
}
