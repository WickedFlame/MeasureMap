using System;
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
        /// Gets the average time each iteration took
        /// </summary>
        public TimeSpan AverageTime
        {
            get
            {
                return TimeSpan.FromTicks(AverageTicks);
            }
        }

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

        /// <summary>
        /// Trace the result to the Console
        /// </summary>
        public string Trace()
        {
            var result = "\n### MeasureMap - Profiler result for Profilesession:\n";
            result += $"\tDuration ========================================\n";
            result += $"\t\tDuration Total:\t\t\t{TotalTime.ToString()}\n";
            result += $"\t\tAverage Time:\t\t\t{AverageTime}\n";
            result += $"\tMemory ==========================================\n";
            result += $"\t\tMemory Initial size:\t{InitialSize}\n";
            result += $"\t\tMemory End size:\t\t{EndSize}\n";
            result += $"\t\tMemory Increase:\t\t{Increase}\n";

            //Console.WriteLine(result);
            System.Diagnostics.Trace.WriteLine(result);

            return result;
        }

        internal void Add(ProfileIteration iteration)
        {
            _iterations.Add(iteration);
        }
    }
}
