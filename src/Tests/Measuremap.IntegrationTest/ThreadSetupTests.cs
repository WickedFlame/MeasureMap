
namespace MeasureMap.IntegrationTest
{
    public class ThreadSetupTests
    {
        [Test]
        public void ThreadSetup_EnsureSetupAll_BeforeStart()
        {
            var log = new List<string>();

            //
            // Ensure that all threads are setup and created
            // and that all OnStartPipelies are called
            // before the tasks are run

            ProfilerSession.StartSession()
                .SetThreads(10)
                .SetIterations(1)
                .RunWarmup(false)
                .OnStartPipeline(s =>
                {
                    log.Add("Setup");
                    return s.CreateContext();
                })
                .Task(() => 
                {
                    log.Add("Action");
                })
                .RunSession();

            log.Count(x => x == "Setup").Should().Be(10);
            log.Count(x => x == "Action").Should().Be(10);
        }
    }
}
