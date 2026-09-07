using System;

namespace MeasureMap.Serialization
{
    public class Benchmark
    {
        public string Name { get; set; }

        public int ThreadId { get; set; }

        public int ThreadNumber { get; set; }

        public long AverageTicks { get; set; }

        public double AverageMilliseconds { get; set; }

        public TimeSpan AverageTime { get; set; }

        public TimeSpan TotalTime { get; set; }

        public TimeSpan Fastest { get; set; }

        public TimeSpan Slowest { get; set; }

        public long Increase { get; set; }

        public long InitialSize { get; set; }

        public long EndSize { get; set; }
    }
}
