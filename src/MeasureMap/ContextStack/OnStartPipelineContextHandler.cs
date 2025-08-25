using System;

namespace MeasureMap.ContextStack
{
    public class OnStartPipelineContextHandler : IContextMiddleware
    {
        private IContextMiddleware _next;
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
            var ctx = _onStartPipeline(_settings);
            if (ctx == null)
            {
                ctx = context;
            }

            ctx.Set(ContextKeys.ThreadNumber, _threadNumber);

            var result = _next != null ? _next.Run(task, ctx) : null;

            return result;
        }
    }
}
