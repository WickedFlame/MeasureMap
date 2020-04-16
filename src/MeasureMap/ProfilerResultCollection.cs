using System.Collections;
using System.Collections.Generic;

namespace MeasureMap
{
    public interface IProfilerResultCollection : IEnumerable<IProfilerResult>
    {
        void Add(string name, IProfilerResult result);

        IEnumerable<string> Keys { get; }

        IProfilerResult this[string key] { get; }
    }

    public class ProfilerResultCollection : IProfilerResultCollection
    {
        private readonly Dictionary<string, IProfilerResult> _results;

        public ProfilerResultCollection()
        {
            _results = new Dictionary<string, IProfilerResult>();
        }

        public IEnumerable<string> Keys => _results.Keys;

        public IProfilerResult this[string key] => _results[key];

        public void Add(string name, IProfilerResult result)
        {
            _results.Add(name, result);
        }

        public IEnumerator<IProfilerResult> GetEnumerator()
        {
            return _results.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
