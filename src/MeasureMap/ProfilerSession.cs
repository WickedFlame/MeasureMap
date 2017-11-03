﻿using System;
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
        private ITaskRunner _task;

        private ProfilerSession()
        {
            _iterations = 1;
            _conditions = new List<Func<ProfilerResult, bool>>();
        }

        /// <summary>
        /// Gets the amount of iterations that the Task will be run
        /// </summary>
        public int Iterations => _iterations;

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
        /// Sets the Task that will be profiled
        /// </summary>
        /// <param name="task">The Task</param>
        /// <returns>The current profiling session</returns>
        public ProfilerSession Task(Action task)
        {
            _task = new TaskRunner(task);

            return this;
        }

        /// <summary>
        /// Sets the Task that will be profiled
        /// </summary>
        /// <typeparam name="T">The return and parameter value</typeparam>
        /// <param name="task">The task to execute</param>
        /// <returns>The current profiling session</returns>
        public ProfilerSession Task<T>(Func<T,T> task)
        {
            _task = new TaskRunner<T>(task);

            return this;
        }

        /// <summary>
        /// Sets the Task that will be profiled passing the current iteration index as parameter
        /// </summary>
        /// <param name="task">The task to execute</param>
        /// <returns>The current profiling session</returns>
        public ProfilerSession Task(Action<int> task)
        {
            _task = new IteratedTaskRunner(task);

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
            
            var worker = new Worker();
            var profile = worker.Run(_task, _iterations);

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
    }
}
