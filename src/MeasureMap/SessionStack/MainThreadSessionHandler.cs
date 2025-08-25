using System;
using MeasureMap.ContextStack;
using MeasureMap.Diagnostics;

namespace MeasureMap.SessionStack
{
    /// <summary>
    /// Runs all tasks in the main thread. This can only be used when running on one thread. Multithreaded handling cannot be run on the main thread
    /// </summary>
    public class MainThreadSessionHandler : SessionHandler, ISessionExecutor
    {
        /// <summary>
        /// Gets or sets the <see cref="IContextStackBuilder"/> to create the ContextStack that runs the task
        /// </summary>
        public IContextStackBuilder StackBuilder { get; set; } = new DefaultContextStackBuilder();

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="settings">The settings for the profiler</param>
        /// <returns>The resulting collection of the executions</returns>
        public override IProfilerResult Execute(ITask task, ProfilerSettings settings)
        {
            settings.Logger.Write($"Start on mainthread", LogLevel.Debug, nameof(BasicSessionHandler));

            var runner = StackBuilder.Create(0, settings);
            var result = runner.Run(task, settings.CreateContext());

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
