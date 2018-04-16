using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MeasureMap
{
    public class ProfilerResultCollection : IEnumerable<ProfilerResult>, IProfilerResult
    {
        private readonly List<ProfilerResult> _results = new List<ProfilerResult>();

        public TimeSpan Elapsed { get; internal set; }

        /// <summary>
        /// The iterations that were run
        /// </summary>
        public IEnumerable<ProfileIteration> Iterations => _results.SelectMany(r => r.Iterations);

        /// <summary>
        /// Gets the fastest iterations
        /// </summary>
        public ProfileIteration Fastest
        {
            get
            {
                return Iterations.OrderBy(i => i.Ticks).FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets the slowest iterations
        /// </summary>
        public ProfileIteration Slowest
        {
            get
            {
                return Iterations.OrderByDescending(i => i.Ticks).FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets the average Milliseconds that all iterations took to run the task
        /// </summary>
        public long AverageMilliseconds
        {
            get
            {
                return Iterations.Select(i => (int)i.Duration.TotalMilliseconds).Sum() / Iterations.Count();
            }
        }

        /// <summary>
        /// Gets the average Ticks that all iterations took to run the task
        /// </summary>
        public long AverageTicks
        {
            get
            {
                return Iterations.Select(i => i.Ticks).Sum() / Iterations.Count();
            }
        }

        /// <summary>
        /// Gets the average time each iteration took
        /// </summary>
        public TimeSpan AverageTime => TimeSpan.FromTicks(AverageTicks);

        /// <summary>
        /// Gets the total time for all iterations
        /// </summary>
        public TimeSpan TotalTime
        {
            get
            {
                return TimeSpan.FromTicks(Iterations.Select(i => i.Ticks).Sum());
            }
        }



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

        internal void Add(ProfilerResult result)
        {
            _results.Add(result);
        }

        public IEnumerator<ProfilerResult> GetEnumerator()
        {
            return _results.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _results.GetEnumerator();
        }
    }
}
