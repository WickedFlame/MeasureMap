using System;

namespace MeasureMap
{
    [Obsolete("Use ISessionMiddleware")]
    public interface ISessionHandler : ISessionMiddleware
    { 
    }

    /// <summary>
    /// Chain of responsibility for executing tasks
    /// </summary>
    public interface ISessionMiddleware
    {
        /// <summary>
        /// Set the next execution item
        /// </summary>
        /// <param name="next">The next handler for the session</param>
        void SetNext(ISessionMiddleware next);

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="settings">The settings for the profiler</param>
        /// <returns>The resulting collection of the executions</returns>
        IProfilerResult Execute(ITask task, ProfilerSettings settings);
    }
}
