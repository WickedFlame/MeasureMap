using MeasureMap.ContextStack;
using System;

namespace MeasureMap
{
    /// <summary>
    /// defines a mechanism to execute the task
    /// </summary>
    public interface IThreadSessionHandler : ISessionHandler, IDisposable
    {
        IContextStackBuilder RunnerFactory { get; set; }
    }
}
