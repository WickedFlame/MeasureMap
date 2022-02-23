using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using MeasureMap.Diagnostics;

namespace MeasureMap
{
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
}
