using System;
using System.Diagnostics;

namespace MeasureMap
{
    /// <summary>
    /// TaskExecutor that measures the total elapsed time
    /// </summary>
    public class ElapsedTimeExecutionHandler : ExecutionHandler
    {
        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="iterations">The iterations to run the task</param>
        /// <returns>The resulting collection of the executions</returns>
        public override IProfilerResult Execute(ITask task, int iterations)
        {
            var sw = new Stopwatch();
            sw.Start();

            var result = base.Execute(task, iterations);

            sw.Stop();
            result.ResultValues.Add("Elapsed", sw.Elapsed);

            return result;
        }
    }
}
