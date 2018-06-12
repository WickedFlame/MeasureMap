using System;

namespace MeasureMap
{
    /// <summary>
    /// Taskhandler that executes a task before each profiling task execution
    /// </summary>
    public class BeforeExecutionTaskHandler : BaseTaskHandler
    {
        private readonly Action _task;

        /// <summary>
        /// Creates a new taskhandler
        /// </summary>
        /// <param name="task">Task to execute before each profiling task execution</param>
        public BeforeExecutionTaskHandler(Action task)
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
            _task();

            var result = base.Run(iteration);

            return result;
        }
    }
}
