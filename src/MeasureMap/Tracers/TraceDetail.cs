
namespace MeasureMap.Tracers
{
    /// <summary>
    /// Enumeration for the level of detail that is traced
    /// </summary>
    public enum TraceDetail
    {
        /// <summary>
        /// Only trace the profile or benchmark results
        /// </summary>
        Minimal = 0,

        /// <summary>
        /// Trace profile results and all results per thread
        /// </summary>
        DetailPerThread = 1,

        /// <summary>
        /// Trace the profile results, thread results and the results of each iteration
        /// </summary>
        FullDetail = 2
    }
}
