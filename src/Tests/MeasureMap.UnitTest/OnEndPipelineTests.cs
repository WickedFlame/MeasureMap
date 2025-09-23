
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

            session.ContextStack.Create(0, new ProfilerSettings()).Run(Mock.Of<ITask>(), context);

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

            session.ContextStack.Create(0, new ProfilerSettings()).Run(Mock.Of<ITask>(), context);
            run.Should().BeTrue();
        }
    }
}
