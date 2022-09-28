using MeasureMap.Tracers.Metrics;

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
        public static TraceOptions Default { get; } = new();

        /// <summary>
        /// Gets or sets the <see cref="ITracer"/> that is used if none other is defined
        /// </summary>
        public ITracer Tracer { get; set; } = new MarkDownTracer();

        /// <summary>
        /// Gets or sets the <see cref="IResultWriter"/> that is used if none other is defined
        /// </summary>
        public IResultWriter ResultWriter { get; set; } = new TraceResultWriter();
        
        /// <summary>
        /// Trace the detail of all threads that are used
        /// </summary>
        public bool TraceThreadDetail { get; set; }

        /// <summary>
        /// Trace all results of all iterations to the output
        /// </summary>
        public bool TraceFullStack { get; set; }

        /// <summary>
        /// Gets or sets the configuration for metrics that are traced
        /// </summary>
        public TraceMetrics Metrics { get; set; }

        /// <summary>
        /// The header that is writen to the output
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Clone the options. Metrics are not copied. These are set as default when tracing
        /// </summary>
        /// <returns></returns>
        internal TraceOptions Clone()
        {
            // do not clone the metrics
            //

            return new TraceOptions
            {
                TraceFullStack = TraceFullStack
            };
        }
    }
}
