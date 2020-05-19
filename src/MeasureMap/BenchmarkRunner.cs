using System;
using System.Collections.Generic;

namespace MeasureMap
{
    /// <summary>
    /// Runs multiple sessions to create benchmarks
    /// </summary>
    public class BenchmarkRunner
    {
        private readonly ProfilerSettings _settings;
        private readonly Dictionary<string, ProfilerSession> _sessions;

        /// <summary>
        /// Creates an instance of the BenchmaekRunner
        /// </summary>
        public BenchmarkRunner()
        {
            _settings = new ProfilerSettings();
            _sessions = new Dictionary<string, ProfilerSession>();
        }

        /// <summary>
        /// Gets the settings for the benchmarks
        /// </summary>
        public ProfilerSettings Settings => _settings;

        /// <summary>
        /// Add a new session to run benchmarks against
        /// </summary>
        /// <param name="name"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public BenchmarkRunner AddSession(string name, ProfilerSession session)
        {
            _sessions.Add(name, session);

            return this;
        }

        /// <summary>
        /// Run all sessions and benchmarks
        /// </summary>
        /// <returns></returns>
        public IProfilerResultCollection RunSessions()
        {
            var results = new ProfilerResultCollection(_settings.Iterations);

            foreach (var key in _sessions.Keys)
            {
                var session = _sessions[key];
                session.SetSettings(_settings);
                var result = session.RunSession();
                results.Add(key, result);
            }

            return results;
        }
    }
}
