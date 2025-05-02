
namespace MeasureMap.Benchmark
{
    public class RampupBenchmarks
    {
        public void RunBenchmarks()
        {
            ProfilerSession.StartSession()
                .Task(ctx => { })
                .SetDuration(TimeSpan.FromSeconds(30))
                .SetMinLogLevel(Diagnostics.LogLevel.Info)
                .SetThreads(10, TimeSpan.FromSeconds(20))
                .RunWarmup(false)
                .RunSession()
                .Trace();
        }
    }
}
