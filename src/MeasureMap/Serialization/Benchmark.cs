using System;

namespace MeasureMap.Serialization
{
    /// <summary>
    /// Represents the aggregated results of a benchmark run, including identity, thread context, timing statistics, and
    /// size growth metrics.
    /// </summary>
    /// <remarks>Captures average, total, fastest, and slowest execution times, along with initial and final
    /// sizes and their increase, for performance analysis and comparison.</remarks>
    public class Benchmark
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ThreadId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ThreadNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long AverageTicks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double AverageMilliseconds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan AverageTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan TotalTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan Fastest { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan Slowest { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long Increase { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long InitialSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long EndSize { get; set; }
    }
}
