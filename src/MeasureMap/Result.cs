using System;
using System.Collections.Generic;
using System.Linq;

namespace MeasureMap
{
    /// <summary>
    /// Represents the result of a profiled session
    /// </summary>
    public class Result : IResult
    {
        private readonly List<IIterationResult> _iterations;

        /// <summary>
        /// Creates a result of the profiled session
        /// </summary>
        public Result()
        {
            _iterations = new List<IIterationResult>();
            ResultValues = new Dictionary<string, object>();
        }
        
        /// <summary>
        /// Collection of all retun values
        /// </summary>
        public IDictionary<string, object> ResultValues { get; }

        /// <summary>
        /// The iterations that were run
        /// </summary>
        public IEnumerable<IIterationResult> Iterations => _iterations;

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
        /// Gets the id of the thread that the task was run in
        /// </summary>
        public int ThreadId
        {
            get
            {
                var first = Iterations.FirstOrDefault();
                return first != null ? first.ThreadId : 0;
            }
        }

        /// <summary>
        /// The initial memory size
        /// </summary>
        public long InitialSize
        {
            get; set;
        }

        /// <summary>
        /// The memory size after measure
        /// </summary>
        public long EndSize
        {
            get; set;
        }

        /// <summary>
        /// The increase in memory size
        /// </summary>
        public long Increase => EndSize - InitialSize;

        /// <summary>
        /// Duration of the warmup
        /// </summary>
        public TimeSpan Warmup { get; set; }
        
        /// <summary>
        /// Add a new <see cref="IIterationResult"/>
        /// </summary>
        /// <param name="iteration"></param>
        public void Add(IIterationResult iteration)
        {
            _iterations.Add(iteration);
        }
    }
}
