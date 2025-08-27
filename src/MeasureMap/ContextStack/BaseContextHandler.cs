namespace MeasureMap.ContextStack
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseContextHandler : IContextMiddleware
    {
        private IContextMiddleware _next;

        /// <summary>
        /// Set the next execution item
        /// </summary>
        /// <param name="next">The next handler for the thread pipeline</param>
        public void SetNext(IContextMiddleware next)
        {
            if (_next != null)
            {
                _next.SetNext(next);
                return;
            }

            _next = next;
        }

        /// <summary>
        /// Run the pipeline
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="context"></param>
        /// <returns>The resulting collection of the executions</returns>
        public virtual IResult Run(ITask task, IExecutionContext context)
        {
            return _next != null ? _next.Run(task, context) : null;
        }
    }
}
