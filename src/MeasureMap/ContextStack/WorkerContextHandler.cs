using System;

namespace MeasureMap.ContextStack
{
    /// <summary>
    /// 
    /// </summary>
    public class WorkerContextHandler : IContextMiddleware
    {
        /// <summary>
        /// Set the next execution item
        /// </summary>
        /// <param name="next">The next handler for the thread pipeline</param>
        public void SetNext(IContextMiddleware next)
        {
            throw new InvalidOperationException("The WorkerHandler cannot contain a child Middleware");
        }

        /// <summary>
        /// Run the pipeline
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="context"></param>
        /// <returns>The resulting collection of the executions</returns>
        public IResult Run(ITask task, IExecutionContext context)
        {
            var worker = new Worker();
            var result = worker.Run(task, context);

            return result;
        }
    }
}
