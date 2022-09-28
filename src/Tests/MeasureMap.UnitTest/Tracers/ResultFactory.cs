
using System;

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
            var result = new ProfilerResult();

            for(var t = 0; t < 2; t++)
            {
                var thread = new Result();
                result.Add(thread);

                for (var i = 0; i < 10; i++)
                {
                    var ticks = 20 - (i + 1 * 2);
                    thread.Add(new IterationResult
                    {
                        ThreadId = t + 1,
                        Iteration = i + 1,
                        TimeStamp = new System.DateTime(2012, 12, 21, 1, 1, 1, 1).AddTicks(i + 1),
                        Duration = TimeSpan.FromTicks(ticks),

                        //TODO: remove ticks. take all from duration
                        Ticks = ticks
                    });
                }
            }

            

            return result;
        }
    }
}
