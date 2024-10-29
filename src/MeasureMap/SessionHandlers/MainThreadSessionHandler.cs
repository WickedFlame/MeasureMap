using System;
using MeasureMap.Diagnostics;

namespace MeasureMap
{
    /// <summary>
    /// Runs all tasks in the main thread. This can only be used when running on one thread. Multithreaded handling cannot be run on the main thread
    /// </summary>
    public class MainThreadSessionHandler : SessionHandler, IThreadSessionHandler
    {
        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="settings">The settings for the profiler</param>
        /// <returns>The resulting collection of the executions</returns>
        public override IProfilerResult Execute(ITask task, ProfilerSettings settings)
        {
            settings.Logger.Write($"Start on mainthread", LogLevel.Debug, nameof(BasicSessionHandler));

            var worker = new Worker();
            var result = worker.Run(task, settings.OnStart());
            
            return new ProfilerResult
            {
                result
            };
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            // nothing to dispose
        }
    }
}
