
namespace MeasureMap.UnitTest.Tracers
{
    public static class ResultFactory
    {
        public static IBenchmarkResult CreateBenchmarkResult()
        {
            return new BenchmarkResult(10)
            {
                { "res1", CreateResult() },
                { "res2", CreateResult() }
            };
        }

        public static IProfilerResult CreateResult()
        {
            var r = new Result();

            for (var i = 0; i < 10; i++)
            {
                r.Add(new IterationResult
                {
                    TimeStamp = new System.DateTime(2012, 12, 21, 1, 1, 1)
                });
            }

            var result = new ProfilerResult
            {
                r
            };

            return result;
        }
    }
}
