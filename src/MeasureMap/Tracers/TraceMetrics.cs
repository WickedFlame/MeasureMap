using MeasureMap.Tracers.Metrics.DefaultMetrics;
using System.Collections.Generic;
using MeasureMap.Tracers.Metrics;
using System.Linq;

namespace MeasureMap.Tracers
{
    /// <summary>
    /// Represents the collection of configured metrics for tracing
    /// </summary>
    public class TraceMetrics
    {
        private static readonly MetricsCollection<IterationMetric, IIterationMetric> DefaultIterationMetrics = new IterationMetricsCollection();
        private static readonly MetricsCollection<ProfilerMetric, IProfilerMetric> DefaultProfilerMetrics = new ProfilerMetricsCollection();
        private static readonly MetricsCollection<ProfileThreadMetric, IProfileThreadResultMetric> DefaultThreadMetrics = new ProfileThreadMetricsCollection();

        private readonly List<IProfilerMetric> _profilerMetrics = new();
        private readonly List<IProfileThreadResultMetric> _threadMetrics = new();
        private readonly List<IIterationMetric> _iterationMetrics = new();

        /// <summary>
        /// Get all registered metrics
        /// </summary>
        public IEnumerable<string> RegisteredMetrics
        {
            get
            {
                var metrics = _profilerMetrics.Select(p => p.Name).ToList();
                metrics.AddRange(_threadMetrics.Select(p => p.Name));
                metrics.AddRange(_iterationMetrics.Select(p => p.Name));

                return metrics;
            }
        }

        /// <summary>
        /// Add a default <see cref="ProfilerMetric"/> to the output. This is used for Profiers and Benchmarks
        /// </summary>
        /// <param name="type"></param>
        public void Add(ProfilerMetric type)
        {
            if (!DefaultProfilerMetrics.Contains(type))
            {
                return;
            }

            Add(DefaultProfilerMetrics.Get(type));
        }

        /// <summary>
        /// Add a custom <see cref="IProfilerMetric"/>. This is used for Profiers and Benchmarks
        /// </summary>
        /// <param name="metric"></param>
        public TraceMetrics Add(IProfilerMetric metric)
        {
            _profilerMetrics.Add(metric);

            return this;
        }

        /// <summary>
        /// Add a default <see cref="ProfileThreadMetric"/> to the output. This is used for thread details of Profiers
        /// </summary>
        /// <param name="type"></param>
        public TraceMetrics Add(ProfileThreadMetric type)
        {
            if (!DefaultThreadMetrics.Contains(type))
            {
                return this;
            }

            Add(DefaultThreadMetrics.Get(type));

            return this;
        }

        /// <summary>
        /// Add a custom <see cref="IProfileThreadResultMetric"/>. This is used for thread details of Profiers
        /// </summary>
        /// <param name="metric"></param>
        public TraceMetrics Add(IProfileThreadResultMetric metric)
        {
            _threadMetrics.Add(metric);

            return this;
        }

        /// <summary>
        /// Add a default <see cref="IterationMetric"/> to the output. This is used for details of each iteration
        /// </summary>
        /// <param name="type"></param>
        public TraceMetrics Add(IterationMetric type)
        {
            if (!DefaultIterationMetrics.Contains(type))
            {
                return this;
            }

            Add(DefaultIterationMetrics.Get(type));

            return this;
        }

        /// <summary>
        /// Add a custom <see cref="IIterationMetric"/>. This is used for details of each iteration
        /// </summary>
        /// <param name="metric"></param>
        public TraceMetrics Add(IIterationMetric metric)
        {
            _iterationMetrics.Add(metric);

            return this;
        }

        /// <summary>
        /// Get the registered <see cref="IProfilerMetric"/>
        /// </summary>
        /// <returns></returns>
        public IProfilerMetric[] GetProfilerMetrics()
        {
            return _profilerMetrics.ToArray();
        }

        /// <summary>
        /// Get the registered <see cref="IProfileThreadResultMetric"/>
        /// </summary>
        /// <returns></returns>
        public IProfileThreadResultMetric[] GetProfileThreadMetrics()
        {
            return _threadMetrics.ToArray();
        }

        /// <summary>
        /// Get the registered <see cref="IIterationMetric"/>
        /// </summary>
        /// <returns></returns>
        public IIterationMetric[] GetIterationMetrics()
        {
            return _iterationMetrics.ToArray();
        }
    }
}
