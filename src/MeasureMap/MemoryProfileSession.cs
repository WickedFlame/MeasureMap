using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasureMap
{
    public class MemoryProfileSession : ProfilerSession
    {
        private int _iterations = 1;
        private Action _task;

        private MemoryProfileSession()
        {
        }

        /// <summary>
        /// Creates a new Session for profiling memory
        /// </summary>
        /// <returns>A profiler session</returns>
        public static MemoryProfileSession StartSession()
        {
            return new MemoryProfileSession();
        }

        /// <summary>
        /// Adds the Task that will be profiled
        /// </summary>
        /// <param name="task">The Task</param>
        /// <returns>The current profiling session</returns>
        public MemoryProfileSession Task(Action task)
        {
            _task = task;

            return this;
        }

        /// <summary>
        /// Sets the amount of iterations that the profileing session should run the task
        /// </summary>
        /// <param name="iterations">The iterations to run the task</param>
        /// <returns>The current profiling session</returns>
        public MemoryProfileSession SetIterations(int iterations)
        {
            _iterations = iterations;

            return this;
        }

        /// <summary>
        /// Starts the profiling session
        /// </summary>
        /// <returns>The resulting profile</returns>
        public MemoryResult RunSession()
        {
            if (_task == null)
            {
                throw new ArgumentNullException("task", "The Task that has to be processed is null or not set.");
            }

            // warmup
            Trace.WriteLine("Running Task once for warmup on Memory Analysis Benchmark");
            _task();

            var profile = new MemoryResult();

            SetProcessor();
            SetThreadPriority();
            ClearGarbageCollector();

            profile.InitialSize = GC.GetTotalMemory(true);

            Trace.WriteLine(string.Format("Running Task for {0} iterations for Memory Analysis Benchmark", _iterations));
            for (int i = 0; i < _iterations; i++)
            {
                var initial = GC.GetTotalMemory(true);

                _task();

                var after = GC.GetTotalMemory(false);

                ClearGarbageCollector();

                var afterCollect = GC.GetTotalMemory(true);

                profile.Add(new MemoryIteration(initial, after, afterCollect));
            }

            ClearGarbageCollector();
            profile.EndSize = GC.GetTotalMemory(true);

            return profile;
        }
    }
}
