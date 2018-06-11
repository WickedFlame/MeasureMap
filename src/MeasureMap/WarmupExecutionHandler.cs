using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MeasureMap
{
    public interface IWarmupExecutionHandler : ITaskExecutionHandler { }

    public class WarmupExecutionHandler : TaskExecutionHandler, IWarmupExecutionHandler
    {
        public override IProfilerResult Execute(ITask task, int iterations)
        {
            var stopwatch = new Stopwatch();

            // warmup
            Trace.WriteLine($"Running Task once for warmup on Performance Analysis Benchmark");
            stopwatch.Start();

            task.Run(0);

            stopwatch.Stop();

            var result = base.Execute(task, iterations);

            result.ResultValues.Add("Warmup", stopwatch.Elapsed);

            return result;
        }
    }
}
