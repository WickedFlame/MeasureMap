using System;
using System.Reflection;
using System.Threading;

namespace MeasureMap.Threading
{
    /// <summary>
    /// Worker that uses <see cref="System.Threading.Tasks.Task"/> to run Benchmarks
    /// </summary>
    public class WorkerTask : IWorkerThread
    {
        private System.Threading.Tasks.Task _task;
        private readonly ManualResetEventSlim _event;
        private bool _disposed;
        private readonly Func<int, IResult> _action;
        private readonly int _index;

        private readonly CancellationTokenSource _cancellationToken;

        /// <summary>
        /// Gets the factory for creating a new WorkerTask
        /// </summary>
        public static Func<int, Func<int, IResult>, IWorkerThread> Factory => (i, e) => new WorkerTask(i, e);

        /// <summary>
        /// Create a new Thread
        /// </summary>
        /// <param name="index"></param>
        /// <param name="action"></param>
        public WorkerTask(int index, Func<int, IResult> action)
        {
            _event = new ManualResetEventSlim();
            _cancellationToken = new CancellationTokenSource();
            _index = index;
            _action = action;

            _event.Reset();
        }

        /// <summary>
        /// Get the final result of the thread
        /// </summary>
        public IResult Result { get; private set; }

        /// <summary>
        /// Get the Id of the thread
        /// </summary>
        public int Id => _task?.Id ?? 0;

        /// <summary>
        /// Gets if the thread is still working
        /// </summary>
        public bool IsAlive => _task is { Status: <= System.Threading.Tasks.TaskStatus.Running };
        
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
            _task = System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                try
                {
                    Result = _action(_index);
                }
                catch
                {
                    // do nothing. just move to finally
                }
                finally
                {
                    // reset the event to be signaled
                    _event.Set();
                }
            }, _cancellationToken.Token, System.Threading.Tasks.TaskCreationOptions.None, System.Threading.Tasks.TaskScheduler.Current);
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

            _disposed = true;
            _cancellationToken.Cancel();
        }
    }
}
