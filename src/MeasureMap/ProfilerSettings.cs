﻿using System;
using System.Collections.Generic;
using MeasureMap.Diagnostics;
using MeasureMap.Runners;
using MeasureMap.Threading;

namespace MeasureMap
{
    /// <summary>
    /// Settings for the profiler
    /// </summary>
    public class ProfilerSettings : IProfilerSettings
    {
        private readonly Dictionary<string, Action<ProfilerSettings, ProfilerSettings>> _changes = new Dictionary<string, Action<ProfilerSettings, ProfilerSettings>>();

        private int _iterations = 1;
        private bool _runWarmup = true;
        private TimeSpan _duration = TimeSpan.Zero;
        private ThreadBehaviour _threadBehaviour = ThreadBehaviour.Thread;

        /// <summary>
        /// 
        /// </summary>
        public ProfilerSettings()
        {
            Logger = Diagnostics.Logger.Setup();
        }

        /// <summary>
        /// Gets the <see cref="ILogger"/>
        /// </summary>
        public ILogger Logger { get; }

        /// <summary>
        /// Gets or sets the amount of iterations that the Task will be run
        /// </summary>
        public int Iterations
        {
            get => _iterations;
            set
            {
                _iterations = value;
                AddChange("iterations", s => s.Iterations, (s, v) => s.Iterations = v);
                Runner = new IterationRunner();
            }
        }

        /// <summary>
        /// Gets or sets the duration that the Task will be run for
        /// </summary>
        public TimeSpan Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                AddChange("duration", s => s.Duration, (s, v) => s.Duration = v);
                Runner = new DurationRunner();
            }
        }

        /// <summary>
        /// Gets or sets if the Warmup should be run when executing the profile session
        /// </summary>
        public bool RunWarmup
        {
            get => _runWarmup;
            set
            {
                _runWarmup = value;
                AddChange("runwarmup", s => s.RunWarmup, (s, v) => s.RunWarmup = v);
            }
        }

        /// <summary>
        /// Defines the way threads are created
        /// </summary>
        public ThreadBehaviour ThreadBehaviour
        {
            get => _threadBehaviour;
            set
            {
                _threadBehaviour = value;
                AddChange("threadBehaviour", s => s.ThreadBehaviour, (s, v) => s.ThreadBehaviour = v);
            }
        }

        /// <summary>
        /// Gets an indicator if the current run is a warmup run
        /// </summary>
        public bool IsWarmup { get; internal set; } = false;

        /// <summary>
        /// Gets the <see cref="ITaskRunner"/> that is used to run the tasks
        /// </summary>
        public ITaskRunner Runner { get; private set; } = new IterationRunner();

        /// <summary>
        /// Gets the <see cref="ITaskExecution"/> that defines how the tasks are run
        /// </summary>
        public ITaskExecution Execution { get; internal set; } = new SimpleTaskExecution();

        /// <summary>
        /// Gets the event that is executed at the start of each thread run. Is also used to create a <see cref="IExecutionContext"/>
        /// </summary>
        public Func<ProfilerSettings, IExecutionContext> OnStartPipelineEvent { get; internal set; } = s => new ExecutionContext(s);

        /// <summary>
        /// Gets the event that is executed at the end of each thread run
        /// </summary>
        public Action<IExecutionContext> OnEndPipelineEvent { get; internal set; } = e => { };

        /// <summary>
        /// Execute the OnStartPipeline event
        /// </summary>
        /// <returns></returns>
        public IExecutionContext OnStartPipeline()
        {
            if(OnStartPipelineEvent == null)
            {
                return new ExecutionContext(this);
            }

            Logger.Write("Starting Pipeline with OnStartPieline Event", LogLevel.Debug);

            return OnStartPipelineEvent(this);
        }

        /// <summary>
        /// Execute the OnEndPipeline event
        /// </summary>
        /// <param name="context"></param>
        public void OnEndPipeline(IExecutionContext context)
        {
            if (OnEndPipelineEvent == null)
            {
                return;
            }

            OnEndPipelineEvent(context);
        }

        private void AddChange<T>(string property, Func<ProfilerSettings, T> func, Action<ProfilerSettings, T> action)
        {
            _changes[property] = (toSet, fromSet) => action(toSet, func(fromSet));
        }

        internal void MergeChangesTo(ProfilerSettings settings)
        {
            foreach (var action in _changes.Values)
            {
                // settings = to
                // this = from
                action(settings, this);
            }
        }
    }
}
