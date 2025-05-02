using MeasureMap;

namespace Measuremap.IntegrationTest
{
    public class OnEndPipelineTests
    {
        [Test]
        public void OnEndPipeline_MultyThreadSessionHandler()
        {
            var set = false;
            var context = new ExecutionContext();

            ProfilerSession.StartSession()
                .OnStartPipeline(s => context)
                .OnEndPipeline(e => set = e == context)
                .SetThreads(1)
                .RunWarmup(false)
                .Task(() => { })
                .RunSession();

            set.Should().BeTrue();
        }

        [Test]
        public void OnEndPipeline_Warmup()
        {
            var set = false;
            var context = new ExecutionContext();

            ProfilerSession.StartSession()
                .OnStartPipeline(s => context)
                .OnEndPipeline(e => set = e == context)
                .SetThreads(1)
                .Task(() => { })
                .RunSession();

            set.Should().BeTrue();
        }

        [Test]
        public void OnEndPipeline_MainThreadSessionHandler()
        {
            var set = false;
            var context = new ExecutionContext();
            context.Set("test", 1);

            ProfilerSession.StartSession()
                .OnStartPipeline(s => context)
                .OnEndPipeline(e => set = e.Get<int>("test") == 1)
                .SetThreadBehaviour(ThreadBehaviour.RunOnMainThread)
                .RunWarmup(false)
                .SetIterations(1)
                .Task(() => { })
                .RunSession();

            set.Should().BeTrue();
        }

        [Test]
        public void OnEndPipeline_Basic()
        {
            var set = false;
            var context = new ExecutionContext();

            ProfilerSession.StartSession()
                .OnStartPipeline(s => context)
                .OnEndPipeline(e => set = e == context)
                .RunWarmup(false)
                .SetIterations(1)
                .Task(() => { })
                .RunSession();

            set.Should().BeTrue();
        }



        [Test]
        public void OnEndPipeline_Context_Updated()
        {
            var set = false;
            var context = new ExecutionContext();
            var refitem = new RefItem();

            ProfilerSession.StartSession()
                .OnStartPipeline(s => 
                {
                    context.Set("test", refitem);
                    return context;
                })
                .OnEndPipeline(e => set = e.Get<RefItem>("test") == refitem)
                .Task(c => { })
                .SetThreads(3)
                .SetIterations(2)
                .RunWarmup(false)
                .RunSession();

            set.Should().BeTrue();
        }

        public class RefItem { }
    }
}
