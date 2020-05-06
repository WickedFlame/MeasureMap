using System;

namespace MeasureMap
{
    /// <summary>
    /// SessionHandler that executes a task before the TaskExecution
    /// </summary>
    public class PreExecutionSessionHandler : SessionHandler
    {
        private readonly Action _action;

        /// <summary>
        /// Creates a SessionHandler that executes a task before the TaskExecution
        /// </summary>
        /// <param name="action"></param>
        public PreExecutionSessionHandler(Action action)
        {
            _action = action;
        }

        /// <summary>
        /// Execute the task
        /// </summary>
        /// <param name="task">The task to execute</param>
        /// <param name="iterations">The iterations for executing the task</param>
        /// <returns></returns>
        public override IProfilerResult Execute(ITask task, int iterations)
        {
            _action.Invoke();

            return base.Execute(task, iterations);
        }
    }
}
