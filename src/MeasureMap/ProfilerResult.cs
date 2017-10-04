using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public IEnumerable<ProfileIteration> Iterations => _iterations;

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
        /// Trace the result to the Console
        /// </summary>
        public string Trace()
        {
            var result = ToString();

            //Console.WriteLine(result);
            System.Diagnostics.Trace.WriteLine(result);

            return result;
        }

        internal void Add(ProfileIteration iteration)
        {
            _iterations.Add(iteration);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("### MeasureMap - Profiler result for Profilesession:");
            sb.AppendLine($"\tSetup ========================================");
            sb.AppendLine($"\t\tIterations:\t\t\t{Iterations.Count()}");
            sb.AppendLine($"\tDuration ========================================");
            sb.AppendLine($"\t\tDuration Total:\t\t\t{TotalTime.ToString()}");
            sb.AppendLine($"\t\tAverage Time:\t\t\t{AverageTime}");
            sb.AppendLine($"\t\tAverage Milliseconds:\t\t{AverageMilliseconds}");
            sb.AppendLine($"\t\tAverage Ticks:\t\t\t{AverageTicks}");
            sb.AppendLine($"\t\tFastest:\t\t\t{TimeSpan.FromTicks(Fastest.Ticks)}");
            sb.AppendLine($"\t\tSlowest:\t\t\t{TimeSpan.FromTicks(Slowest.Ticks)}");
            sb.AppendLine($"\tMemory ==========================================");
            sb.AppendLine($"\t\tMemory Initial size:\t\t{InitialSize}");
            sb.AppendLine($"\t\tMemory End size:\t\t{EndSize}");
            sb.AppendLine($"\t\tMemory Increase:\t\t{Increase}");
            
            return sb.ToString();
        }
    }
}
