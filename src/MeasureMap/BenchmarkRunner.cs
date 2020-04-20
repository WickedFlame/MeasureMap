using System;
using System.Collections.Generic;

namespace MeasureMap
{
    /// <summary>
    /// Runs multiple sessions to create benchmarks
    /// </summary>
    public class BenchmarkRunner
    {
        private int _iterations;
        private readonly Dictionary<string, ProfilerSession> _sessions;

        public BenchmarkRunner()
        {
            _iterations = 1;
            _sessions = new Dictionary<string, ProfilerSession>();
        }

        /// <summary>
        /// Set the amount of iterations that the benchmarks run
        /// </summary>
        /// <param name="iterations"></param>
        /// <returns></returns>
        public BenchmarkRunner SetIterations(int iterations)
        {
            if (iterations < 1)
            {
                throw new InvalidOperationException("Invalid amount of iterations. There have to be at lease 1 iteration");
            }

            _iterations = iterations;

            return this;
        }

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
            var results = new ProfilerResultCollection(_iterations);

            foreach (var key in _sessions.Keys)
            {
                var session = _sessions[key];
                session.SetIterations(_iterations);
                var result = session.RunSession();
                results.Add(key, result);
            }

            return results;
        }
    }
}
