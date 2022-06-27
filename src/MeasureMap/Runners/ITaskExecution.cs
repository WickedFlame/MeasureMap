using System;

namespace MeasureMap.Runners
{
    /// <summary>
    /// Defines tha way a task is executed
    /// </summary>
    public interface ITaskExecution
    {
        /// <summary>
        /// Execute the task
        /// </summary>
        /// <param name="context"></param>
        /// <param name="execution"></param>
        void Execute(IExecutionContext context, Action execution);
    }
}
