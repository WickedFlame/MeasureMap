
namespace MeasureMap
{
    /// <summary>
    /// Represents a task handler that can be chained together with further task handlers
    /// </summary>
    public interface ITaskHandler
    {
        /// <summary>
        /// Set the next execution item
        /// </summary>
        /// <param name="next">The next executor</param>
        void SetNext(ITaskHandler next);

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="context">The current execution context</param>
        /// <returns>The resulting collection of the executions</returns>
        IIterationResult Run(IExecutionContext context);
    }
}
