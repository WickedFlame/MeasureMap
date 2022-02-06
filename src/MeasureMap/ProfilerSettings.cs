using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using MeasureMap.Runners;

namespace MeasureMap
{
    public class ProfilerSettings
    {
        private readonly Dictionary<string, Action<ProfilerSettings, ProfilerSettings>> _changes = new Dictionary<string, Action<ProfilerSettings, ProfilerSettings>>();

        private int _iterations = 1;
        private bool _runWarmup = true;
        private TimeSpan _duration;

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
                Runner = new TimeRunner();
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
        /// Gets the <see cref="ITaskRunner"/> that is used to run the tasks
        /// </summary>
        public ITaskRunner Runner { get; private set; } = new IterationRunner();


        private void AddChange<T>(string property, Func<ProfilerSettings, T> func, Action<ProfilerSettings, T> action)
        {
            _changes[property] = (origSet, newSet) => action(origSet, func(newSet));
        }

        internal void MergeChangesTo(ProfilerSettings settings)
        {
            foreach (var action in _changes.Values)
            {
                action(settings, this);
            }
        }
    }
}
