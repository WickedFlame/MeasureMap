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
        /// <returns>The resulting collection of the executions</returns>
        public virtual IIterationResult Run()
        {
            var output = _task.Run(0);
            var iteration = new IterationResult()
            {
                Data = output
            };

            return iteration;
        }
    }
}
