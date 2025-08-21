
namespace MeasureMap.RunnerHandlers
{
    public class DefaultPipelineRunner : IPipelineRunner
    {
        private readonly int _threadNumber;
        private readonly ProfilerSettings _settings;

        public DefaultPipelineRunner(int threadNumber, ProfilerSettings settings)
        {
            _threadNumber = threadNumber;
            _settings = settings;
        }

        public IResult Run(ITask task)
        {
            var ctx = _settings.OnStartPipeline();
            ctx.Set(ContextKeys.ThreadNumber, _threadNumber);

            var worker = new Worker();
            var result = worker.Run(task, ctx);

            _settings.OnEndPipeline(ctx);

            return result;
        }
    }
}
