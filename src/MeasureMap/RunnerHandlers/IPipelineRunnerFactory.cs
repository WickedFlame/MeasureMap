namespace MeasureMap.RunnerHandlers
{
    public interface IPipelineRunnerFactory
    {
        IRunnerMiddleware Create(int threadNumber, ProfilerSettings settings);
    }
}
