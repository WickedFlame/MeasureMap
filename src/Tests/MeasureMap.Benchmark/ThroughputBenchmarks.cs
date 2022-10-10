
using MeasureMap.Tracers.Metrics;

namespace MeasureMap.Benchmark
{
    public class ThroughputBenchmarks
    {
        public void RunBenchmarks()
        {
            // check the throughput 
            // from 0 to 1000ms
            //

            var benchmark = new BenchmarkRunner();
            benchmark.SetIterations(10);

            benchmark.Task("no interval", () => { }).SetInterval(TimeSpan.Zero);
            benchmark.Task("1 ms interval", () => { }).SetInterval(TimeSpan.FromMilliseconds(1));
            benchmark.Task("5 ms interval", () => { }).SetInterval(TimeSpan.FromMilliseconds(5));
            benchmark.Task("10 ms interval", () => { }).SetInterval(TimeSpan.FromMilliseconds(10));
            benchmark.Task("100 ms interval", () => { }).SetInterval(TimeSpan.FromMilliseconds(100));
            benchmark.Task("1000 ms interval", () => { }).SetInterval(TimeSpan.FromMilliseconds(1000));

            benchmark.RunSessions()
                .Trace(new TraceMetrics());
        }
        
        public class TraceMetrics : Tracers.TraceMetrics
        {
            public TraceMetrics()
            {
                Add(ProfilerMetric.Throughput);
            }
        }
    }
}
