using System;
using MeasureMap.Diagnostics;

namespace MeasureMap.Runners
{
    /// <summary>
    /// Runner that runs the task for a given amount of iterations
    /// </summary>
    public class IterationRunner : ITaskRunner
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Creates a new instance of the worker
        /// </summary>
        public IterationRunner()
        {
            _logger = Logger.Setup();
        }

        /// <summary>
        /// Runs the task for the given amount of iterations that are defined in the <see cref="ProfilerSettings"/>
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="context"></param>
        /// <param name="action"></param>
        public void Run(ProfilerSettings settings, IExecutionContext context, Action<IExecutionContext> action)
        {
            _logger.Write($"Running Task for {settings.Iterations} iterations for Perfomance Analysis Benchmark");

            var execution = settings.Execution;

            for (var i = 0; i < settings.Iterations; i++)
            {
                _logger.Write($"Running Task for iteration {i + 1}");
                context.Set(ContextKeys.Iteration, i + 1);

                execution.Execute(context.Clone(), action);
            }
        }
    }
}
