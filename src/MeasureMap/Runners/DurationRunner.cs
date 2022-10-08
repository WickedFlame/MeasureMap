using System;
using System.Diagnostics;

namespace MeasureMap.Runners
{
    /// <summary>
    /// Runner that runs the task for a given duration
    /// </summary>
    public class DurationRunner : ITaskRunner
    {
        /// <summary>
        /// Runs the task for the given duration that is defined in the <see cref="ProfilerSettings"/>
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="context"></param>
        /// <param name="action"></param>
        public void Run(ProfilerSettings settings, IExecutionContext context, Action<IExecutionContext> action)
        {
            settings.Logger.Write($"Running Task for {settings.Duration} for Perfomance Analysis Benchmark", source: nameof(DurationRunner));
            var stopWatch = new Stopwatch();

            var execution = settings.Execution;

            var duration = settings.Duration.TotalMilliseconds;

            var iteration = 1;
            stopWatch.Start();
            while (stopWatch.Elapsed.TotalMilliseconds < duration)
            {
                settings.Logger.Write($"Running Task for iteration {iteration}", source: nameof(DurationRunner));
                context.Set(ContextKeys.Iteration, iteration);

                execution.Execute(context.Clone(), action);

                iteration++;
            }

            settings.Logger.Write($"Running {iteration} iterations took {stopWatch.Elapsed}");
        }
    }
}
