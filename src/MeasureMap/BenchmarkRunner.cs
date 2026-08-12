using MeasureMap.Attributes;
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
            _sessions = [];
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
            //
            // Raise the Windows timer resolution for the whole run so that short
            // Sleep/timer based waits are not rounded up. Without this the first
            // benchmark is consistently slower than the following ones.
            using var timerScope = new HighResolutionTimerScope();

            if (_settings.RunWarmup)
            {
                // Clone the settings for the Session Warmup
                var clone = _settings.Clone();
                clone.RunWarmup = false;

                // Distribute at least 30 warmup iterations across all sessions.
                // Rounding up guarantees the combined total is never below 30.
                clone.Iterations = _sessions.Count > 0
                    ? (int)Math.Ceiling(30d / _sessions.Count)
                    : 30;

                RunBenchmarks(clone);
            }

            var settings = _settings.Clone();
            settings.RunWarmup = false;
            var results = RunBenchmarks(settings);

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

        private IBenchmarkResult RunBenchmarks(ProfilerSettings settings)
        {
            var results = new BenchmarkResult(settings);

            foreach (var key in _sessions.Keys)
            {
                settings.Logger.Write($"Starting Benchmarks for {key}", Diagnostics.LogLevel.Info, nameof(BenchmarkRunner));

                var session = _sessions[key];
                session.SetSettings(settings);
                var result = session.RunSession();
                results.Add(key, result);
            }

            return results;
        }
    }
}
