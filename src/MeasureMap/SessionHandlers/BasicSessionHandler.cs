
using MeasureMap.Threading;

namespace MeasureMap
{
    /// <summary>
    /// A single threaded task session handler
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
            ThreadHelper.SetProcessor();
            ThreadHelper.SetThreadPriority();

            var threads = new ThreadList();

            var worker = new Worker(threads);
            var p = worker.Run(task, settings);

            threads.WaitAll();

            return new ProfilerResult
            {
                p
            };
        }
    }
}
