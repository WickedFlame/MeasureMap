
namespace MeasureMap
{
    /// <summary>
    /// Chain of responsibility for executing tasks
    /// </summary>
    public interface ISessionHandler
    {
        /// <summary>
        /// Set the next execution item
        /// </summary>
        /// <param name="next">The next handler for the session</param>
        void SetNext(ISessionHandler next);

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="iterations">The iterations to run the task</param>
        /// <returns>The resulting collection of the executions</returns>
        IProfilerResult Execute(ITask task, int iterations);
    }
}
