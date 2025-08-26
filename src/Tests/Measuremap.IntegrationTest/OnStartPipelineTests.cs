using MeasureMap;

namespace MeasureMap.IntegrationTest
{
    public class OnStartPipelineTests
    {
        [Test]
        public void OnStartPipeline_MultyThreadSessionHandler()
        {
            var context = new ExecutionContext();

            ProfilerSession.StartSession()
                .OnStartPipeline(s => context)
                .Task(c => c.Should().BeSameAs(context))
                .SetThreads(1)
                .RunWarmup(false)
                .RunSession();
        }

        [Test]
        public void OnStartPipeline_Warmup()
        {
            //
            // the context is cloned in the IterationRunner so it is not same as OnStartPipeline
            // all properties should be same...

            var context = new ExecutionContext();

            ProfilerSession.StartSession()
                .OnStartPipeline(s => context)
                .Task(c => c.Should().BeEquivalentTo(context))
                .SetThreads(1)
                .RunSession();
        }

        [Test]
        public void OnStartPipeline_MainThreadSessionHandler()
        {
            var context = new ExecutionContext();
            context.Set("test", 1);

            ProfilerSession.StartSession()
                .OnStartPipeline(s => context)
                .Task(c => c.Get<int>("test").Should().Be(1))
                .SetThreadBehaviour(ThreadBehaviour.RunOnMainThread)
                .RunWarmup(false)
                .SetIterations(1)
                .RunSession();
        }

        [Test]
        public void OnStartPipeline_Basic()
        {
            var context = new ExecutionContext();

            ProfilerSession.StartSession()
                .OnStartPipeline(s => context)
                .Task(c => c.Should().BeSameAs(context))
                .RunWarmup(false)
                .SetIterations(1)
                .RunSession();
        }

        [Test]
        public void OnStartPipeline_Settings_CreateContext()
        {
            IExecutionContext context = null;

            ProfilerSession.StartSession()
                .OnStartPipeline(s => context = s.CreateContext())
                .Task(c => c.Should().NotBeNull().And.BeSameAs(context))
                .RunWarmup(false)
                .SetIterations(1)
                .RunSession();
        }
    }
}
