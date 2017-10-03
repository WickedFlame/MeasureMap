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
        public long Ticks { get; }

        /// <summary>
        /// Gets the Milliseconds that the iteration took to run the Task
        /// </summary>
        public TimeSpan Duration { get; }

        /// <summary>
        /// The timestamp of when the iteration was run
        /// </summary>
        public DateTime TimeStamp { get; }

        /// <summary>
        /// The memory size after execution
        /// </summary>
        public long AfterExecution { get; }

        /// <summary>
        /// The memory size after GC
        /// </summary>
        public long AfterGarbageCollection { get; }

        /// <summary>
        /// The initial memory size
        /// </summary>
        public long InitialSize { get; }

        /// <summary>
        /// Gets or sets the data that is returned by the Task. If no data is returned, the iteration is contained
        /// </summary>
        public object Data { get; set; }

        public override string ToString()
        {
            return "Ticks: " + Ticks + " mS: " + Duration;
        }
    }
}
