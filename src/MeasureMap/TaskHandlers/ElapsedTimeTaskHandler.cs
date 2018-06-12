using System.Diagnostics;

namespace MeasureMap
{
    /// <summary>
    /// 
    /// </summary>
    public class ElapsedTimeTaskHandler : BaseTaskHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iteration">The current iteration</param>
        /// <returns></returns>
        public override IIterationResult Run(int iteration)
        {
            var sw = new Stopwatch();
            sw.Start();

            var result = base.Run(iteration);

            sw.Stop();

            result.Ticks = sw.ElapsedTicks;
            result.Duration = sw.Elapsed;

            return result;
        }
    }
}
