using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MeasureMap.Attributes;

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
        public IBenchmarkResult RunSessions()
        {
            var results = new BenchmarkResult(_settings);

            foreach (var key in _sessions.Keys)
            {
                _settings.Logger.Write($"Starting Benchmarks for {key}", Diagnostics.LogLevel.Info, nameof(BenchmarkRunner));

                var session = _sessions[key];
                session.SetSettings(_settings);
                var result = session.RunSession();
                results.Add(key, result);
            }

            return results;
        }

        /// <summary>
        /// Run all benchmarks based on the Attributes defined in the benchmark class 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IBenchmarkResult RunSession<T>() where T : class, new()
        {
            var builder = new AttributeSessionBuilder<T>(this);
            builder.BuildSessions();
            return RunSessions();
        }
    }
}
