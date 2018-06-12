using System;

namespace MeasureMap
{
    /// <summary>
    /// Taskhandler that executes a task after each profiling task execution
    /// </summary>
    public class AfterExecutionTaskHandler : BaseTaskHandler
    {
        private readonly Action _task;

        /// <summary>
        /// Creates a new taskhandler
        /// </summary>
        /// <param name="task">Task to execute after each profiling task execution</param>
        public AfterExecutionTaskHandler(Action task)
        {
            _task = task;
        }

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="iteration">The current iteration</param>
        /// <returns>The resulting collection of the executions</returns>
        public override IIterationResult Run(int iteration)
        {
            var result = base.Run(iteration);

            _task();

            return result;
        }
    }
}
