using System.Collections.Generic;
using System.Linq;

namespace MeasureMap
{
    /// <summary>
    /// Represents the result of a profiled session
    /// </summary>
    public class ProfilerResult 
    {
        private readonly List<ProfileIteration> _iterations;

        /// <summary>
        /// Creates a result of the profiled session
        /// </summary>
        public ProfilerResult()
        {
            _iterations = new List<ProfileIteration>();
        }

        /// <summary>
        /// The iterations that were run
        /// </summary>
        public IEnumerable<ProfileIteration> Iterations
        {
            get
            {
                return _iterations;
            }
        }
        
        /// <summary>
        /// Gets the average Milliseconds that all iterations took to run the task
        /// </summary>
        public long AverageMilliseconds
        {
            get
            {
                return Iterations.Select(i => i.Duration.Milliseconds).Sum() / Iterations.Count();
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
        public long Increase
        {
            get
            {
                return EndSize - InitialSize;
            }
        }

        internal void Add(ProfileIteration iteration)
        {
            _iterations.Add(iteration);
        }
    }
}
