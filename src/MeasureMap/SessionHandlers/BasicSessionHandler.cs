using System;
using MeasureMap.Threading;
using MeasureMap.Diagnostics;

namespace MeasureMap
{
    /// <summary>
    /// A single threaded task session handler. Runs all tasks in a separate thread.
    /// </summary>
    public class BasicSessionHandler : SessionHandler, IThreadSessionHandler
    {
        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="settings">The settings for the profiler</param>
        /// <returns>The resulting collection of the executions</returns>
        public override IProfilerResult Execute(ITask task, ProfilerSettings settings)
        {
            var runnerThreads = new WorkerThreadList();
            
            var thread = runnerThreads.StartNew(0, _ =>
            {
                var worker = new Worker();
                return worker.Run(task, settings.OnStart());
            }, settings.GetThreadFactory());
            
            settings.Logger.Write($"Start thread {thread.Id}", LogLevel.Debug, nameof(BasicSessionHandler));

            runnerThreads.WaitAll();

            return new ProfilerResult
            {
                thread.Result
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
