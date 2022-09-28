using System.Collections.Generic;

namespace MeasureMap.Tracers.Metrics.DefaultMetrics
{
    internal abstract class MetricsCollection<TType, TMetric> where TType : Enumeration
    {
        private readonly Dictionary<TType, TMetric> _defaultMetrics = new();

        public void Add(TType type, TMetric metric)
        {
            _defaultMetrics.Add(type, metric);
        }

        public TMetric Get(TType type)
        {
            if (!_defaultMetrics.ContainsKey(type))
            {
                return default;
            }

            return _defaultMetrics[type];
        }

        public bool Contains(TType type)
            => _defaultMetrics.ContainsKey(type);
    }
}
