using System;

namespace MeasureMap
{
    /// <summary>
    /// Marks a iteration of a run Task
    /// </summary>
    public class PerformanceIteration
    {
        /// <summary>
        /// Creates a object containing information on the iteration
        /// </summary>
        /// <param name="ticks">Ticks the Task took to run</param>
        /// <param name="duration">The duration the Task took to run</param>
        public PerformanceIteration(long ticks, TimeSpan duration)
        {
            TimeStamp = DateTime.Now;
            Ticks = ticks;
            Duration = duration;
        }

        /// <summary>
        /// Gets the Ticks that the iteration took to run the Task
        /// </summary>
        public long Ticks { get; private set; }

        /// <summary>
        /// Gets the Milliseconds that the iteration took to run the Task
        /// </summary>
        public TimeSpan Duration { get; private set; }

        /// <summary>
        /// The timestamp of when the iteration was run
        /// </summary>
        public DateTime TimeStamp { get; private set; }

        public override string ToString()
        {
            return "Ticks: " + Ticks + " mS: " + Duration;
        }
    }
}
