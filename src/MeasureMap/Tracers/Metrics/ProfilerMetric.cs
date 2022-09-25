
namespace MeasureMap.Tracers.Metrics
{
    /// <summary>
    /// Enumeration of all default Metrics of a ProfilerResult
    /// </summary>
    public class ProfilerMetric : Enumeration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public ProfilerMetric(string name) : base(name)
        {
        }

        // Warmup
        //
        public static readonly ProfilerMetric DurationWarmup = new("Duration Warmup");


        // Setup
        //
        public static readonly ProfilerMetric Threads = new("Threads");
        public static readonly ProfilerMetric Iterations = new("Iterations");
        public static readonly ProfilerMetric Duration = new("Duration");
        public static readonly ProfilerMetric TotalTime = new("Total Time");
        public static readonly ProfilerMetric AverageTime = new("Average Time");
        public static readonly ProfilerMetric AverageMilliseconds = new("Average Milliseconds");
        public static readonly ProfilerMetric AverageNanoseconds = new("Average Nanoseconds");
        public static readonly ProfilerMetric AverageMicroseconds = new("Average Microseconds");
        public static readonly ProfilerMetric AverageTicks = new("Average Ticks");
        public static readonly ProfilerMetric Fastest = new("Fastest");
        public static readonly ProfilerMetric Slowest = new("Slowest");
        public static readonly ProfilerMetric Throughput = new("Throughput");


        // Memory
        //
        public static readonly ProfilerMetric MemoryInitialSize = new("Memory Initial size");
        public static readonly ProfilerMetric MemoryEndSize = new("Memory End size");
        public static readonly ProfilerMetric MemoryIncrease = new("Memory Increase");
    }
}
