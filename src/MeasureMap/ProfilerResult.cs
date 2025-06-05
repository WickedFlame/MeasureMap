using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MeasureMap
{
    /// <summary>
    /// The result
    /// </summary>
    public class ProfilerResult : IProfilerResult
    {
        private readonly List<IResult> _results = new List<IResult>();

        /// <summary>
        /// Creates a profiler result
        /// </summary>
        public ProfilerResult()
        {
            ResultValues = new Dictionary<string, object>();
        }

        /// <summary>
        /// Collection of all retun values
        /// </summary>
        public IDictionary<string, object> ResultValues { get; }

        /// <summary>
        /// Gets the total timespan
        /// </summary>
        public TimeSpan Elapsed => ResultValues.ContainsKey(ResultValueType.Elapsed) ? (TimeSpan)ResultValues[ResultValueType.Elapsed] : TimeSpan.Zero;

        /// <summary>
        /// The iterations that were run.
        /// This is a summary of all Iterations over all threads.
        /// Thre results of each thread is accessed through the enumerator
        /// </summary>
        public IEnumerable<IIterationResult> Iterations => _results.SelectMany(r => r.Iterations);

        /// <summary>
        /// Gets the fastest iterations
        /// </summary>
        public IIterationResult Fastest
        {
            get
            {
                return Iterations.OrderBy(i => i.Ticks).FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets the slowest iterations
        /// </summary>
        public IIterationResult Slowest
        {
            get
            {
                return Iterations.OrderByDescending(i => i.Ticks).FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets the average Milliseconds that all iterations took to run the task
        /// </summary>
        [Obsolete("Use Extensionmethods AverageTicks.ToMilliSeconds()")]
        public double AverageMilliseconds
        {
            get
            {
                return Math.Round(Iterations.Select(i => i.Duration.TotalMilliseconds).Sum() / Iterations.Count(), 5);
            }
        }

        /// <summary>
        /// Gets the average Ticks that all iterations took to run the task
        /// </summary>
        public long AverageTicks
        {
            get
            {
                return Iterations.Any() ? Iterations.Select(i => i.Ticks).Sum() / Iterations.Count() : 0;
            }
        }

        /// <summary>
        /// Gets the average time each iteration took
        /// </summary>
        public TimeSpan AverageTime => TimeSpan.FromMilliseconds(AverageTicks.ToMilliseconds());

        /// <summary>
        /// Gets the sum of all times for all iterations
        /// </summary>
        public TimeSpan TotalTime
        {
            get
            {
                return TimeSpan.FromMilliseconds(Iterations.Select(i => i.Ticks).Sum().ToMilliseconds());
            }
        }

        /// <summary>
        /// Gets the id of the thread that the task was run in.
        /// For multithreaded profiles this gets the ThreadId of the first result.
        /// Use the enumerator of the <see cref="IProfilerResult"/> to get the ThreadIds of all threads used
        /// </summary>
        public int ThreadId => this.FirstOrDefault()?.ThreadId ?? 0;

        /// <summary>
        /// Gets the number of the thread created by MeasureMap. This is not the same as the ThreadId
        /// </summary>
        public int ThreadNumber => this.FirstOrDefault()?.ThreadNumber ?? 0;

        /// <summary>
        /// The initial memory size
        /// </summary>
        public long InitialSize => _results.Select(r => r.InitialSize).Min();

        /// <summary>
        /// The memory size after measure
        /// </summary>
        public long EndSize => _results.Select(r => r.EndSize).Max();

        /// <summary>
        /// The increase in memory size
        /// </summary>
        public long Increase => EndSize - InitialSize;

        /// <summary>
        /// Duration of the warmup
        /// </summary>
        public TimeSpan Warmup => ResultValues.ContainsKey(ResultValueType.Warmup) ? (TimeSpan)ResultValues[ResultValueType.Warmup] : TimeSpan.Zero;

        /// <summary>
        /// Gets the last <see cref="Result"/> for fast access that is needed during executions
        /// </summary>
        public IResult Last { get; private set; }

        /// <summary>
        /// Add a new result
        /// </summary>
        /// <param name="result"></param>
        public void Add(IResult result)
        {
            _results.Add(result);
            Last = result;
        }

        /// <summary>
        /// The enumerator. Each <see cref="IResult"/> represents the results of a thread
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IResult> GetEnumerator()
        {
            return _results.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _results.GetEnumerator();
        }
    }
}
