using System;

namespace MeasureMap.ContextStack
{
    /// <summary>
    /// Builder to create a Context Stack that is run per thread
    /// Creates a new Instance of the <see cref="IContextMiddleware"/> per thread
    /// </summary>
    public interface IContextStackBuilder
    {
        /// <summary>
        /// Add a new middleware to the Context Stack
        /// </summary>
        /// <param name="middleware"></param>
        void Add(Func<int, ProfilerSettings, IContextMiddleware> middleware);

        /// <summary>
        /// Create a new instance of the Context Stack
        /// </summary>
        /// <param name="threadNumber"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        IContextMiddleware Create(int threadNumber, ProfilerSettings settings);
    }
}
