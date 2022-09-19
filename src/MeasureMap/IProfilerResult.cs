using System;
using System.Collections.Generic;

namespace MeasureMap
{
    /// <summary>
    /// Result for multiple profiles
    /// </summary>
    public interface IProfilerResult : IResult, IEnumerable<IResult>
    {
    }
}