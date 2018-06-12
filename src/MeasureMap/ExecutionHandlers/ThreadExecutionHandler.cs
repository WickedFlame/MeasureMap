﻿using System.Collections.Generic;
using System.Linq;

namespace MeasureMap
{
    /// <summary>
    /// defines a mechanism to execute the task
    /// </summary>
    public interface IThreadExecutionHandler : IExecutionHandler
    {
    }

    /// <summary>
    /// A single threaded task executor
    /// </summary>
    public class ThreadExecutionHandler : ExecutionHandler, IThreadExecutionHandler
    {
        /// <summary>
        /// Executes the task on a single thread
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="iterations">The iterations to run the task</param>
        /// <returns>The resulting collection of the executions</returns>
        public override IProfilerResult Execute(ITask task, int iterations)
        {
            ThreadHelper.SetProcessor();
            ThreadHelper.SetThreadPriority();

            var worker = new Worker();
            var p = worker.Run(task, iterations);

            return new ProfilerResult
            {
                p
            };
        }
    }

    /// <summary>
    /// A threaded task executor
    /// </summary>
    public class MultyThreadExecutionHandler : ExecutionHandler, IThreadExecutionHandler
    {
        private readonly int _threadCount;

        /// <summary>
        /// Creates a new threaded task executor
        /// </summary>
        /// <param name="threadCount">The amount of threads to run the task</param>
        public MultyThreadExecutionHandler(int threadCount)
        {
            _threadCount = threadCount;
        }

        /// <summary>
        /// Executes the task on multiple threads
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="iterations">The iterations to run the task</param>
        /// <returns>The resulting collection of the executions</returns>
        public override IProfilerResult Execute(ITask task, int iterations)
        {
            var threads = new List<System.Threading.Tasks.Task<Result>>();

            for (int i = 0; i < _threadCount; i++)
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

            var collectîon = new ProfilerResult();
            foreach (var result in results)
            {
                collectîon.Add(result);
            }

            return collectîon;
        }
    }
}