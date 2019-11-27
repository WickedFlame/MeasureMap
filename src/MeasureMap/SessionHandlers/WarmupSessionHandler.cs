using System.Diagnostics;
using MeasureMap.Diagnostics;

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
        private readonly Logger _logger;

        /// <summary>
        /// Creates an instance of the WarmupSessionHandler
        /// </summary>
        public WarmupSessionHandler()
        {
            _logger = Logger.Setup();
        }

        /// <summary>
        /// Executes a warmup for the task
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="iterations">The iterations to run the task</param>
        /// <returns>The resulting collection of the executions</returns>
        public override IProfilerResult Execute(ITask task, int iterations)
        {
            var stopwatch = new Stopwatch();

            // warmup
            _logger.Write($"Running Task once for warmup on Performance Analysis Benchmark");

            stopwatch.Start();

            task.Run(new ExecutionContext());

            stopwatch.Stop();

            var result = base.Execute(task, iterations);

            result.ResultValues.Add("Warmup", stopwatch.Elapsed);

            return result;
        }
    }
}
