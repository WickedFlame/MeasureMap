using System;
using System.Collections.Generic;
using MeasureMap.Diagnostics;
using MeasureMap.RunnerHandlers;

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
        private IThreadSessionHandler _executor;

        private readonly ISessionHandler _sessionPipeline;
        private readonly ProcessingPipeline _processingPipeline;
        private readonly IPipelineRunnerFactory _runnerPipeline;

        private readonly ProfilerSettings _settings;

        private ProfilerSession()
        {
            _settings = new ProfilerSettings();
            _assertions = new List<Func<IResult, bool>>();
            _executor = new BasicSessionHandler();

            _sessionPipeline = new TaskExecutionChain();
            _sessionPipeline.SetNext(new ElapsedTimeSessionHandler());

            _processingPipeline = new ProcessingPipeline();

            _runnerPipeline = new DefaultPipelineRunnerFactory();
        }

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
        /// Gets the processing pipeline containing the middleware that get executed for every iteration. The task is executed at the top of the executionchain.
        /// </summary>
        public ITaskMiddleware ProcessingPipeline => _processingPipeline;

        public IPipelineRunnerFactory RunnerPipeline => _runnerPipeline;

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
        public ProfilerSession SetExecutionHandler(IThreadSessionHandler handler)
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
                throw new ArgumentException($"The Task that has to be processed is null or not set.");
            }

            if(_settings.RunWarmup)
            {
                _sessionPipeline.SetNext(new WarmupSessionHandler(RunnerPipeline));
            }

            //
            // The executor has to be the last element added to the session pipeline
            // The executor runs the processing pipeline
            _executor.RunnerFactory = RunnerPipeline;
            _sessionPipeline.SetNext(_executor);


            //_executor.se
            //_executor.RunnerFactory = 

            _processingPipeline.SetNext(new ProcessDataTaskHandler());
            _processingPipeline.SetNext(new MemoryCollectionTaskHandler());
            _processingPipeline.SetNext(new ElapsedTimeTaskHandler());
            _processingPipeline.SetNext(_task);

            var threads = _executor is MultyThreadSessionHandler handler ? handler.ThreadCount : 1;

            Settings.Logger.Write($"Running {Settings.Iterations} Iterations on {threads} Threads", LogLevel.Debug, "ProfilerSession");

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
