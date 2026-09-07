namespace MeasureMap.Serialization
{
    /// <summary>
    /// 
    /// </summary>
    public static class BenchmarkResultExtensions
    {
        /// <summary>
        /// Convert a IBenchmarkResult to a BenchmarkModel for serialization.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static BenchmarkModel ToBenchmarkModel(this IBenchmarkResult result)
        {
            var model = new BenchmarkModel
            {
                Iterations = result.Iterations,
                Duration = result.Duration
            };

            foreach (var key in result.Keys)
            {
                var res = result[key];
                model.Benchmarks.Add(new Benchmark
                {
                    Name = key,
                    ThreadId = res.ThreadId,
                    ThreadNumber = res.ThreadNumber,
                    AverageTicks = res.AverageTicks,
                    AverageMilliseconds = res.AverageTicks.ToMilliseconds(),
                    AverageTime = res.AverageTime,
                    TotalTime = res.TotalTime,
                    Fastest = res.Fastest.Duration,
                    Slowest = res.Slowest.Duration,
                    Increase = res.Increase,
                    InitialSize = res.InitialSize,
                    EndSize = res.EndSize
                });
            }

            return model;
        }
    }
}
