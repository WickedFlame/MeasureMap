using System;
using System.Collections.Generic;

namespace MeasureMap
{
    public interface IResult
    {
        IDictionary<string, object> ResultValues { get; }

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
    }

    public interface IProfilerResult : IResult, IEnumerable<IResult>
    {
    }
}