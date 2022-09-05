using System;
using System.Threading;

namespace MeasureMap.Threading
{
    /// <summary>
    /// 
    /// </summary>
    public class WorkerThread : IDisposable
    {
        private readonly Thread _thread;
        private readonly ManualResetEventSlim _event;
        private bool _disposed;

        /// <summary>
        /// Create a new Thread
        /// </summary>
        /// <param name="index"></param>
        /// <param name="action"></param>
        public WorkerThread(int index, Func<Result> action)
        {
            _thread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    Result = action();
                }
                catch
                {
                    // do nothing. just move to finally
                }
                finally
                {
                    IsAlive = false;

                    // reset the event to be signaled
                    _event.Set();
                }
            }))
            {
                IsBackground = true,
                Name = $"MeasuerMap_{index}",
                Priority = ThreadPriority.Highest
            };

            _event = new ManualResetEventSlim();
            _event.Reset();
        }

        /// <summary>
        /// Get the final result of the thread
        /// </summary>
        public Result Result { get; private set; }

        /// <summary>
        /// Get the Id of the thread
        /// </summary>
        public int Id => _thread.ManagedThreadId;

        /// <summary>
        /// Gets if the thread is still working
        /// </summary>
        public bool IsAlive { get; private set; } = true;

        /// <summary>
		/// Gets a <see cref="System.Threading.WaitHandle"/> that is used to wait for the event to be set.
		/// </summary>
		/// <remarks>
		/// <see cref="WaitHandle"/> should only be used if it's needed for integration with code bases that rely on having a WaitHandle.
		/// </remarks>
		public WaitHandle WaitHandle => _event.WaitHandle;

        /// <summary>
        /// Start the thread
        /// </summary>
        public void Start()
        {
            try
            {
                _thread.Start();
            }
            catch
            {
                // do noting
            }
        }

        /// <summary>
        /// Dispose the thread
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose the class
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _thread.Interrupt();
            _disposed = true;
        }
    }
}
