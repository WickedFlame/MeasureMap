using MeasureMap;

namespace Measuremap.IntegrationTest
{
    public class ExecutionContextTests
    {
        [Test]
        public void ExecutionContext_MultyThreadSessionHandler()
        {
            var context = new ExecutionContext();

            ProfilerSession.StartSession()
                .CreateExecutionContext(s => context)
                .Task(c => c.Should().BeSameAs(context))
                .SetThreads(1)
                .RunWarmup(false)
                .RunSession();
        }

        [Test]
        public void ExecutionContext_Warmup()
        {
            var context = new ExecutionContext();

            ProfilerSession.StartSession()
                .CreateExecutionContext(s => context)
                .Task(c => c.Should().BeSameAs(context))
                .SetThreads(1)
                .RunSession();
        }

        [Test]
        public void ExecutionContext_MainThreadSessionHandler()
        {
            var context = new ExecutionContext();
            context.Set("test", 1);

            ProfilerSession.StartSession()
                .CreateExecutionContext(s => context)
                .Task(c => c.Get<int>("test").Should().Be(1))
                .SetThreadBehaviour(ThreadBehaviour.RunOnMainThread)
                .RunWarmup(false)
                .SetIterations(1)
                .RunSession();
        }

        [Test]
        public void ExecutionContext_Basic()
        {
            var context = new ExecutionContext();

            ProfilerSession.StartSession()
                .CreateExecutionContext(s => context)
                .Task(c => c.Should().BeSameAs(context))
                .RunWarmup(false)
                .SetIterations(1)
                .RunSession();
        }
    }
}
