using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasureMap
{
    public interface ITaskExecutor
    {
        /// <summary>
        /// Set the next execution item
        /// </summary>
        /// <param name="next">The next executor</param>
        void SetNext(ITaskExecutor next);

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="iterations">The iterations to run the task</param>
        /// <returns>The resulting collection of the executions</returns>
        ProfilerResultCollection Execute(ITaskRunner task, int iterations);
    }

    public class TaskExecutor : ITaskExecutor
    {
        private ITaskExecutor _next;

        /// <summary>
        /// Set the next execution item
        /// </summary>
        /// <param name="next">The next executor</param>
        public void SetNext(ITaskExecutor next)
        {
            _next = next;
        }

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="iterations">The iterations to run the task</param>
        /// <returns>The resulting collection of the executions</returns>
        public virtual ProfilerResultCollection Execute(ITaskRunner task, int iterations)
        {
            return _next.Execute(task, iterations);
        }
    }
}
