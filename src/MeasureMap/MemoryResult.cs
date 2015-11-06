namespace MeasureMap
{
    /// <summary>
    /// Represents the result of a memory profiling
    /// </summary>
    public class MemoryResult : ProfileResult<MemoryIteration>
    {
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
    }
}
