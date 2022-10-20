using MeasureMap.Diagnostics;
using MeasureMap.Threading;
using System.Collections.Generic;

namespace MeasureMap
{
    /// <summary>
    /// The context containing info to the execution run
    /// </summary>
    public interface IExecutionContext
    {
        /// <summary>
        /// The data store for the context
        /// </summary>
        IDictionary<string, object> SessionData { get; }

        /// <summary>
        /// Gets a list containing all threads that are associated with this run
        /// </summary>
        IThreadList Threads { get; }

        /// <summary>
        /// Gets the settings associated with the session
        /// </summary>
        IProfilerSettings Settings { get; }

        /// <summary>
        /// Gets the logger associated with the session
        /// </summary>
        ILogger Logger { get; }
    }
}
