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
        private static readonly bool IsRunningOnMono;

        static Worker()
        {
            IsRunningOnMono = Type.GetType("Mono.Runtime") != null;
        }

        /// <summary>
        /// Runs the provided task for the iteration count
        /// </summary>
        /// <param name="task">The task that has to be run</param>
        /// <param name="iterations">The amount of iterations to run the task</param>
        /// <returns></returns>
        public ProfilerResult Run(ITaskRunner task, int iterations)
        {
            var profile = new ProfilerResult();
            var stopwatch = new Stopwatch();

            // warmup
            Trace.WriteLine($"Running Task once for warmup on Performance Analysis Benchmark");
            stopwatch.Start();

            task.Run(0);

            stopwatch.Stop();
            profile.Warmup = stopwatch.Elapsed;

            SetProcessor();
            SetThreadPriority();
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
        /// Sets the process to run on second core with high priority
        /// </summary>
        protected void SetProcessor()
        {
            if (!IsRunningOnMono)
            {
                var process = Process.GetCurrentProcess();

                try
                {
                    // Uses the second Core or Processor for the Test
                    process.ProcessorAffinity = new IntPtr(2);
                }
                catch (Exception)
                {
                    Trace.WriteLine($"Could not set Task to run on second Core or Processor");
                }

                // Prevents "Normal" processes from interrupting Threads
                process.PriorityClass = ProcessPriorityClass.High;
            }
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

        /// <summary>
        /// Sets the thread priority to highest
        /// </summary>
        protected void SetThreadPriority()
        {
            // Prevents "Normal" Threads from interrupting this thread
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
        }
    }
}
