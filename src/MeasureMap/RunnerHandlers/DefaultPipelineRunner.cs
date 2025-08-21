
namespace MeasureMap.RunnerHandlers
{
    public class DefaultPipelineRunner : IRunnerMiddleware
    {
        private readonly int _threadNumber;
        private readonly ProfilerSettings _settings;
        private IRunnerMiddleware _next;

        public DefaultPipelineRunner(int threadNumber, ProfilerSettings settings)
        {
            _threadNumber = threadNumber;
            _settings = settings;
        }

        /// <summary>
        /// Set the next execution item
        /// </summary>
        /// <param name="next">The next handler for the thread pipeline</param>
        public void SetNext(IRunnerMiddleware next)
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
            //var ctx = _settings.OnStartPipeline();
            //ctx.Set(ContextKeys.ThreadNumber, _threadNumber);

            //var worker = new Worker();
            //var result = worker.Run(task, ctx);

            //_settings.OnEndPipeline(ctx);

            var result = _next.Run(task, context);

            return result;
        }
    }
}
