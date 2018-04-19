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
        public ProfilerResult Run(ITaskRunner task, int iterations)
        {
            // warmup
            Trace.WriteLine($"Running Task once for warmup on Performance Analysis Benchmark");
            task.Run(0);

            var profile = new ProfilerResult();
            var stopwatch = new Stopwatch();
            
            ForceGarbageCollector();

            Trace.WriteLine($"Running Task for {iterations} iterations for Perfomance Analysis Benchmark");

            profile.InitialSize = GC.GetTotalMemory(true);

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

                profile.Add(iteration);
            }

            ForceGarbageCollector();
            profile.EndSize = GC.GetTotalMemory(true);

            return profile;
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
