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
            var results = new ProfilerResultCollection();

            foreach (var session in _sessions)
            {
                var result = session.Value.RunSession();
                results.Add(session.Key, result);
            }

            return results;
        }
    }
}
