using System;

namespace MeasureMap.IterationStack
{
    /// <summary>
    /// Taskhandler that measures the memory before and after each profiling task execution
    /// </summary>
    public class MemoryCollectionIterationHandler : IterationHandler
    {
        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="context">The current execution context</param>
        /// <returns>The resulting collection of the executions</returns>
        public override IIterationResult Run(IExecutionContext context)
        {
            var initial = GC.GetTotalMemory(true);

            var result = base.Run(context);

            result.InitialSize = initial;
            result.AfterExecution = GC.GetTotalMemory(false);

            return result;
        }
    }
}
