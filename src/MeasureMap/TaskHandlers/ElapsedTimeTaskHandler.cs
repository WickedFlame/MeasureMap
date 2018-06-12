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
        /// <returns></returns>
        public override IIterationResult Run()
        {
            var sw = new Stopwatch();
            sw.Start();

            var result = base.Run();

            sw.Stop();

            result.Ticks = sw.ElapsedTicks;
            result.Duration = sw.Elapsed;

            return result;
        }
    }
}
