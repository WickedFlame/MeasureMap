using System;
using System.Collections.Generic;

namespace MeasureMap
{
    /// <summary>
    /// TODO: Memorytests: http://www.codeproject.com/Articles/5171/Advanced-Unit-Testing-Part-IV-Fixture-Setup-Teardo
    /// </summary>
    public class ProfilerSession
    {
        private readonly List<Func<IResult, bool>> _conditions;
        private int _iterations = 1;
        private ITask _task;
        private IThreadSessionHandler _executor;

        private readonly ISessionHandler _sessionHandler;
        private readonly TaskHandlerChain _taskHandler;

        private ProfilerSession()
        {
            _iterations = 1;
            _conditions = new List<Func<IResult, bool>>();
            _executor = new ThreadSessionHandler();

            _sessionHandler = new TaskExecutionChain();
            _sessionHandler.SetNext(new ElapsedTimeSessionHandler());

            _taskHandler = new TaskHandlerChain();
        }

        /// <summary>
        /// Gets the amount of iterations that the Task will be run
        /// </summary>
        public int Iterations => _iterations;

        /// <summary>
        /// Gets the chain of handlers that get executed before the task execution
        /// </summary>
        public ISessionHandler SessionHandler => _sessionHandler;

        /// <summary>
        /// Gets the chain of handlers that get executed when running every task
        /// </summary>
        public ITaskHandler TaskHandler => _taskHandler;

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
        public ProfilerSession AddCondition(Func<IResult, bool> condition)
        {
            _conditions.Add(condition);

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

            _sessionHandler.SetNext(new WarmupSessionHandler());
            _sessionHandler.SetNext(_executor);

            _taskHandler.SetNext(new ProcessDataTaskHandler());
            _taskHandler.SetNext(new MemoryCollectionTaskHandler());
            _taskHandler.SetNext(new ElapsedTimeTaskHandler());
            _taskHandler.SetNext(_task);

            //var profiles = _executor.Execute(_task, _iterations);
            var profiles = _sessionHandler.Execute(_taskHandler, _iterations);
            
            foreach (var condition in _conditions)
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
