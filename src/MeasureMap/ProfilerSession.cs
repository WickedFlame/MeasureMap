using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace MeasureMap
{
    /// <summary>
    /// TODO: Memorytests: http://www.codeproject.com/Articles/5171/Advanced-Unit-Testing-Part-IV-Fixture-Setup-Teardo
    /// </summary>
    public class ProfilerSession
    {
        private readonly List<Func<ProfilerResult, bool>> _conditions;
        private int _iterations = 1;
        private Action _task;

        private static readonly bool IsRunningOnMono;

        static ProfilerSession()
        {
            IsRunningOnMono = Type.GetType("Mono.Runtime") != null;
        }

        private ProfilerSession()
        {
            _iterations = 1;
            _conditions = new List<Func<ProfilerResult, bool>>();
        }

        /// <summary>
        /// Gets the amount of iterations that the Task will be run
        /// </summary>
        public int Iterations
        {
            get
            {
                return _iterations;
            }
        }
        
        /// <summary>
        /// Creates a new Session for profiling performance
        /// </summary>
        /// <returns>A profiler session</returns>
        public static ProfilerSession StartSession()
        {
            return new ProfilerSession();
        }
        
        /// <summary>
        /// Sets the amount of iterations that the profileing session should run the task
        /// </summary>
        /// <param name="iterations">The iterations to run the task</param>
        /// <returns>The current profiling session</returns>
        public ProfilerSession SetIterations(int iterations)
        {
            _iterations = iterations;

            return this;
        }

        /// <summary>
        /// Adds the Task that will be profiled
        /// </summary>
        /// <param name="task">The Task</param>
        /// <returns>The current profiling session</returns>
        public ProfilerSession Task(Action task)
        {
            _task = task;

            return this;
        }

        /// <summary>
        /// Adds a condition to the profiling session
        /// </summary>
        /// <param name="condition">The condition that will be checked</param>
        /// <returns>The current profiling session</returns>
        public ProfilerSession AddCondition(Func<ProfilerResult, bool> condition)
        {
            _conditions.Add(condition);

            return this;
        }

        /// <summary>
        /// Starts the profiling session
        /// </summary>
        /// <returns>The resulting profile</returns>
        public ProfilerResult RunSession()
        {
            if (_task == null)
            {
                throw new ArgumentNullException($"task", $"The Task that has to be processed is null or not set.");
            }

            // warmup
            Trace.WriteLine($"Running Task once for warmup on Performance Analysis Benchmark");
            _task();

            var profile = new ProfilerResult();
            var stopwatch = new Stopwatch();

            SetProcessor();
            SetThreadPriority();
            ForceGarbageCollector();

            Trace.WriteLine($"Running Task for {_iterations} iterations for Perfomance Analysis Benchmark");

            profile.InitialSize = GC.GetTotalMemory(true);

            for (int i = 0; i < _iterations; i++)
            {
                var initial = GC.GetTotalMemory(true);

                stopwatch.Reset();
                stopwatch.Start();

                _task();

                stopwatch.Stop();

                var after = GC.GetTotalMemory(false);
                ForceGarbageCollector();
                var afterCollect = GC.GetTotalMemory(true);
                
                profile.Add(new ProfileIteration(stopwatch.ElapsedTicks, stopwatch.Elapsed, initial, after, afterCollect));
            }

            ForceGarbageCollector();
            profile.EndSize = GC.GetTotalMemory(true);

            foreach (var condition in _conditions)
            {
                if (!condition(profile))
                {
                    throw new AssertionException($"Condition failed: {condition}");
                }
            }

            Trace.WriteLine($"Running Task for {_iterations} iterations with an Average of { profile.AverageMilliseconds} Milliseconds");

            return profile;
        }

        /// <summary>
        /// Sets the process to run on second core with high priority
        /// </summary>
        protected void SetProcessor()
        {
            if (!IsRunningOnMono)
            {
                var process = Process.GetCurrentProcess();

                try
                {
                    // Uses the second Core or Processor for the Test
                    process.ProcessorAffinity = new IntPtr(2);
                }
                catch (Exception)
                {
                    Trace.WriteLine($"Could not set Task to run on second Core or Processor");
                }

                // Prevents "Normal" processes from interrupting Threads
                process.PriorityClass = ProcessPriorityClass.High;
            }
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

        /// <summary>
        /// Sets the thread priority to highest
        /// </summary>
        protected void SetThreadPriority()
        {
            // Prevents "Normal" Threads from interrupting this thread
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
        }
    }
}
