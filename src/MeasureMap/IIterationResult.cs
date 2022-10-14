using System;

namespace MeasureMap
{
    /// <summary>
    /// Marks a iteration of a run Task
    /// </summary>
    public interface IIterationResult
    {
        /// <summary>
        /// Gets the id of the thread that the task was run in
        /// </summary>
        int ThreadId { get; set; }

        /// <summary>
        /// Gets the current process
        /// </summary>
        int ProcessId { get; set; }

        /// <summary>
        /// Gets the current iteration
        /// </summary>
        int Iteration { get; set; }
        
        /// <summary>
        /// Gets or sets the data that is returned by the Task. If no data is returned, the iteration is contained
        /// </summary>
        object Data { get; set; }

        /// <summary>
        /// Gets the Milliseconds that the iteration took to run the Task
        /// </summary>
        TimeSpan Duration { get; }

        /// <summary>
        /// Gets the Ticks that the iteration took to run the Task
        /// </summary>
        long Ticks { get; set; }

        /// <summary>
        /// The initial memory size
        /// </summary>
        long InitialSize { get; set; }

        /// <summary>
        /// The memory size after execution
        /// </summary>
        long AfterExecution { get; set; }

        /// <summary>
        /// The timestamp of when the iteration was run
        /// </summary>
        DateTime TimeStamp { get; set; }
    }
}
