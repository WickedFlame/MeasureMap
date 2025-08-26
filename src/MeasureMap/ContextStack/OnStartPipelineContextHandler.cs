using System;

namespace MeasureMap.ContextStack
{
    public class OnStartPipelineContextHandler : BaseContextHandler
    {
        private readonly int _threadNumber;
        private readonly ProfilerSettings _settings;
        private readonly Func<ProfilerSettings, IExecutionContext> _onStartPipeline;

        public OnStartPipelineContextHandler(int threadNumber, ProfilerSettings settings, Func<ProfilerSettings, IExecutionContext> onStartPipeline)
        {
            _threadNumber = threadNumber;
            _settings = settings;
            _onStartPipeline = onStartPipeline;
        }

        /// <summary>
        /// Run the pipeline
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="context"></param>
        /// <returns>The resulting collection of the executions</returns>
        public override IResult Run(ITask task, IExecutionContext context)
        {
            var ctx = _onStartPipeline(_settings);
            if (ctx == null)
            {
                ctx = context;
            }

            ctx.Set(ContextKeys.ThreadNumber, _threadNumber);

            var result = base.Run(task, ctx);

            return result;
        }
    }
}
