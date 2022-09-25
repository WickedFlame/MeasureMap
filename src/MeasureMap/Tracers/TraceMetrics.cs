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
        /// Add a default <see cref="ProfilerMetric"/> to the output
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
        /// Add a custom <see cref="IProfilerMetric"/>
        /// </summary>
        /// <param name="metric"></param>
        public void Add(IProfilerMetric metric)
        {
            _profilerMetrics.Add(metric);
        }

        /// <summary>
        /// Add a default <see cref="ProfileThreadMetric"/> to the output
        /// </summary>
        /// <param name="type"></param>
        public void Add(ProfileThreadMetric type)
        {
            if (!DefaultThreadMetrics.Contains(type))
            {
                return;
            }

            Add(DefaultThreadMetrics.Get(type));
        }

        /// <summary>
        /// Add a custom <see cref="IProfileThreadResultMetric"/>
        /// </summary>
        /// <param name="metric"></param>
        public void Add(IProfileThreadResultMetric metric)
        {
            _threadMetrics.Add(metric);
        }

        /// <summary>
        /// Add a default <see cref="IterationMetric"/> to the output
        /// </summary>
        /// <param name="type"></param>
        public void Add(IterationMetric type)
        {
            if (!DefaultIterationMetrics.Contains(type))
            {
                return;
            }

            Add(DefaultIterationMetrics.Get(type));
        }

        /// <summary>
        /// Add a custom <see cref="IIterationMetric"/>
        /// </summary>
        /// <param name="metric"></param>
        public void Add(IIterationMetric metric)
        {
            _iterationMetrics.Add(metric);
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
