using MeasureMap.RunnerHandlers;
using System;

namespace MeasureMap
{
    /// <summary>
    /// defines a mechanism to execute the task
    /// </summary>
    public interface IThreadSessionHandler : ISessionHandler, IDisposable
    {
        IPipelineRunnerFactory RunnerFactory { get; set; }
    }
}
