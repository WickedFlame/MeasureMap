using System;
using MeasureMap.Threading;
using MeasureMap.Diagnostics;
using MeasureMap.RunnerHandlers;

namespace MeasureMap
{
    /// <summary>
    /// A single threaded task session handler. Runs all tasks in a separate thread.
    /// </summary>
    public class BasicSessionHandler : SessionHandler, IThreadSessionHandler
    {

        public IPipelineRunnerFactory RunnerFactory { get; set; } = new DefaultPipelineRunnerFactory();

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="settings">The settings for the profiler</param>
        /// <returns>The resulting collection of the executions</returns>
        public override IProfilerResult Execute(ITask task, ProfilerSettings settings)
        {
            var runnerThreads = new WorkerThreadList();
            
            var thread = runnerThreads.StartNew(0, i =>
            {
                var runner = RunnerFactory.Create(i, settings);
                var result = runner.Run(task, settings.CreateContext());

                return result;
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
