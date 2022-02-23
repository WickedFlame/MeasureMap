using System;
using System.Diagnostics;
using System.Threading;
using MeasureMap.Diagnostics;
using MeasureMap.Runners;

namespace MeasureMap
{
    /// <summary>
    /// A worker that runs the provided tasks
    /// </summary>
    public class Worker
    {
        /// <summary>
        /// Creates a new instance of the worker
        /// </summary>
        public Worker()
        {
        }

        /// <summary>
        /// Runs the provided task for the iteration count
        /// </summary>
        /// <param name="task">The task that has to be run</param>
        /// <param name="settings">The settings for the profiler</param>
        /// <returns></returns>
        public Result Run(ITask task, ProfilerSettings settings)
        {
            var result = new Result();

            ForceGarbageCollector();
            
            result.InitialSize = GC.GetTotalMemory(true);
            var context = new ExecutionContext();

            var runner = settings.Runner;
            runner.Run(settings, context, () =>
            {
                var iteration = task.Run(context);

                result.Add(iteration);
            });

            ForceGarbageCollector();
            result.EndSize = GC.GetTotalMemory(true);

            return result;
        }
        
        /// <summary>
        /// Forces the GC to run
        /// </summary>
        protected void ForceGarbageCollector()
        {
            // clean up
#pragma warning disable S1215 // "GC.Collect" should not be called
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
#pragma warning restore S1215 // "GC.Collect" should not be called
        }
    }
}
