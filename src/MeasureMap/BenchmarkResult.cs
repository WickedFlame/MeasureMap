using System;
using System.Collections;
using System.Collections.Generic;

namespace MeasureMap
{
    /// <summary>
    /// Defines the redults of benchmarktests
    /// </summary>
    public class BenchmarkResult : IBenchmarkResult
    {
        private readonly Dictionary<string, IProfilerResult> _results;

        /// <summary>
        /// Creates an instance of a BenchmarkResult
        /// </summary>
        /// <param name="settings"></param>
        public BenchmarkResult(ProfilerSettings settings)
        {
            _results = new Dictionary<string, IProfilerResult>();
            Iterations = settings.Iterations;
            Duration = settings.Duration;
        }

        /// <summary>
        /// Gets the configured duration that the benchmarktests were run for
        /// </summary>
        public TimeSpan Duration { get; }

        /// <summary>
        /// Gets the amount of iterations that the benchmarktests were run
        /// </summary>
        public int Iterations { get; }

        /// <summary>
        /// Gets the keys collection
        /// </summary>
        public IEnumerable<string> Keys => _results.Keys;

        /// <summary>
        /// Indexer for benchmarkresults 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IProfilerResult this[string key] => _results[key];

        /// <summary>
        /// Add a result of benchmarktest
        /// </summary>
        /// <param name="name">The name of the result</param>
        /// <param name="result">The result</param>
        public void Add(string name, IProfilerResult result)
        {
            _results.Add(name, result);
        }

        /// <summary>
        /// The enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IProfilerResult> GetEnumerator()
        {
            return _results.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
