using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MeasureMap
{
    public interface IWarmupExecutor : ITaskExecutor { }

    public class WarmupExecutor : TaskExecutor, IWarmupExecutor
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
