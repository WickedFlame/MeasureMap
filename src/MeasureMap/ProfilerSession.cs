using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MeasureMap
{
    public class ProfilerSession
    {
        private static readonly bool IsRunningOnMono;

        static ProfilerSession()
        {
            IsRunningOnMono = Type.GetType("Mono.Runtime") != null;
        }

        protected ProfilerSession()
        {
        }

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
                    Trace.WriteLine("Could not set Task to run on second Core or Processor");
                }

                // Prevents "Normal" processes from interrupting Threads
                process.PriorityClass = ProcessPriorityClass.High;
            }
        }

        protected void ClearGarbageCollector()
        {
            // clean up
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        protected void SetThreadPriority()
        {
            // Prevents "Normal" Threads from interrupting this thread
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
        }
    }
}
