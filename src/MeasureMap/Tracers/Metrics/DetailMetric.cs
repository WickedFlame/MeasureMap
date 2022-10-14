
namespace MeasureMap.Tracers.Metrics
{
    /// <summary>
    /// Enumeration of all default Metrics of a IterationResult
    /// </summary>
    public class DetailMetric : Enumeration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public DetailMetric(string name) : base(name)
        {
        }

        /// <summary>
        /// AfterExecution
        /// </summary>
        public static readonly DetailMetric AfterExecution = new("AfterExecution");

        /// <summary>
        /// AfterGarbageCollection
        /// </summary>
        public static readonly DetailMetric AfterGarbageCollection = new("AfterGarbageCollection");

        /// <summary>
        /// Duration
        /// </summary>
        public static readonly DetailMetric Duration = new("Duration");

        /// <summary>
        /// Initial size
        /// </summary>
        public static readonly DetailMetric InitialSize = new("Initial size");

        /// <summary>
        /// Ticks
        /// </summary>
        public static readonly DetailMetric Ticks = new("Ticks");

        /// <summary>
        /// Nanoseconds
        /// </summary>
        public static readonly DetailMetric Nanoseconds = new("Nanoseconds");

        /// <summary>
        /// Microseconds
        /// </summary>
        public static readonly DetailMetric Microseconds = new("Microseconds");

        /// <summary>
        /// Milliseconds
        /// </summary>
        public static readonly DetailMetric Milliseconds = new("Milliseconds");

        /// <summary>
        /// TimeStamp
        /// </summary>
        public static readonly DetailMetric TimeStamp = new("TimeStamp");

        /// <summary>
        /// Iteration
        /// </summary>
        public static readonly DetailMetric Iteration = new("Iteration");

        /// <summary>
        /// ThreadId
        /// </summary>
        public static readonly DetailMetric ThreadId = new("ThreadId");

        /// <summary>
        /// ProcessId
        /// </summary>
        public static readonly DetailMetric ProcessId = new("ProcessId");
    }
}
