
namespace MeasureMap.Tracers.Metrics
{
    /// <summary>
    /// Enumeration of all default Metrics of a IterationResult
    /// </summary>
    public class IterationMetric : Enumeration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public IterationMetric(string name) : base(name)
        {
        }

        /// <summary>
        /// AfterExecution
        /// </summary>
        public static readonly IterationMetric AfterExecution = new("AfterExecution");

        /// <summary>
        /// AfterGarbageCollection
        /// </summary>
        public static readonly IterationMetric AfterGarbageCollection = new("AfterGarbageCollection");

        /// <summary>
        /// Duration
        /// </summary>
        public static readonly IterationMetric Duration = new("Duration");

        /// <summary>
        /// Initial size
        /// </summary>
        public static readonly IterationMetric InitialSize = new("Initial size");

        /// <summary>
        /// Ticks
        /// </summary>
        public static readonly IterationMetric Ticks = new("Ticks");

        /// <summary>
        /// Nanoseconds
        /// </summary>
        public static readonly IterationMetric Nanoseconds = new("Nanoseconds");

        /// <summary>
        /// Microseconds
        /// </summary>
        public static readonly IterationMetric Microseconds = new("Microseconds");

        /// <summary>
        /// Milliseconds
        /// </summary>
        public static readonly IterationMetric Milliseconds = new("Milliseconds");

        /// <summary>
        /// TimeStamp
        /// </summary>
        public static readonly IterationMetric TimeStamp = new("TimeStamp");

        /// <summary>
        /// Iteration
        /// </summary>
        public static readonly IterationMetric Iteration = new("Iteration");

        /// <summary>
        /// ThreadId
        /// </summary>
        public static readonly IterationMetric ThreadId = new("ThreadId");

        /// <summary>
        /// ProcessId
        /// </summary>
        public static readonly IterationMetric ProcessId = new("ProcessId");
    }
}
