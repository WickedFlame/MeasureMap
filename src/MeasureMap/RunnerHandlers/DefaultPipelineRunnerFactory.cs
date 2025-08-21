namespace MeasureMap.RunnerHandlers
{
    public class DefaultPipelineRunnerFactory : IPipelineRunnerFactory
    {
        public IPipelineRunner Create(int threadNumber, ProfilerSettings settings)
        {
            return new DefaultPipelineRunner(threadNumber, settings);
        }
    }
}
