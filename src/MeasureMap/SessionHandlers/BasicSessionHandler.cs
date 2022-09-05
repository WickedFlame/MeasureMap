
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
            var runnerThreads = new WorkerThreadList();
            var threads = new ThreadList();
            
            var thread = runnerThreads.StartNew(0, () =>
            {
                var worker = new Worker(threads);
                return worker.Run(task, settings);
            });

            System.Diagnostics.Trace.WriteLine($"MeasureMap - Start thread {thread.Id}");

            runnerThreads.WaitAll();
            threads.WaitAll();

            return new ProfilerResult
            {
                thread.Result
            };
        }
    }
}
