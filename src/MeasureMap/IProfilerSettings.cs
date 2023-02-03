using MeasureMap.Diagnostics;
using MeasureMap.Runners;
using System;

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

        /// <summary>
        /// Gets the <see cref="ITaskRunner"/> that is used to run the tasks
        /// </summary>
        ITaskRunner Runner { get; }

        /// <summary>
        /// Gets or sets the duration that the Task will be run for
        /// </summary>
        TimeSpan Duration { get; set; }

        /// <summary>
        /// Gets the <see cref="ITaskExecution"/> that defines how the tasks are run
        /// </summary>
        ITaskExecution Execution { get; }
    }
}
