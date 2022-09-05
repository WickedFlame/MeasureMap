using System;

namespace MeasureMap.Threading
{
    /// <summary>
    /// Defines a list of threads
    /// </summary>
    public interface IThreadList
    {
        /// <summary>
        /// Start the action in a new thread
        /// </summary>
        /// <param name="execution"></param>
        void StartNew(Action execution);

        /// <summary>
        /// Wait for all threads to end
        /// </summary>
        void WaitAll();
    }
}
