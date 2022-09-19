
namespace MeasureMap.Tracers
{
    /// <summary>
    /// Options that are used for tracing the result to the output
    /// </summary>
    public class TraceOptions
    {
        /// <summary>
        /// Gets the default TraceOptions
        /// </summary>
        public static TraceOptions Default { get; } = new TraceOptions();

        /// <summary>
        /// Gets or sets the <see cref="ITracer"/> that is used if none other is defined
        /// </summary>
        public ITracer Tracer { get; set; } = new MarkDownTracer();

        /// <summary>
        /// Gets or sets the <see cref="IResultWriter"/> that is used if none other is defined
        /// </summary>
        public IResultWriter ResultWriter { get; set; } = new TraceWriter();
        
        /// <summary>
        /// Trace all results of all iterations to the output
        /// </summary>
        public bool TraceFullStack { get; set; }
    }
}
