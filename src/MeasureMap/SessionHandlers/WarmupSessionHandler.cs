using MeasureMap.ContextStack;
using System.Diagnostics;

namespace MeasureMap
{
    /// <summary>
    /// Warmup for the task
    /// </summary>
    public interface IWarmupSessionHandler : ISessionHandler
    {
    }

    /// <summary>
    /// Warmup for the task
    /// </summary>
    public class WarmupSessionHandler : SessionHandler, IWarmupSessionHandler
    {
        public WarmupSessionHandler() 
            : this(new DefaultContextStackBuilder())
        {
        }

        public WarmupSessionHandler(IContextStackBuilder runnerFactory)
        {
            RunnerFactory = runnerFactory;
        }

        public IContextStackBuilder RunnerFactory { get; }

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

            var ws = new ProfilerSettings { Iterations = 1, IsWarmup = true };
            
            var runner = RunnerFactory.Create(0, ws);
            runner.Run(task, ws.CreateContext());

            stopwatch.Stop();

            var result = base.Execute(task, settings);

            result.ResultValues.Add("Warmup", stopwatch.Elapsed);

            return result;
        }
    }
}
