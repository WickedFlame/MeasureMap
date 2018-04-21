using System;
using System.Diagnostics;
using System.Threading;

namespace MeasureMap
{
    /// <summary>
    /// A worker that runs the provided tasks
    /// </summary>
    public class Worker
    {
        /// <summary>
        /// Runs the provided task for the iteration count
        /// </summary>
        /// <param name="task">The task that has to be run</param>
        /// <param name="iterations">The amount of iterations to run the task</param>
        /// <returns></returns>
        public Result Run(ITaskRunner task, int iterations)
        {
            var result = new Result();
            var stopwatch = new Stopwatch();

            //// warmup
            //Trace.WriteLine($"Running Task once for warmup on Performance Analysis Benchmark");
            //stopwatch.Start();

            //task.Run(0);

            //stopwatch.Stop();
            //profile.Warmup = stopwatch.Elapsed;

            ForceGarbageCollector();

            Trace.WriteLine($"Running Task for {iterations} iterations for Perfomance Analysis Benchmark");

            result.InitialSize = GC.GetTotalMemory(true);

            for (int i = 0; i < iterations; i++)
            {
                var initial = GC.GetTotalMemory(true);

                stopwatch.Reset();
                stopwatch.Start();

                var output = task.Run(i);

                stopwatch.Stop();

                var after = GC.GetTotalMemory(false);
                ForceGarbageCollector();
                var afterCollect = GC.GetTotalMemory(true);

                var iteration = new ProfileIteration(stopwatch.ElapsedTicks, stopwatch.Elapsed, initial, after, afterCollect)
                {
                    Data = output
                };

                result.Add(iteration);
            }

            ForceGarbageCollector();
            result.EndSize = GC.GetTotalMemory(true);

            return result;
        }
        
        /// <summary>
        /// Forces the GC to run
        /// </summary>
        protected void ForceGarbageCollector()
        {
            // clean up
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
