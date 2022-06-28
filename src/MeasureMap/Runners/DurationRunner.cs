using System;
using System.Diagnostics;
using MeasureMap.Diagnostics;

namespace MeasureMap.Runners
{
    /// <summary>
    /// Runner that runs the task for a given duration
    /// </summary>
    public class DurationRunner : ITaskRunner
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Creates a new instance of the worker
        /// </summary>
        public DurationRunner()
        {
            _logger = Logger.Setup();
        }

        /// <summary>
        /// Runs the task for the given duration that is defined in the <see cref="ProfilerSettings"/>
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="context"></param>
        /// <param name="action"></param>
        public void Run(ProfilerSettings settings, IExecutionContext context, Action<IExecutionContext> action)
        {
            _logger.Write($"Running Task for {settings.Duration} for Perfomance Analysis Benchmark");
            var stopWatch = new Stopwatch();

            var execution = settings.Execution;

            var duration = settings.Duration.TotalMilliseconds;

            var iteration = 1;
            stopWatch.Start();
            while (stopWatch.Elapsed.TotalMilliseconds < duration)
            {
                _logger.Write($"Running Task for iteration {iteration}");
                context.Set(ContextKeys.Iteration, iteration);

                execution.Execute(context.Clone(), action);

                iteration++;
            }
        }
    }
}
