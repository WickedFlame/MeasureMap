using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MeasureMap
{
    internal static class ThreadHelper
    {
        public static Task<ProfilerResult> QueueTask(int index, bool threadAffinity, Func<int, ProfilerResult> action)
        {
            var task = new Task<ProfilerResult>(() =>
            {
                if (threadAffinity)
                {
                    Thread.BeginThreadAffinity();
                    var affinity = GetAffinity(index + 1, Environment.ProcessorCount);
                    GetCurrentThread().ProcessorAffinity = new IntPtr(1 << affinity);
                }

                var result = action.Invoke(index);

                if (threadAffinity)
                {
                    Thread.EndThreadAffinity();
                }

                return result;
            });

            return task;
        }

        [DllImport("kernel32.dll")]
        private static extern int GetCurrentThreadId();

        private static ProcessThread GetCurrentThread()
        {
            var id = GetCurrentThreadId();
            var process = (from ProcessThread th in System.Diagnostics.Process.GetCurrentProcess().Threads
                           where th.Id == id
                           select th).Single();

            return process;
        }

        private static int GetAffinity(int index, int cores)
        {
            var affinity = index * 2 % cores;

            if (index % cores >= cores / 2)
            {
                affinity++;
            }

            return affinity;
        }
    }
}
