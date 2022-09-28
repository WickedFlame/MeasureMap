using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MeasureMap.Threading
{
    /// <summary>
    /// Represents a list of WorkerThreads
    /// </summary>
    public class WorkerThreadList : IEnumerable<WorkerThread>
    {
        private readonly List<WorkerThread> _threads = new List<WorkerThread>();

        /// <summary>
        /// Start a new thread and add it to the list
        /// </summary>
        /// <param name="index"></param>
        /// <param name="execution"></param>
        /// <returns></returns>
        public WorkerThread StartNew(int index, Func<Result> execution)
        {
            var thread = new WorkerThread(index, () =>
            {
                var result = execution.Invoke();

                return result;
            });

            Add(thread);
            
            thread.Start();

            return thread;
        }

        /// <summary>
        /// Add a task to the threadlist
        /// </summary>
        /// <param name="thread"></param>
        public void Add(WorkerThread thread)
        {
            lock (_threads)
            {
                _threads.Add(thread);
            }
        }

        /// <summary>
        /// Remove a task from the threadlist
        /// </summary>
        /// <param name="thread"></param>
        public void Remove(WorkerThread thread)
        {
            lock (_threads)
            {
                if (_threads.Contains(thread))
                {
                    _threads.Remove(thread);
                }
            }
        }

        /// <summary>
        /// Wait for all threads to end
        /// </summary>
        public void WaitAll()
        {
            var threads = GetTaskArray();
            while (threads.Length > 0)
            {
                Debug.WriteLine($"[{DateTime.Now:o}] [MeasureMap] [Debug] [{nameof(WorkerThreadList)}] Task count before waitall: {threads.Length}");

                for(var i = 0; i < threads.Length; i++)
                {
                    var thread = threads[i];
                    thread.WaitHandle.WaitOne(-1);
                }

                threads = GetTaskArray();

                Debug.WriteLine($"[{DateTime.Now:o}] [MeasureMap] [Debug] [{nameof(WorkerThreadList)}] Task count after waitall: {threads.Length}");
            }
        }

        private WorkerThread[] GetTaskArray()
        {
            lock (_threads)
            {
                return _threads.Where(t => t.IsAlive).ToArray();
            }
        }

        /// <summary>
        /// Get the enumerator for the list of WorkerThread
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerator<WorkerThread> GetEnumerator()
        {
            return _threads.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
