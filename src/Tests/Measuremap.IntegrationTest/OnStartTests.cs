using MeasureMap;

namespace Measuremap.IntegrationTest
{
    public class OnStartTests
    {
        [Test]
        public void OnStart_MultyThreadSessionHandler()
        {
            var context = new ExecutionContext();

            ProfilerSession.StartSession()
                .OnStart(s => context)
                .Task(c => c.Should().BeSameAs(context))
                .SetThreads(1)
                .RunWarmup(false)
                .RunSession();
        }

        [Test]
        public void OnStart_Warmup()
        {
            var context = new ExecutionContext();

            ProfilerSession.StartSession()
                .OnStart(s => context)
                .Task(c => c.Should().BeSameAs(context))
                .SetThreads(1)
                .RunSession();
        }

        [Test]
        public void OnStart_MainThreadSessionHandler()
        {
            var context = new ExecutionContext();
            context.Set("test", 1);

            ProfilerSession.StartSession()
                .OnStart(s => context)
                .Task(c => c.Get<int>("test").Should().Be(1))
                .SetThreadBehaviour(ThreadBehaviour.RunOnMainThread)
                .RunWarmup(false)
                .SetIterations(1)
                .RunSession();
        }

        [Test]
        public void OnStart_Basic()
        {
            var context = new ExecutionContext();

            ProfilerSession.StartSession()
                .OnStart(s => context)
                .Task(c => c.Should().BeSameAs(context))
                .RunWarmup(false)
                .SetIterations(1)
                .RunSession();
        }
    }
}
