using System.Collections.Generic;
using System.Linq;

namespace MeasureMap
{
    public class ProfileResult
    {
        private readonly List<Iteration> _iterations;

        public ProfileResult()
        {
            _iterations = new List<Iteration>();
        }

        /// <summary>
        /// The iterations that were run
        /// </summary>
        public IEnumerable<Iteration> Iterations
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
                return _iterations.Select(i => i.Duration.Milliseconds).Sum() / _iterations.Count;
            }
        }

        /// <summary>
        /// Gets the average Ticks that all iterations took to run the task
        /// </summary>
        public long AverageTicks
        {
            get
            {
                return _iterations.Select(i => i.Ticks).Sum() / _iterations.Count;
            }
        }

        internal void Add(Iteration iteration)
        {
            _iterations.Add(iteration);
        }
    }
}
