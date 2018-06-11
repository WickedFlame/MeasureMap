using System;

namespace MeasureMap
{
    /// <summary>
    /// Chainofresponnsibility Manager for executing tasks
    /// </summary>
    public class TaskExecutionChain : ITaskExecutor
    {
        private ITaskExecutor _root;
        private ITaskExecutor _last;

        /// <summary>
        /// Set the next execution item
        /// </summary>
        /// <param name="next">The next executor</param>
        public void SetNext(ITaskExecutor next)
        {
            if(_root == null)
            {
                _root = next;
                _last = next;
            }
            else
            {
                _last.SetNext(next);
            }

            _last = next;
        }

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="iterations">The iterations to run the task</param>
        /// <returns>The resulting collection of the executions</returns>
        public IProfilerResult Execute(ITask task, int iterations)
        {
            return _root.Execute(task, iterations);
        }
    }
}
