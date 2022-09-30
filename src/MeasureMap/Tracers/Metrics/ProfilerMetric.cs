
namespace MeasureMap.Tracers.Metrics
{
    /// <summary>
    /// Metric definition for Profilers and Benchmarks
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
        /// <summary>
        /// Duration Warmup
        /// </summary>
        public static readonly ProfilerMetric DurationWarmup = new("Duration Warmup");


        // Setup
        //
        /// <summary>
        /// Threads
        /// </summary>
        public static readonly ProfilerMetric Threads = new("Threads");

        /// <summary>
        /// Iterations
        /// </summary>
        public static readonly ProfilerMetric Iterations = new("Iterations");

        /// <summary>
        /// Returns the timespan that the Session took for all tasks to run
        /// </summary>
        public static readonly ProfilerMetric Duration = new("Duration");

        /// <summary>
        /// Returns the total sum of all times of all loops
        /// </summary>
        public static readonly ProfilerMetric TotalTime = new("Total Time");

        /// <summary>
        /// Avg. Time
        /// </summary>
        public static readonly ProfilerMetric AverageTime = new("Avg. Time");

        /// <summary>
        /// Avg. Milliseconds
        /// </summary>
        public static readonly ProfilerMetric AverageMilliseconds = new("Avg. Milliseconds");

        /// <summary>
        /// Avg. Nanoseconds
        /// </summary>
        public static readonly ProfilerMetric AverageNanoseconds = new("Avg. Nanoseconds");

        /// <summary>
        /// Avg. Microseconds
        /// </summary>
        public static readonly ProfilerMetric AverageMicroseconds = new("Avg. Microseconds");

        /// <summary>
        /// Avg. Ticks
        /// </summary>
        public static readonly ProfilerMetric AverageTicks = new("Avg. Ticks");

        /// <summary>
        /// Fastest
        /// </summary>
        public static readonly ProfilerMetric Fastest = new("Fastest");

        /// <summary>
        /// Slowest
        /// </summary>
        public static readonly ProfilerMetric Slowest = new("Slowest");

        /// <summary>
        /// Throughput
        /// </summary>
        public static readonly ProfilerMetric Throughput = new("Throughput");


        // Memory
        //
        /// <summary>
        /// Memory init size
        /// </summary>
        public static readonly ProfilerMetric MemoryInitialSize = new("Memory init size");

        /// <summary>
        /// Memory end size
        /// </summary>
        public static readonly ProfilerMetric MemoryEndSize = new("Memory end size");

        /// <summary>
        /// Memory increase
        /// </summary>
        public static readonly ProfilerMetric MemoryIncrease = new("Memory increase");
    }
}
