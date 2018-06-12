using System;

namespace MeasureMap
{
    public class MemoryCollectionTaskHandler : BaseTaskHandler
    {
        public override IIterationResult Run(int iteration)
        {
            var initial = GC.GetTotalMemory(true);

            var result = base.Run(iteration);

            result.InitialSize = initial;
            result.AfterExecution = GC.GetTotalMemory(false);
            ForceGarbageCollector();
            result.AfterGarbageCollection = GC.GetTotalMemory(true);

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
