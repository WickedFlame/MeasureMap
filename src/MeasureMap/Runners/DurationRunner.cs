using System;
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
        public void Run(ProfilerSettings settings, ExecutionContext context, Action action)
        {
            _logger.Write($"Running Task for {settings.Duration} for Perfomance Analysis Benchmark");
            var time = DateTime.Now + settings.Duration;

            var iteration = 1;
            while(DateTime.Now < time)
            {
                _logger.Write($"Running Task for iteration {iteration}");
                context.Set(ContextKeys.Iteration, iteration);
                action();

                iteration++;
            }
        }
    }
}
