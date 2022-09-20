
namespace MeasureMap.Tracers
{
    /// <summary>
    /// Tracers are used to write the result in a desired format to a output source
    /// </summary>
    public interface ITracer
    {
        /// <summary>
        /// Trace the <see cref="IProfilerResult"/>
        /// </summary>
        /// <param name="result"></param>
        /// <param name="writer"></param>
        void Trace(IProfilerResult result, IResultWriter writer);

        /// <summary>
        /// Trace the <see cref="IBenchmarkResult"/>
        /// </summary>
        /// <param name="result"></param>
        /// <param name="writer"></param>
        void Trace(IBenchmarkResult result, IResultWriter writer);
    }
}
