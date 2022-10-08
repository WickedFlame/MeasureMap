using MeasureMap.Threading;

namespace MeasureMap.Benchmark
{
    public class WorkerThreadListBenchmarks
    {
        public void RunBenchmarks()
        {
            var threads = new WorkerThreadList();

            var benchmark = new BenchmarkRunner();
            benchmark.SetIterations(10);

            benchmark.Task("Setup 1 Thread", ctx => threads.StartNew(ctx.Get<int>(ContextKeys.Iteration), () => new Result()));

            benchmark.RunSessions()
                .Trace(new MeasureMap.Benchmark.TraceMetrics());
        }
    }
}
