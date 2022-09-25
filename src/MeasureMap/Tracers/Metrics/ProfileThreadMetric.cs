
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

        public static readonly ProfileThreadMetric ThreadId = new("ThreadId");
        public static readonly ProfileThreadMetric Iterations = new("Iterations");

        public static readonly ProfileThreadMetric AverageNanoseconds = new("Average Nanoseconds");
        public static readonly ProfileThreadMetric AverageMicroseconds = new("Average Microseconds");
        public static readonly ProfileThreadMetric AverageMilliseconds = new("Average Milliseconds");
        public static readonly ProfileThreadMetric AverageTicks = new("Average Ticks");
        public static readonly ProfileThreadMetric AverageTime = new("Average Time");
        public static readonly ProfileThreadMetric Fastest = new("Fastest");
        public static readonly ProfileThreadMetric Slowest = new("Slowest");
        public static readonly ProfileThreadMetric TotalTime = new("Total Time");
        public static readonly ProfileThreadMetric Throughput = new("Throughput");


        public static readonly ProfileThreadMetric MemoryInitialSize = new("Memory Initial size");
        public static readonly ProfileThreadMetric MemoryEndSize = new("Memory End size");
        public static readonly ProfileThreadMetric MemoryIncrease = new("Memory Increase");
    }
}
