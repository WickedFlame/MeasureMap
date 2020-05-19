using System;
using System.Collections.Generic;
using MeasureMap.Diagnostics;

namespace MeasureMap
{
    //
    // TODO: Memorytests: http://www.codeproject.com/Articles/5171/Advanced-Unit-Testing-Part-IV-Fixture-Setup-Teardo
    // 

    /// <summary>
    /// The main entry for a profiling session
    /// </summary>
    public class ProfilerSession
    {
        private readonly List<Func<IResult, bool>> _assertions;
        private ITask _task;
        private IThreadSessionHandler _executor;

        private readonly ISessionHandler _sessionPipeline;
        private readonly ProcessingPipeline _processingPipeline;
        private readonly ProfilerSettings _settings;

        private ProfilerSession()
        {
            _settings = new ProfilerSettings();
            _assertions = new List<Func<IResult, bool>>();
            _executor = new ThreadSessionHandler();

            _sessionPipeline = new TaskExecutionChain();
            _sessionPipeline.SetNext(new ElapsedTimeSessionHandler());

            _processingPipeline = new ProcessingPipeline();
        }

        /// <summary>
        /// Gets the amount of iterations that the Task will be run
        /// </summary>
        public int Iterations => _settings.Iterations;

        /// <summary>
        /// Gets the settings for this profiler
        /// </summary>
        public ProfilerSettings Settings => _settings;

        /// <summary>
        /// Gets the chain of handlers that get executed before the task execution
        /// </summary>
        [Obsolete("Use SessionPipeline")]
        public ISessionHandler SessionHandler => _sessionPipeline;

        /// <summary>
        /// Gets the chain of handlers that get executed before the execution of the ProcessingPipeline
        /// </summary>
        public ISessionHandler SessionPipeline => _sessionPipeline;

        /// <summary>
        /// Gets the chain of handlers that get executed when running every task
        /// </summary>
        [Obsolete("Use ProcessingPipeline")]
        public ITaskMiddleware TaskHandler => _processingPipeline;

        /// <summary>
        /// Gets the processing pipeline containing the middleware that get executed for every itteration. The task is executed at the top of the executionchain.
        /// </summary>
        public ITaskMiddleware ProcessingPipeline => _processingPipeline;

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
            _settings.Iterations = iterations;

            return this;
        }

        /// <summary>
        /// Sets the amount of threads that the profiling sessions should run in.
        /// All iterations are run on every thread.
        /// </summary>
        /// <param name="thredCount">The amount of threads that the task is run on</param>
        /// <returns>The current profiling session</returns>
        public ProfilerSession SetThreads(int thredCount)
        {
            _executor = new MultyThreadSessionHandler(thredCount);

            return this;
        }
        
        /// <summary>
        /// Sets the Taskrunner that will be profiled
        /// </summary>
        /// <param name="runner">The runner containig the task</param>
        /// <returns>The current profiling session</returns>
        public ProfilerSession Task(ITask runner)
        {
            _task = runner;

            return this;
        }

        /// <summary>
        /// Adds a condition to the profiling session
        /// </summary>
        /// <param name="condition">The condition that will be checked</param>
        /// <returns>The current profiling session</returns>
        [Obsolete("Use Assert", false)]
        public ProfilerSession AddCondition(Func<IResult, bool> condition) => Assert(condition);

        /// <summary>
        /// Adds a condition to the profiling session
        /// </summary>
        /// <param name="assertion">The condition that will be checked</param>
        /// <returns>The current profiling session</returns>
        public ProfilerSession Assert(Func<IResult, bool> assertion)
        {
            _assertions.Add(assertion);

            return this;
        }
        
        /// <summary>
        /// Starts the profiling session
        /// </summary>
        /// <returns>The resulting profile</returns>
        public IProfilerResult RunSession()
        {
            if (_task == null)
            {
                throw new ArgumentNullException($"task", $"The Task that has to be processed is null or not set.");
            }

            _sessionPipeline.SetNext(new WarmupSessionHandler());
            _sessionPipeline.SetNext(_executor);

            _processingPipeline.SetNext(new ProcessDataTaskHandler());
            _processingPipeline.SetNext(new MemoryCollectionTaskHandler());
            _processingPipeline.SetNext(new ElapsedTimeTaskHandler());
            _processingPipeline.SetNext(_task);

            //var profiles = _executor.Execute(_task, _iterations);
            var profiles = _sessionPipeline.Execute(_processingPipeline, _settings);
            
            foreach (var condition in _assertions)
            {
                foreach (var profile in profiles)
                {
                    if (!condition(profile))
                    {
                        throw new AssertionException($"Condition failed: {condition}");
                    }
                }
            }
            
            return profiles;
        }
    }
}
