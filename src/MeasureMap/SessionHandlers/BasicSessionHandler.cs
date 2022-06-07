
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

            var worker = new Worker();
            var p = worker.Run(task, settings);

            return new ProfilerResult
            {
                p
            };
        }
    }
}
