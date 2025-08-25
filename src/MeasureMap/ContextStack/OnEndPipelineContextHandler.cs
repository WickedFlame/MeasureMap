using System;

namespace MeasureMap.ContextStack
{
    /// <summary>
    /// 
    /// </summary>
    public class OnEndPipelineContextHandler : IContextMiddleware
    {
        private readonly Action<IExecutionContext> _onEndPipelineEvent;
        private IContextMiddleware _next;

        public OnEndPipelineContextHandler(Action<IExecutionContext> onEndPipelineEvent)
        {
            _onEndPipelineEvent = onEndPipelineEvent;
        }

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
        public IResult Run(ITask task, IExecutionContext context)
        {
            var result = _next != null ? _next.Run(task, context) : null;

            _onEndPipelineEvent(context);

            return result;
        }
    }
}
