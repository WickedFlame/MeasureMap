
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

        public static readonly IterationMetric AfterExecution = new("AfterExecution");
        public static readonly IterationMetric AfterGarbageCollection = new("AfterGarbageCollection");
        public static readonly IterationMetric Duration = new("Duration");
        public static readonly IterationMetric InitialSize = new("InitialSize");
        public static readonly IterationMetric Ticks = new("Ticks");
        public static readonly IterationMetric Nanoseconds = new("Nanoseconds");
        public static readonly IterationMetric Microseconds = new("Microseconds");
        public static readonly IterationMetric Milliseconds = new("Milliseconds");
        public static readonly IterationMetric TimeStamp = new("TimeStamp");
        public static readonly IterationMetric Iteration = new("Iteration");
        public static readonly IterationMetric ThreadId = new("ThreadId");
        public static readonly IterationMetric ProcessId = new("ProcessId");
    }
}
