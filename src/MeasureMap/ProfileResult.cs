using System.Collections.Generic;
using System.Linq;

namespace MeasureMap
{
    /// <summary>
    /// Represents the result of a profiled session
    /// </summary>
    public class ProfileResult<T>
    {
        private readonly List<T> _iterations;

        /// <summary>
        /// Creates a result of the profiled session
        /// </summary>
        public ProfileResult()
        {
            _iterations = new List<T>();
        }

        /// <summary>
        /// The iterations that were run
        /// </summary>
        public IEnumerable<T> Iterations
        {
            get
            {
                return _iterations;
            }
        }


        internal void Add(T iteration)
        {
            _iterations.Add(iteration);
        }
    }
}
