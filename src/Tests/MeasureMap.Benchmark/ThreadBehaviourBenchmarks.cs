
using MeasureMap.Tracers.Metrics;

namespace MeasureMap.Benchmark
{
    public class ThreadBehaviourBenchmarks
    {
        public void RunBenchmarks()
        {
            // check the throughput 
            // from 0 to 1000ms
            //

            var benchmark = new BenchmarkRunner();
            benchmark.SetIterations(10);

            benchmark.Task("Thread", () => { }).SetThreadBehaviour(ThreadBehaviour.Thread);
            benchmark.Task("Task", () => { }).SetThreadBehaviour(ThreadBehaviour.Task);
            benchmark.Task("MainThread", () => { }).SetThreadBehaviour(ThreadBehaviour.RunOnMainThread);

            benchmark.RunSessions()
                .Trace(new TraceMetrics());
        }
        
        public class TraceMetrics : Tracers.TraceMetrics
        {
            public TraceMetrics()
            {
                Add(ProfilerMetric.Duration);
                Add(ProfilerMetric.AverageMilliseconds);
                Add(ProfilerMetric.Throughput);
            }
        }
    }
}
