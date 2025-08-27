using System;

namespace MeasureMap.ContextStack
{
    /// <summary>
    /// 
    /// </summary>
    public class OnEndPipelineContextHandler : BaseContextHandler
    {
        private readonly Action<IExecutionContext> _onEndPipelineEvent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="onEndPipelineEvent"></param>
        public OnEndPipelineContextHandler(Action<IExecutionContext> onEndPipelineEvent)
        {
            _onEndPipelineEvent = onEndPipelineEvent;
        }

        /// <summary>
        /// Run the pipeline
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="context"></param>
        /// <returns>The resulting collection of the executions</returns>
        public override IResult Run(ITask task, IExecutionContext context)
        {
            var result = base.Run(task, context);

            _onEndPipelineEvent(context);

            return result;
        }
    }
}
