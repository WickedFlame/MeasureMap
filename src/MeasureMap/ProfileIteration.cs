using System;

namespace MeasureMap
{
    /// <summary>
    /// Marks a iteration of a run Task
    /// </summary>
    public class ProfileIteration
    {
        /// <summary>
        /// Creates a object containing information on the iteration
        /// </summary>
        /// <param name="ticks">Ticks the Task took to run</param>
        /// <param name="duration">The duration the Task took to run</param>
        /// <param name="initialSize">The initial memory size</param>
        /// <param name="afterExecution">The memory size afte execution</param>
        /// <param name="afterGarbageCollection">The memory size after GC</param>
        public ProfileIteration(long ticks, TimeSpan duration, long initialSize, long afterExecution, long afterGarbageCollection)
        {
            TimeStamp = DateTime.Now;
            Ticks = ticks;
            Duration = duration;
            InitialSize = initialSize;
            AfterExecution = afterExecution;
            AfterGarbageCollection = afterGarbageCollection;
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

        /// <summary>
        /// The memory size after execution
        /// </summary>
        public long AfterExecution { get; private set; }

        /// <summary>
        /// The memory size after GC
        /// </summary>
        public long AfterGarbageCollection { get; private set; }

        /// <summary>
        /// The initial memory size
        /// </summary>

        public long InitialSize { get; private set; }
        public override string ToString()
        {
            return "Ticks: " + Ticks + " mS: " + Duration;
        }
    }
}
