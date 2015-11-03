using System;

namespace MeasureMap
{
    public class Iteration
    {
        public Iteration(long ticks, TimeSpan duration)
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

        public DateTime TimeStamp { get; private set; }

        public override string ToString()
        {
            return "Ticks: " + Ticks + " mS: " + Duration;
        }
    }
}
