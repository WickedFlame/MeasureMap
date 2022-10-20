using MeasureMap.Diagnostics;

namespace MeasureMap
{
    /// <summary>
    /// Settings for the profiler
    /// </summary>
    public interface IProfilerSettings
    {
        /// <summary>
        /// Gets the <see cref="ILogger"/>
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// Gets the amount of iterations that the Task will be run
        /// </summary>
        int Iterations { get; }

        /// <summary>
        /// Gets or sets if the Warmup should be run when executing the profile session
        /// </summary>
        bool RunWarmup { get; }
    }
}
