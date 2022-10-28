using MeasureMap.Diagnostics;
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
        /// Gets the settings associated with the session
        /// </summary>
        IProfilerSettings Settings { get; }

        /// <summary>
        /// Gets the logger associated with the session
        /// </summary>
        ILogger Logger { get; }
    }
}
