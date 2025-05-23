﻿using System;
using System.Collections.Generic;

namespace MeasureMap
{
    /// <summary>
    /// The result
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Gets the id of the thread that the task was run in
        /// </summary>
        int ThreadId { get; }

        /// <summary>
        /// Gets the number of the thread created by MeasureMap. This is not the same as the ThreadId
        /// </summary>
        int ThreadNumber { get; }

        /// <summary>
        /// The iterations that were run
        /// </summary>
        IEnumerable<IIterationResult> Iterations { get; }

        /// <summary>
        /// Collection of all retun values
        /// </summary>
        IDictionary<string, object> ResultValues { get; }

        /// <summary>
        /// Gets the average Milliseconds that all iterations took to run the task
        /// </summary>
        [Obsolete("Use Extensionmethods AverageTicks.ToMilliSeconds()")]
        double AverageMilliseconds { get; }

        /// <summary>
        /// Gets the average Ticks that all iterations took to run the task
        /// </summary>
        long AverageTicks { get; }

        /// <summary>
        /// Gets the average time each iteration took
        /// </summary>
        TimeSpan AverageTime { get; }

        /// <summary>
        /// Gets the total time for all iterations
        /// </summary>
        TimeSpan TotalTime { get; }

        /// <summary>
        /// Gets the fastest iterations
        /// </summary>
        IIterationResult Fastest { get; }

        /// <summary>
        /// Gets the slowest iterations
        /// </summary>
        IIterationResult Slowest { get; }

        /// <summary>
        /// The increase in memory size
        /// </summary>
        long Increase { get; }

        /// <summary>
        /// The initial memory size
        /// </summary>
        long InitialSize { get; }

        /// <summary>
        /// The memory size after measure
        /// </summary>
        long EndSize { get; }
    }
}
