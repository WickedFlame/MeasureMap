using System.Collections.Generic;
using System.Linq;
using MeasureMap.Diagnostics;

namespace MeasureMap
{
    /// <summary>
    /// defines a mechanism to execute the task
    /// </summary>
    public interface IThreadSessionHandler : ISessionHandler
    {
    }

    /// <summary>
    /// A single threaded task session handler
    /// </summary>
    public class ThreadSessionHandler : SessionHandler, IThreadSessionHandler
    {
        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="settings">The settings for the profiler</param>
        /// <returns>The resulting collection of the executions</returns>
        public override IProfilerResult Execute(ITask task, ProfilerSettings settings)
        {
            ThreadHelper.SetProcessor();
            ThreadHelper.SetThreadPriority();

            var worker = new Worker();
            var p = worker.Run(task, settings);

            return new ProfilerResult
            {
                p
            };
        }
    }

    /// <summary>
    /// A multy threaded task session handler
    /// </summary>
    public class MultyThreadSessionHandler : SessionHandler, IThreadSessionHandler
    {
        private readonly int _threadCount;
        
        /// <summary>
        /// Creates a new threaded task executor
        /// </summary>
        /// <param name="threadCount">The amount of threads to run the task</param>
        public MultyThreadSessionHandler(int threadCount)
        {
            _threadCount = threadCount;
        }

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="settings">The settings for the profiler</param>
        /// <returns>The resulting collection of the executions</returns>
        public override IProfilerResult Execute(ITask task, ProfilerSettings settings)
        {
            var threads = new List<System.Threading.Tasks.Task<Result>>();

            for (int i = 0; i < _threadCount; i++)
            {
                var thread = ThreadHelper.QueueTask(i, threadIndex =>
                {
                    var worker = new Worker();
                    var p = worker.Run(task, settings);
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
