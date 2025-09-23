namespace MeasureMap.IterationStack
{
    /// <summary>
    /// Taskhandler that reads the current process and thread for each profiling task execution
    /// </summary>
    public class ProcessDataIterationHandler : IterationHandler
    {
        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="context">The current execution context</param>
        /// <returns>The resulting collection of the executions</returns>
        public override IIterationResult Run(IExecutionContext context)
        {
            var result = base.Run(context);

            result.ThreadId = context.Get<int>(ContextKeys.ThreadId);
            result.ProcessId = context.Get<int>(ContextKeys.ProcessId);
            result.Iteration = context.Get<int>(ContextKeys.Iteration);
            result.ThreadNumber = context.Get<int>(ContextKeys.ThreadNumber);

            return result;
        }
    }
}
