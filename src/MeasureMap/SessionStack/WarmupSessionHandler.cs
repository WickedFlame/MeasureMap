using MeasureMap.ContextStack;
using System.Diagnostics;

namespace MeasureMap.SessionStack
{
    /// <summary>
    /// Warmup for the task
    /// </summary>
    public class WarmupSessionHandler : SessionHandler, IWarmupSessionHandler
    {
        /// <summary>
        /// 
        /// </summary>
        public WarmupSessionHandler() 
            : this(new DefaultContextStackBuilder())
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="runnerFactory"></param>
        public WarmupSessionHandler(IContextStackBuilder runnerFactory)
        {
            StackBuilder = runnerFactory;
        }

        /// <summary>
        /// Gets or sets the <see cref="IContextStackBuilder"/> to create the ContextStack that runs the task
        /// </summary>
        public IContextStackBuilder StackBuilder { get; }

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
            
            var runner = StackBuilder.Create(0, ws);
            runner.Run(task, ws.CreateContext());

            stopwatch.Stop();

            var result = base.Execute(task, settings);

            result.ResultValues.Add("Warmup", stopwatch.Elapsed);

            return result;
        }
    }
}
