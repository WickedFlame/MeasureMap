using MeasureMap.ContextStack;
using System;

namespace MeasureMap.SessionStack
{
    /// <summary>
    /// defines a mechanism to execute the task
    /// </summary>
    public interface ISessionExecutor : ISessionMiddleware, IDisposable
    {
        /// <summary>
        /// Gets or sets the <see cref="IContextStackBuilder"/> to create the ContextStack that runs the task
        /// </summary>
        IContextStackBuilder StackBuilder { get; set; }
    }
}
