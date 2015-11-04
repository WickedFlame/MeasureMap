﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace MeasureMap
{
    /// <summary>
    /// TODO: Memorytests: http://www.codeproject.com/Articles/5171/Advanced-Unit-Testing-Part-IV-Fixture-Setup-Teardo
    /// </summary>
    public class ProfileSession : ISession
    {
        private static readonly bool IsRunningOnMono;

        private readonly List<Func<ProfileResult, bool>> _conditions;
        private int _iterations = 1;
        private Action _task;

        static ProfileSession()
        {
            IsRunningOnMono = Type.GetType("Mono.Runtime") != null;
        }

        private ProfileSession()
        {
            _iterations = 1;
            _conditions = new List<Func<ProfileResult, bool>>();
        }

        public int Iterations
        {
            get
            {
                return _iterations;
            }
        }
        
        public static ProfileSession StartSession()
        {
            return new ProfileSession();
        }
        
        /// <summary>
        /// Sets the amount of iterations that the profileing session should run the task
        /// </summary>
        /// <param name="iterations">The iterations to run the task</param>
        /// <returns>The current profiling session</returns>
        public ProfileSession SetIterations(int iterations)
        {
            _iterations = iterations;

            return this;
        }

        /// <summary>
        /// Adds the Task that will be profiled
        /// </summary>
        /// <param name="task">The Task</param>
        /// <returns>The current profiling session</returns>
        public ProfileSession Task(Action task)
        {
            _task = task;

            return this;
        }

        /// <summary>
        /// Adds a condition to the profiling session
        /// </summary>
        /// <param name="condition">The condition that will be checked</param>
        /// <returns>The current profiling session</returns>
        public ProfileSession AddCondition(Func<ProfileResult, bool> condition)
        {
            _conditions.Add(condition);

            return this;
        }

        /// <summary>
        /// Starts the profiling session
        /// </summary>
        /// <returns>The resulting profile</returns>
        public ProfileResult RunSession()
        {
            if (_task == null)
            {
                throw new ArgumentNullException("task", "The Task that has to be processed is null or not set.");
            }

            // warmup
            Trace.WriteLine("Running Task once for warmup on Performance Analysis Benchmark");
            _task();

            var profile = new ProfileResult();
            var stopwatch = new Stopwatch();

            SetProcessor();
            
            Thread.CurrentThread.Priority = ThreadPriority.Highest; // Prevents "Normal" Threads from interrupting this thread

            // clean up
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Trace.WriteLine(string.Format("Running Task for {0} iterations for Perfomance Analysis Benchmark", _iterations));
            for (int i = 0; i < _iterations; i++)
            {
                stopwatch.Reset();
                stopwatch.Start();

                _task();

                stopwatch.Stop();

                Trace.WriteLine(string.Format("Task ran for {0} Ticks {1} Milliseconds", stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds));

                profile.Add(new Iteration(stopwatch.ElapsedTicks, stopwatch.Elapsed));
            }

            foreach (var condition in _conditions)
            {
                if (!condition(profile))
                {
                    throw new AssertionException(string.Format("Condition failed: {0}", condition.ToString()));
                }
            }

            Trace.WriteLine(string.Format("Running Task for {0} iterations with an Average of {1} Milliseconds", _iterations, profile.AverageMilliseconds));

            return profile;
        }
        
        private void SetProcessor()
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
                    Trace.WriteLine("Could not set Task to run on second Core or Processor");
                }

                // Prevents "Normal" processes from interrupting Threads
                process.PriorityClass = ProcessPriorityClass.High; 
            }
        }
    }
}
