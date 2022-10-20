
namespace MeasureMap.Benchmark
{
    public class StartSessionBenchmarks
    {
        public void RunBenchmarks()
        {
            var benchmark = new BenchmarkRunner();
            benchmark.SetIterations(10);

            benchmark.Task("Setup 1 Thread", () => SetupOnThreads(1)).SetThreads(10);

            benchmark.Task("Setup 10 Threads", () => SetupOnThreads(10)).SetThreads(10);

            benchmark.RunSessions()
                .Trace(new MeasureMap.Benchmark.TraceMetrics());
        }

        public void SetupOnThreads(int threads)
        {
            ProfilerSession.StartSession()
                .SetThreads(threads)
                .Task(c2 =>
                {
                    // do nothing
                })
                .RunSession();
        }
    }
}
