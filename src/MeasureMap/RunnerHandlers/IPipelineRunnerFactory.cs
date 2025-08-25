using System;

namespace MeasureMap.RunnerHandlers
{
    public interface IPipelineRunnerFactory
    {
        void Add(Func<int, ProfilerSettings, IRunnerMiddleware> middleware);

        IRunnerMiddleware Create(int threadNumber, ProfilerSettings settings);
    }
}
