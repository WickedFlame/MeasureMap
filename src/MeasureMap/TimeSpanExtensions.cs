using System.Diagnostics;

namespace MeasureMap
{
    /// <summary>
    /// Extensionmethods for TimeSpan and Ticks
    /// </summary>
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Convert any number of Ticks to Nanoseconds
        /// </summary>
        /// <param name="ticks"></param>
        /// <returns></returns>
        public static double ToNanoseconds(this long ticks)
        {
            return 1000000000.0 * (double)ticks / Stopwatch.Frequency;
        }

        /// <summary>
        /// Convert any number of Ticks to Microseconds
        /// </summary>
        /// <param name="ticks"></param>
        /// <returns></returns>
        public static double ToMicroseconds(this long ticks)
        {
            return 1000000.0 * (double)ticks / Stopwatch.Frequency;
        }

        /// <summary>
        /// Convert any number of Ticks to Milliseconds
        /// </summary>
        /// <param name="ticks"></param>
        /// <returns></returns>
        public static double ToMilliseconds(this long ticks)
        {
            return 1000.0 * (double)ticks / Stopwatch.Frequency;
        }
    }
}
