using System;

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
        /// <param name="context">The executioncontext that is passed to the tasks</param>
        /// <returns></returns>
        public Result Run(ITask task, IExecutionContext context)
        {
            var result = new Result();

            ForceGarbageCollector();

            result.InitialSize = GC.GetTotalMemory(true);

            var runner = context.Settings.Runner;
            runner.Run(context.Settings, context, c =>
            {
                var iterationResult = task.Run(c);

                result.Add(iterationResult);
            });

            result.EndSize = GC.GetTotalMemory(true);
            ForceGarbageCollector();

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
