
namespace MeasureMap.Tracers.Metrics
{
    /// <summary>
    /// Enumeration of all default Metrics of a ProfilerThread
    /// A Profiler is run on 1-n Threads
    /// Each thred results in a ProfileThreadResult
    /// </summary>
    public class ThreadMetric : Enumeration
    {
        /// <summary>
        /// Creates a instance for a enumeration
        /// </summary>
        /// <param name="name"></param>
        public ThreadMetric(string name) : base(name)
        {
        }

        /// <summary>
        /// ThreadId
        /// </summary>
        public static readonly ThreadMetric ThreadId = new("ThreadId");

        /// <summary>
        /// Iterations
        /// </summary>
        public static readonly ThreadMetric Iterations = new("Iterations");


        /// <summary>
        /// Avg. Nanoseconds
        /// </summary>
        public static readonly ThreadMetric AverageNanoseconds = new("Avg. Nanoseconds");

        /// <summary>
        /// Avg. Microseconds
        /// </summary>
        public static readonly ThreadMetric AverageMicroseconds = new("Avg. Microseconds");

        /// <summary>
        /// Avg. Milliseconds
        /// </summary>
        public static readonly ThreadMetric AverageMilliseconds = new("Avg. Milliseconds");

        /// <summary>
        /// Avg. Ticks
        /// </summary>
        public static readonly ThreadMetric AverageTicks = new("Avg. Ticks");

        /// <summary>
        /// Avg. Time
        /// </summary>
        public static readonly ThreadMetric AverageTime = new("Avg. Time");

        /// <summary>
        /// Fastest
        /// </summary>
        public static readonly ThreadMetric Fastest = new("Fastest");

        /// <summary>
        /// Slowest
        /// </summary>
        public static readonly ThreadMetric Slowest = new("Slowest");

        /// <summary>
        /// Total Time
        /// </summary>
        public static readonly ThreadMetric TotalTime = new("Total Time");

        /// <summary>
        /// Throughput
        /// </summary>
        public static readonly ThreadMetric Throughput = new("Throughput");

        /// <summary>
        /// Memory Initial size
        /// </summary>
        public static readonly ThreadMetric MemoryInitialSize = new("Memory Initial size");

        /// <summary>
        /// Memory End size
        /// </summary>
        public static readonly ThreadMetric MemoryEndSize = new("Memory End size");

        /// <summary>
        /// Memory Increase
        /// </summary>
        public static readonly ThreadMetric MemoryIncrease = new("Memory Increase");
    }
}
