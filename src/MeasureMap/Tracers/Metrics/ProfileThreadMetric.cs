
namespace MeasureMap.Tracers.Metrics
{
    /// <summary>
    /// Enumeration of all default Metrics of a ProfilerThread
    /// A Profiler is run on 1-n Threads
    /// Each thred results in a ProfileThreadResult
    /// </summary>
    public class ProfileThreadMetric : Enumeration
    {
        /// <summary>
        /// Creates a instance for a enumeration
        /// </summary>
        /// <param name="name"></param>
        public ProfileThreadMetric(string name) : base(name)
        {
        }

        /// <summary>
        /// ThreadId
        /// </summary>
        public static readonly ProfileThreadMetric ThreadId = new("ThreadId");

        /// <summary>
        /// Iterations
        /// </summary>
        public static readonly ProfileThreadMetric Iterations = new("Iterations");


        /// <summary>
        /// Avg. Nanoseconds
        /// </summary>
        public static readonly ProfileThreadMetric AverageNanoseconds = new("Avg. Nanoseconds");

        /// <summary>
        /// Avg. Microseconds
        /// </summary>
        public static readonly ProfileThreadMetric AverageMicroseconds = new("Avg. Microseconds");

        /// <summary>
        /// Avg. Milliseconds
        /// </summary>
        public static readonly ProfileThreadMetric AverageMilliseconds = new("Avg. Milliseconds");

        /// <summary>
        /// Avg. Ticks
        /// </summary>
        public static readonly ProfileThreadMetric AverageTicks = new("Avg. Ticks");

        /// <summary>
        /// Avg. Time
        /// </summary>
        public static readonly ProfileThreadMetric AverageTime = new("Avg. Time");

        /// <summary>
        /// Fastest
        /// </summary>
        public static readonly ProfileThreadMetric Fastest = new("Fastest");

        /// <summary>
        /// Slowest
        /// </summary>
        public static readonly ProfileThreadMetric Slowest = new("Slowest");

        /// <summary>
        /// Total Time
        /// </summary>
        public static readonly ProfileThreadMetric TotalTime = new("Total Time");

        /// <summary>
        /// Throughput
        /// </summary>
        public static readonly ProfileThreadMetric Throughput = new("Throughput");


        /// <summary>
        /// Memory Initial size
        /// </summary>
        public static readonly ProfileThreadMetric MemoryInitialSize = new("Memory Initial size");

        /// <summary>
        /// Memory End size
        /// </summary>
        public static readonly ProfileThreadMetric MemoryEndSize = new("Memory End size");

        /// <summary>
        /// Memory Increase
        /// </summary>
        public static readonly ProfileThreadMetric MemoryIncrease = new("Memory Increase");
    }
}
