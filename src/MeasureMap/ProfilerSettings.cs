using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MeasureMap
{
    public class ProfilerSettings
    {
        private readonly Dictionary<string, Action<ProfilerSettings, ProfilerSettings>> _changes = new Dictionary<string, Action<ProfilerSettings, ProfilerSettings>>();

        private int _iterations = 1;
        private bool _runWarmup = true;

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
