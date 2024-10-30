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
            // warmup
            settings.Logger.Write($"Running Task once for warmup on Performance Analysis Benchmark", source: nameof(WarmupSessionHandler));

            var stopwatch = Stopwatch.StartNew();

            settings.IsWarmup = true;
            var ctx = settings.OnStartPipeline();

            task.Run(ctx);

            stopwatch.Stop();
            settings.OnEndPipeline(ctx);

            settings.IsWarmup = false;

            var result = base.Execute(task, settings);

            result.ResultValues.Add("Warmup", stopwatch.Elapsed);

            return result;
        }
    }
}
