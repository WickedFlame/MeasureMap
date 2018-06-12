using System;

namespace MeasureMap
{
    public class MemoryCollectionTaskHandler : BaseTaskHandler
    {
        public override IIterationResult Run(int iteration)
        {
            var initial = GC.GetTotalMemory(true);

            var result = base.Run(iteration);

            var after = GC.GetTotalMemory(false);
            ForceGarbageCollector();
            var afterCollect = GC.GetTotalMemory(true);

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
