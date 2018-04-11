using System.Collections.Generic;
using System.Linq;

namespace MeasureMap
{
    /// <summary>
    /// defines a mechanism to execute the task
    /// </summary>
    public interface ITaskExecutor
    {
        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="iterations">The iterations to run the task</param>
        /// <returns>The resulting collection of the executions</returns>
        ProfilerResultCollection Execute(ITaskRunner task, int iterations);
    }

    /// <summary>
    /// A single threaded task executor
    /// </summary>
    public class SingleTaskExecutor : ITaskExecutor
    {
        /// <summary>
        /// Executes the task on a single thread
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="iterations">The iterations to run the task</param>
        /// <returns>The resulting collection of the executions</returns>
        public ProfilerResultCollection Execute(ITaskRunner task, int iterations)
        {
            var worker = new Worker();
            var p = worker.Run(task, iterations);

            return new ProfilerResultCollection
            {
                p
            };
        }
    }

    /// <summary>
    /// A threaded task executor
    /// </summary>
    public class ThreadedTaskExecutor : ITaskExecutor
    {
        private readonly int _threadCount;

        /// <summary>
        /// Creates a new threaded task executor
        /// </summary>
        /// <param name="threadCount">The amount of threads to run the task</param>
        public ThreadedTaskExecutor(int threadCount)
        {
            _threadCount = threadCount;
        }

        /// <summary>
        /// Executes the task on multiple threads
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="iterations">The iterations to run the task</param>
        /// <returns>The resulting collection of the executions</returns>
        public ProfilerResultCollection Execute(ITaskRunner task, int iterations)
        {
            var threads = new List<System.Threading.Tasks.Task<ProfilerResult>>();

            for (int i = 0; i < 5; i++)
            {
                var thread = ThreadHelper.QueueTask(i, threadIndex =>
                {
                    var worker = new Worker();
                    var p = worker.Run(task, iterations);
                    return p;
                });

                threads.Add(thread);
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }

            System.Threading.Tasks.Task.WaitAll(threads.ToArray());

            var results = threads.Select(s => s.Result);

            var collectîon = new ProfilerResultCollection();
            foreach (var result in results)
            {
                collectîon.Add(result);
            }

            return collectîon;
        }
    }
}
