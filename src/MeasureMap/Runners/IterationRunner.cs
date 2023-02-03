using System;
using MeasureMap.Diagnostics;

namespace MeasureMap.Runners
{
    /// <summary>
    /// Runner that runs the task for a given amount of iterations
    /// </summary>
    public class IterationRunner : ITaskRunner
    {
        /// <summary>
        /// Runs the task for the given amount of iterations that are defined in the <see cref="ProfilerSettings"/>
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="context"></param>
        /// <param name="action"></param>
        public void Run(IProfilerSettings settings, IExecutionContext context, Action<IExecutionContext> action)
        {
            settings.Logger.Write($"Running Task for {settings.Iterations} iterations for Perfomance Analysis Benchmark", source: nameof(IterationRunner));

            var execution = settings.Execution;

            for (var i = 0; i < settings.Iterations; i++)
            {
                settings.Logger.Write($"Running Task for iteration {i + 1}", LogLevel.Debug, source: nameof(IterationRunner));
                context.Set(ContextKeys.Iteration, i + 1);

                execution.Execute(context.Clone(), action);
            }
        }
    }
}
