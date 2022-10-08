using System.Diagnostics;

namespace MeasureMap
{
    /// <summary>
    /// Warmup for the task
    /// </summary>
    public interface IWarmupSessionHandler : ISessionHandler { }

    /// <summary>
    /// Warmup for the task
    /// </summary>
    public class WarmupSessionHandler : SessionHandler, IWarmupSessionHandler
    {
        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="settings">The settings for the profiler</param>
        /// <returns>The resulting collection of the executions</returns>
        public override IProfilerResult Execute(ITask task, ProfilerSettings settings)
        {
            var stopwatch = new Stopwatch();

            // warmup
            _logger.Write($"Running Task once for warmup on Performance Analysis Benchmark", source: nameof(WarmupSessionHandler));

            stopwatch.Start();

            task.Run(new ExecutionContext(settings));

            stopwatch.Stop();

            var result = base.Execute(task, settings);

            result.ResultValues.Add("Warmup", stopwatch.Elapsed);

            return result;
        }
    }
}
