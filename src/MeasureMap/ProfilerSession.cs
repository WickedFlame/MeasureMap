using System;
using System.Collections.Generic;
using System.Diagnostics;
using MeasureMap.ContextStack;
using MeasureMap.Diagnostics;
using MeasureMap.IterationStack;
using MeasureMap.SessionStack;

namespace MeasureMap
{
    //
    // TODO: Memorytests: http://www.codeproject.com/Articles/5171/Advanced-Unit-Testing-Part-IV-Fixture-Setup-Teardo
    // 

    /// <summary>
    /// The main entry for a profiling session
    /// </summary>
    public class ProfilerSession : IDisposable
    {
        private readonly List<Func<IResult, bool>> _assertions;
        private ITask _task;
        private ISessionExecutor _executor;

        private readonly ISessionMiddleware _sessionStack;
        private readonly IIterationMiddleware _iterationStack = new IterationStackBuilder();
        private readonly IContextStackBuilder _contextStack = new DefaultContextStackBuilder();

        private readonly ProfilerSettings _settings;

        private ProfilerSession()
        {
            _settings = new ProfilerSettings();
            _assertions = new List<Func<IResult, bool>>();
            _executor = new BasicSessionHandler();

            _sessionStack = new SessionStackBuilder();
            _sessionStack.SetNext(new ElapsedTimeSessionHandler());
        }

        /// <summary>
        /// Gets the settings for this profiler
        /// </summary>
        public ProfilerSettings Settings => _settings;

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("Use SessionStack instead")]
        public ISessionMiddleware SessionPipeline => _sessionStack;

        /// <summary>
        /// Gets the chain of handlers that get executed before the execution of the ProcessingPipeline
        /// </summary>
        public ISessionMiddleware SessionStack => _sessionStack;

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("Use IterationStack instead")]
        public IIterationMiddleware ProcessingPipeline => _iterationStack;

        /// <summary>
        /// Gets the processing pipeline containing the middleware that get executed for every iteration. The task is executed at the top of the executionchain.
        /// </summary>
        public IIterationMiddleware IterationStack => _iterationStack;

        /// <summary>
        /// Gets the ContextStack containing the middleware executed for each thread
        /// </summary>
        public IContextStackBuilder ContextStack => _contextStack;

        /// <summary>
        /// Creates a new Session for profiling performance
        /// </summary>
        /// <returns>A profiler session</returns>
        public static ProfilerSession StartSession()
        {
            return new ProfilerSession();
        }
        
        /// <summary>
        /// Sets the amount of threads that the profiling sessions should run in.
        /// All iterations are run on every thread.
        /// </summary>
        /// <returns>The current profiling session</returns>
        public ProfilerSession SetExecutionHandler(ISessionExecutor handler)
        {
            _executor = handler;

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
                throw new ArgumentException($"The Task that has to be processed is null or not set.");
            }

            if(_settings.RunWarmup)
            {
                _sessionStack.SetNext(new WarmupSessionHandler(ContextStack));
            }

            //
            // The executor has to be the last element added to the session pipeline
            // The executor runs the processing pipeline
            _executor.StackBuilder = ContextStack;
            _sessionStack.SetNext(_executor);


            //_executor.se
            //_executor.RunnerFactory = 

            _iterationStack.SetNext(new ProcessDataIterationHandler());
            _iterationStack.SetNext(new MemoryCollectionIterationHandler());
            _iterationStack.SetNext(new ElapsedTimeIterationHandler());
            _iterationStack.SetNext(_task);

            var threads = _executor is MultiThreadSessionHandler handler ? handler.ThreadCount : 1;

            Settings.Logger.Write($"Running {Settings.Iterations} Iterations on {threads} Threads", LogLevel.Debug, "ProfilerSession");

            var profiles = _sessionStack.Execute(_iterationStack, _settings);
            
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

        /// <summary>
        /// Dispose the class
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose the class
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            _executor.Dispose();
        }
    }
}
