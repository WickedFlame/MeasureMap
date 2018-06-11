
namespace MeasureMap
{
    /// <summary>
    /// Chainofresponsibility for executing tasks
    /// </summary>
    public interface ITaskExecutionHandler
    {
        /// <summary>
        /// Set the next execution item
        /// </summary>
        /// <param name="next">The next executor</param>
        void SetNext(ITaskExecutionHandler next);

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="iterations">The iterations to run the task</param>
        /// <returns>The resulting collection of the executions</returns>
        IProfilerResult Execute(ITask task, int iterations);
    }

    /// <summary>
    /// Baseclass for Chainofresponsibility for executing tasks
    /// </summary>
    public abstract class TaskExecutionHandler : ITaskExecutionHandler
    {
        private ITaskExecutionHandler _next;

        /// <summary>
        /// Set the next execution item
        /// </summary>
        /// <param name="next">The next executor</param>
        public void SetNext(ITaskExecutionHandler next)
        {
            _next = next;
        }

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="iterations">The iterations to run the task</param>
        /// <returns>The resulting collection of the executions</returns>
        public virtual IProfilerResult Execute(ITask task, int iterations)
        {
            return _next.Execute(task, iterations);
        }
    }
}
