using System;

namespace MeasureMap
{
    public class TaskHandler : ITaskHandler
    {
        private ITask _task;

        public TaskHandler(ITask task)
        {
            _task = task;
        }
        /// <summary>
        /// Set the next execution item
        /// </summary>
        /// <param name="next">The next executor</param>
        public void SetNext(ITaskHandler next)
        {
            throw new InvalidOperationException("TaskHandler is not allowed to have a child handler");
        }

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="context">The current execution context</param>
        /// <returns>The resulting collection of the executions</returns>
        public IIterationResult Run(IExecutionContext context)
        {
            var iteration = context.Get<int>(ContextKeys.Iteration);
            var output = _task.Run(iteration);
            var result = new IterationResult()
            {
                Data = output
            };

            return result;
        }
    }
}
