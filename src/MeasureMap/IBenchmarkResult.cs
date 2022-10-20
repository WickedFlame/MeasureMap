using System;
using System.Collections.Generic;

namespace MeasureMap
{
    /// <summary>
    /// Defines the redults of benchmarktests
    /// </summary>
    public interface IBenchmarkResult : IEnumerable<IProfilerResult>
    {
        /// <summary>
        /// Add a result of benchmarktest
        /// </summary>
        /// <param name="name">The name of the result</param>
        /// <param name="result">The result</param>
        void Add(string name, IProfilerResult result);

        /// <summary>
        /// Gets the configured duration that the benchmarktests were run for
        /// </summary>
        TimeSpan Duration { get; }

        /// <summary>
        /// Gets the amount of iterations that the benchmarktests were run
        /// </summary>
        int Iterations { get; }

        /// <summary>
        /// Gets the keys collection
        /// </summary>
        IEnumerable<string> Keys { get; }

        /// <summary>
        /// Indexer for benchmarkresults 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IProfilerResult this[string key] { get; }
    }
}
