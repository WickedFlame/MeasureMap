using System;
using System.Collections.Generic;

namespace MeasureMap
{
    public class BenchmarkRunner
    {
        private int _iterations;
        private readonly Dictionary<string, ProfilerSession> _sessions;

        public BenchmarkRunner()
        {
            _iterations = 1;
            _sessions = new Dictionary<string, ProfilerSession>();
        }

        public BenchmarkRunner SetIterations(int iterations)
        {
            if (iterations < 1)
            {
                throw new InvalidOperationException("Invalid amount of iterations. There have to be at lease 1 iteration");
            }

            _iterations = iterations;

            return this;
        }

        public BenchmarkRunner AddSession(string name, ProfilerSession session)
        {
            _sessions.Add(name, session);

            return this;
        }

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
