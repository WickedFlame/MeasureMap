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
        private static readonly MetricsCollection<DetailMetric, IDetailMetric> DefaultDetailMetrics = new DetailMetricsCollection();
        private static readonly MetricsCollection<ProfilerMetric, IProfilerMetric> DefaultProfilerMetrics = new ProfilerMetricsCollection();
        private static readonly MetricsCollection<ThreadMetric, IThreadMetric> DefaultThreadMetrics = new ThreadMetricsCollection();

        private readonly List<IProfilerMetric> _profilerMetrics = new();
        private readonly List<IThreadMetric> _threadMetrics = new();
        private readonly List<IDetailMetric> _detailMetrics = new();

        /// <summary>
        /// Get all registered metrics
        /// </summary>
        public IEnumerable<string> RegisteredMetrics
        {
            get
            {
                var metrics = _profilerMetrics.Select(p => p.Name).ToList();
                metrics.AddRange(_threadMetrics.Select(p => p.Name));
                metrics.AddRange(_detailMetrics.Select(p => p.Name));

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
        /// Add a default <see cref="ThreadMetric"/> to the output. This is used for thread details of Profiers
        /// </summary>
        /// <param name="type"></param>
        public TraceMetrics Add(ThreadMetric type)
        {
            if (!DefaultThreadMetrics.Contains(type))
            {
                return this;
            }

            Add(DefaultThreadMetrics.Get(type));

            return this;
        }

        /// <summary>
        /// Add a custom <see cref="IThreadMetric"/>. This is used for thread details of Profiers
        /// </summary>
        /// <param name="metric"></param>
        public TraceMetrics Add(IThreadMetric metric)
        {
            _threadMetrics.Add(metric);

            return this;
        }

        /// <summary>
        /// Add a default <see cref="DetailMetric"/> to the output. This is used for details of each iteration
        /// </summary>
        /// <param name="type"></param>
        public TraceMetrics Add(DetailMetric type)
        {
            if (!DefaultDetailMetrics.Contains(type))
            {
                return this;
            }

            Add(DefaultDetailMetrics.Get(type));

            return this;
        }

        /// <summary>
        /// Add a custom <see cref="IDetailMetric"/>. This is used for details of each iteration
        /// </summary>
        /// <param name="metric"></param>
        public TraceMetrics Add(IDetailMetric metric)
        {
            _detailMetrics.Add(metric);

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
        /// Get the registered <see cref="IThreadMetric"/>
        /// </summary>
        /// <returns></returns>
        public IThreadMetric[] GetThreadMetrics()
        {
            return _threadMetrics.ToArray();
        }

        /// <summary>
        /// Get the registered <see cref="IDetailMetric"/>
        /// </summary>
        /// <returns></returns>
        public IDetailMetric[] GetDetailMetrics()
        {
            return _detailMetrics.ToArray();
        }
    }
}
