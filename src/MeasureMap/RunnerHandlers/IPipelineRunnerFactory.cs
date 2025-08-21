namespace MeasureMap.RunnerHandlers
{
    public interface IPipelineRunnerFactory
    {
        IPipelineRunner Create(int threadNumber, ProfilerSettings settings);
    }
}
