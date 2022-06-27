using System;

namespace MeasureMap.Runners
{
    /// <summary>
    /// Simple and direct execution ot the task
    /// </summary>
    public class SimpleTaskExecution : ITaskExecution
    {
        /// <summary>
        /// Executes the task directly
        /// </summary>
        /// <param name="context"></param>
        /// <param name="execution"></param>
        public void Execute(IExecutionContext context, Action<IExecutionContext> execution)
        {
            execution(context);
        }
    }
}
