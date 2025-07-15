using MeasureMap;
using Polaroider;

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
                .SetIterations(20)
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

            //
            // it's enaough to only check the firs 20
            log.Take(20).MatchSnapshot();
        }
    }
}
