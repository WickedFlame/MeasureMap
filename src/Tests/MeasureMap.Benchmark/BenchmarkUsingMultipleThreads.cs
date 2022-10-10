
using MeasureMap.Tracers.Metrics;

namespace MeasureMap.Benchmark
{
    public class BenchmarkUsingMultipleThreads
    {
        public void RunBenchmarks()
        {
            // Using more threads does not neccessarily increase performance
            // Running a loop that almost does no work, will result in a single thread doing 
            // almost the same amount of iterations as a 100 threads do together.
            // When simulating some work with the Thread.Sleep it is shown that the more threads are used
            // the more work is done
            //

            var benchmark = new BenchmarkRunner();
            benchmark.SetDuration(TimeSpan.FromSeconds(10));

            benchmark.Task("Setup 1 Thread", () => Work()).SetThreads(1);

            benchmark.Task("Setup 10 Threads", () => Work()).SetThreads(10);

            benchmark.Task("Setup 100 Threads", () => Work()).SetThreads(100);

            benchmark.RunSessions()
                .Trace(new TraceMetrics());
        }

        private void Work()
        {
            // Simultate some hard work
            //

            Thread.Sleep(5);
            
            for (int i = 0; i < 1000000; i++) { }
        }

        public class TraceMetrics : Tracers.TraceMetrics
        {
            public TraceMetrics()
            {
                Add(ProfilerMetric.AverageMilliseconds);
                Add(ProfilerMetric.Fastest);
                Add(ProfilerMetric.Slowest);
                Add(ProfilerMetric.Threads);
                Add(ProfilerMetric.Iterations);
                Add(ProfilerMetric.Throughput);
            }
        }
    }
}
