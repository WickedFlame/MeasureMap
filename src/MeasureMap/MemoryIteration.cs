namespace MeasureMap
{
    /// <summary>
    /// A profiled iterateion
    /// </summary>
    public class MemoryIteration
    {
        /// <summary>
        /// Creates a profile iterateion
        /// </summary>
        /// <param name="initialSize">The initial size</param>
        /// <param name="afterExecution">The size afte execution</param>
        /// <param name="afterGarbageCollection">The size after GC</param>
        public MemoryIteration(long initialSize, long afterExecution, long afterGarbageCollection)
        {
            InitialSize = initialSize;
            AfterExecution = afterExecution;
            AfterGarbageCollection = afterGarbageCollection;
        }

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
    }
}
