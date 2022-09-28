using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Thread = System.Threading.Tasks.Task;

namespace MeasureMap.Threading
{
    /// <summary>
    /// List of threads
    /// </summary>
    public class ThreadList : IThreadList
    {
        private readonly List<Thread> _threads = new List<Thread>();

        /// <summary>
        /// Start the action in a new thread
        /// </summary>
        /// <param name="execution"></param>
        public void StartNew(Action execution)
        {
            var thread = Thread.Factory.StartNew(execution);
            Add(thread);
        }

        /// <summary>
        /// Add a task to the threadlist
        /// </summary>
        /// <param name="task"></param>
        public void Add(Thread task)
        {
            lock (_threads)
            {
                _threads.Add(task);

                task.ContinueWith(t =>
                {
                    // make sure the thread is completed
                    t.ConfigureAwait(false).GetAwaiter().GetResult();
                    Remove(t);
                });
            }
        }

        /// <summary>
        /// Remove a task from the threadlist
        /// </summary>
        /// <param name="task"></param>
        public void Remove(Thread task)
        {
            lock (_threads)
            {
                if (_threads.Contains(task))
                {
                    _threads.Remove(task);
                }
            }
        }

        /// <summary>
        /// Wait for all threads to end
        /// </summary>
        public void WaitAll()
        {
            while (Count() > 0)
            {
                Debug.WriteLine($"[{DateTime.Now:o}] [MeasureMap] [Debug] [{nameof(ThreadList)}] Task count before waitall: {Count()}");

                Thread.WaitAll(GetTaskArray(), -1, CancellationToken.None);

                Debug.WriteLine($"[{DateTime.Now:o}] [MeasureMap] [Debug] [{nameof(ThreadList)}] Task count after waitall: {Count()}");
            }
        }

        private Thread[] GetTaskArray()
        {
            lock (_threads)
            {
                return _threads.ToArray();
            }
        }

        /// <summary>
        /// Gets the count of threads in the list
        /// </summary>
        /// <returns></returns>
        private int Count()
        {
            lock (_threads)
            {
                return _threads.Count;
            }
        }
    }
}
