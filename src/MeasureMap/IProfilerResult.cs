using System.Collections.Generic;

namespace MeasureMap
{
    /// <summary>
    /// Result for multiple profiles
    /// Each <see cref="IResult"/> in the Enumerator represents the results of a thread
    /// </summary>
    public interface IProfilerResult : IResult, IEnumerable<IResult>
    {
    }
}