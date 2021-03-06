﻿using System;
using System.Diagnostics;
using System.Threading;
using MeasureMap.Diagnostics;

namespace MeasureMap
{
    /// <summary>
    /// A worker that runs the provided tasks
    /// </summary>
    public class Worker
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Creates a new instance of the worker
        /// </summary>
        public Worker()
        {
            _logger = Logger.Setup();
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

            _logger.Write($"Running Task for {settings.Iterations} iterations for Perfomance Analysis Benchmark");

            result.InitialSize = GC.GetTotalMemory(true);
            var context = new ExecutionContext();

            for (int i = 0; i < settings.Iterations; i++)
            {
                _logger.Write($"Running Task for iteration {i}");
                context.Set(ContextKeys.Iteration, i);

                var iteration = task.Run(context);

                result.Add(iteration);
            }

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
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
