﻿using System;
using MeasureMap.Threading;

namespace MeasureMap
{
    /// <summary>
    /// A worker that runs the provided tasks
    /// </summary>
    public class Worker
    {
        /// <summary>
        /// Creates a new instance of the worker
        /// </summary>
        public Worker()
        {
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
            var context = new ExecutionContext(settings);

            var runner = settings.Runner;
            runner.Run(settings, context, c =>
            {
                var iteration = task.Run(c);

                result.Add(iteration);
            });

            result.EndSize = GC.GetTotalMemory(true);
            ForceGarbageCollector();

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
