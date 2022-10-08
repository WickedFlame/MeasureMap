using System;
using MeasureMap.Threading;

namespace MeasureMap
{
    /// <summary>
    /// A worker that runs the provided tasks
    /// </summary>
    public class Worker
    {
        private readonly IThreadList _threads;

        /// <summary>
        /// Creates a new instance of the worker
        /// </summary>
        public Worker() : this(new ThreadList())
        {
        }

        /// <summary>
        /// Creates a new instance of the worker
        /// </summary>
        /// <param name="threads"></param>
        public Worker(IThreadList threads)
        {
            _threads = threads ?? throw new ArgumentNullException(nameof(threads));
        }

        /// <summary>
        /// Runs the provided task for the iteration count
        /// </summary>
        /// <param name="task">The task that has to be run</param>
        /// <param name="settings">The settings for the profiler</param>
        /// <returns></returns>
        public Result Run(ITask task, ProfilerSettings settings)
        {
            var result = new Result();

            ForceGarbageCollector();
            
            result.InitialSize = GC.GetTotalMemory(true);
            var context = new ExecutionContext(settings)
            {
                Threads = _threads
            };

            var runner = settings.Runner;
            runner.Run(settings, context, c =>
            {
                var iteration = task.Run(c);

                result.Add(iteration);
            });

            ForceGarbageCollector();
            result.EndSize = GC.GetTotalMemory(true);

            return result;
        }
        
        /// <summary>
        /// Forces the GC to run
        /// </summary>
        protected void ForceGarbageCollector()
        {
            // clean up
#pragma warning disable S1215 // "GC.Collect" should not be called
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
#pragma warning restore S1215 // "GC.Collect" should not be called
        }
    }
}
