
namespace MeasureMap
{
    /// <summary>
    /// Base task handler containing logic for calling the next task in the chain
    /// </summary>
    public abstract class BaseTaskHandler : ITaskHandler
    {
        private ITaskHandler _next;

        /// <summary>
        /// Set the next execution item
        /// </summary>
        /// <param name="next">The next executor</param>
        public void SetNext(ITaskHandler next)
        {
            _next = next;
        }

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="context">The current execution context</param>
        /// <returns>The resulting collection of the executions</returns>
        public virtual IIterationResult Run(IExecutionContext context)
        {
            if(_next == null)
            {
                return new IterationResult();
            }

            return _next.Run(context);
        }
    }
}
