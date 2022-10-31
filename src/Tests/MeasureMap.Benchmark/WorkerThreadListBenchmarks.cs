using MeasureMap.Threading;
using MeasureMap.Tracers.Metrics;

namespace MeasureMap.Benchmark
{
    public class WorkerThreadListBenchmarks
    {
        public void RunBenchmarks()
        {
            // Measure the time it takes to create a new thread
            // ~ 0.24 ms
            // 

            var threads = new WorkerThreadList();

            var benchmark = new BenchmarkRunner();
            benchmark.SetIterations(10);

            benchmark.Task("StartNew Thread", ctx => threads.StartNew(ctx.Get<int>(ContextKeys.Iteration), () => new Result(), WorkerThread.Factory));
            benchmark.Task("StartNew Task", ctx => threads.StartNew(ctx.Get<int>(ContextKeys.Iteration), () => new Result(), WorkerTask.Factory));

            benchmark.RunSessions()
                .Trace(new TraceMetrics());
        }

        public class TraceMetrics : Tracers.TraceMetrics
        {
            public TraceMetrics()
            {
                Add(ProfilerMetric.AverageMilliseconds);
                Add(ProfilerMetric.Fastest);
                Add(ProfilerMetric.Slowest);
                Add(ProfilerMetric.Throughput);
            }
        }
    }
}
