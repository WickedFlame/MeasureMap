
namespace MeasureMap
{
    /// <summary>
    /// Baseclass for Chain of responsibility for executing tasks
    /// </summary>
    public abstract class ExecutionHandler : IExecutionHandler
    {
        private IExecutionHandler _next;

        /// <summary>
        /// Set the next execution item
        /// </summary>
        /// <param name="next">The next executor</param>
        public void SetNext(IExecutionHandler next)
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
