using System;
using System.Collections.Generic;

namespace MeasureMap
{
    public interface IProfilerResult
    {
        long AverageMilliseconds { get; }

        long AverageTicks { get; }

        TimeSpan AverageTime { get; }
        
        ProfileIteration Fastest { get; }

        long Increase { get; }

        long InitialSize { get; }

        long EndSize { get; }

        IEnumerable<ProfileIteration> Iterations { get; }

        ProfileIteration Slowest { get; }

        TimeSpan TotalTime { get; }

        /// <summary>
        /// Duration of the warmup
        /// </summary>
        TimeSpan Warmup { get; }
    }
}