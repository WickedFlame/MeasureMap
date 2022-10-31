using System;
using System.Threading;

namespace MeasureMap.Threading
{
    /// <summary>
    /// Thread to run the Worker in
    /// </summary>
    public interface IWorkerThread : IDisposable
    {
        /// <summary>
        /// Get the final result of the thread
        /// </summary>
        Result Result { get; }

        /// <summary>
        /// Get the Id of the thread
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets if the thread is still working
        /// </summary>
        bool IsAlive { get; }

        /// <summary>
        /// Gets a <see cref="System.Threading.WaitHandle"/> that is used to wait for the event to be set.
        /// </summary>
        /// <remarks>
        /// <see cref="WaitHandle"/> should only be used if it's needed for integration with code bases that rely on having a WaitHandle.
        /// </remarks>
        public WaitHandle WaitHandle { get; }

        /// <summary>
        /// Start the thread
        /// </summary>
        void Start();
    }
}