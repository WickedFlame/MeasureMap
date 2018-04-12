using System;
using System.Collections.Generic;

namespace MeasureMap
{
    public interface IProfilerResult
    {
        long AverageMilliseconds { get; }

        long AverageTicks { get; }

        TimeSpan AverageTime { get; }

        long EndSize { get; set; }

        ProfileIteration Fastest { get; }

        long Increase { get; }

        long InitialSize { get; set; }

        IEnumerable<ProfileIteration> Iterations { get; }

        ProfileIteration Slowest { get; }

        TimeSpan TotalTime { get; }
    }
}