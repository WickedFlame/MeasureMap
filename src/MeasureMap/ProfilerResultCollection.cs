using System;
using System.Collections;
using System.Collections.Generic;

namespace MeasureMap
{
    public class ProfilerResultCollection : IEnumerable<ProfilerResult>
    {
        private readonly List<ProfilerResult> _results = new List<ProfilerResult>();

        public TimeSpan Elapsed { get; internal set; }

        public void Add(ProfilerResult result)
        {
            _results.Add(result);
        }

        public IEnumerator<ProfilerResult> GetEnumerator()
        {
            return _results.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _results.GetEnumerator();
        }
    }
}
