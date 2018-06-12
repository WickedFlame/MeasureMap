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
        
        IIterationResult Fastest { get; }

        long Increase { get; }

        long InitialSize { get; }

        long EndSize { get; }

        IEnumerable<IIterationResult> Iterations { get; }

        IIterationResult Slowest { get; }

        TimeSpan TotalTime { get; }
    }

    public interface IProfilerResult : IResult, IEnumerable<IResult>
    {
    }
}