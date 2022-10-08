
namespace MeasureMap.Benchmark
{
    public class BenchmarkSample
    {
        public void RunBenchmarks()
        {
            var benchmark = new BenchmarkRunner();
            benchmark.SetIterations(100);
            benchmark.SetMinLogLevel(Diagnostics.LogLevel.Info);

            benchmark.Task("Setup 1 Thread", () => {  }).SetThreads(1);

            benchmark.Task("Setup 10 Threads", () => { }).SetThreads(10);

            benchmark.RunSessions()
                .Trace(new MeasureMap.Benchmark.TraceMetrics());
        }
    }
}
