namespace MeasureMap.ContextStack
{
    /// <summary>
    /// Middleware for the Context Stack
    /// </summary>
    public interface IContextMiddleware
    {
        /// <summary>
        /// Set the next execution item
        /// </summary>
        /// <param name="next">The next handler for the thread pipeline</param>
        void SetNext(IContextMiddleware next);

        /// <summary>
        /// Run the pipeline
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="context"></param>
        /// <returns>The resulting collection of the executions</returns>
        IResult Run(ITask task, IExecutionContext context);
    }
}
