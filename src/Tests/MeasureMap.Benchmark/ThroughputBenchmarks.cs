
using MeasureMap.Tracers.Metrics;

namespace MeasureMap.Benchmark
{
    public class ThroughputBenchmarks
    {
        public void RunBenchmarks()
        {
            // check the throughput 
            // from 0 to 
            var benchmark = new BenchmarkRunner();
            benchmark.SetIterations(100);

            benchmark.Task("no interval", () => { }).SetInterval(TimeSpan.Zero);
            benchmark.Task("1 ms interval", () => { }).SetInterval(TimeSpan.FromMilliseconds(1));
            benchmark.Task("5 ms interval", () => { }).SetInterval(TimeSpan.FromMilliseconds(5));
            benchmark.Task("10 ms interval", () => { }).SetInterval(TimeSpan.FromMilliseconds(10));
            benchmark.Task("100 ms interval", () => { }).SetInterval(TimeSpan.FromMilliseconds(100));
            benchmark.Task("1000 ms interval", () => { }).SetInterval(TimeSpan.FromMilliseconds(1000));

            benchmark.RunSessions()
                .Trace(new TraceMetrics());
        }

        //# MeasureMap - Benchmark result
        // Iterations: 100
        //## Summary
        //| Name        |        Avg. Time | Avg. Milliseconds |          Fastest |          Slowest | Threads |     Throughput |
        //| ----------- | ---------------: | ----------------: | ---------------: | ---------------: | ------- | -------------: |
        //| no interval | 00:00:00.0000034 |         0.0034 ms | 00:00:00.0000026 | 00:00:00.0000277 | 1       |    614.71127/s |
        //| 1 ms interval | 00:00:00.0000027 |         0.0027 ms | 00:00:00.0000025 | 00:00:00.0000051 | 1       |      975.605/s |
        //| 5 ms interval | 00:00:00.0000034 |         0.0034 ms | 00:00:00.0000019 | 00:00:00.0000117 | 1       |    200.39522/s |
        //| 10 ms interval | 00:00:00.0000024 |         0.0024 ms | 00:00:00.0000014 | 00:00:00.0000104 | 1       |    100.54647/s |
        //| 100 ms interval | 00:00:00.0000041 |         0.0041 ms | 00:00:00.0000018 | 00:00:00.0000121 | 1       |     10.09525/s |
        //| 1000 ms interval | 00:00:00.0000048 |         0.0048 ms | 00:00:00.0000016 | 00:00:00.0001001 | 1       |      1.01004/s |

        public class TraceMetrics : Tracers.TraceMetrics
        {
            public TraceMetrics()
            {
                Add(ProfilerMetric.Throughput);
            }
        }
    }
}
