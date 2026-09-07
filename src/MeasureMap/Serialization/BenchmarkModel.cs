using System;
using System.Collections.Generic;

namespace MeasureMap.Serialization
{
    public class BenchmarkModel
    {
        public int Iterations { get; set; }

        public TimeSpan Duration { get; set; }

        public List<Benchmark> Benchmarks { get; set; } = [];
    }
}
