using MeasureMap.Tracers.Metrics;

namespace MeasureMap.Tracers
{
    /// <summary>
    /// 
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Pad a string to a direction with a min width
        /// </summary>
        /// <param name="value"></param>
        /// <param name="align"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static string Pad(this string value, TextAlign align, int width)
        {
            return align == TextAlign.Left ? value.PadRight(width) : value.PadLeft(width);
        }

        /// <summary>
        /// Pad a string to a direction with a min width with a default char
        /// </summary>
        /// <param name="value"></param>
        /// <param name="align"></param>
        /// <param name="width"></param>
        /// <param name="paddingChar"></param>
        /// <returns></returns>
        public static string Pad(this string value, TextAlign align, int width, char paddingChar)
        {
            return align == TextAlign.Left ? value.PadLeft(width, paddingChar) : value.PadRight(width, paddingChar);
        }

        /// <summary>
        /// Get the tracelength of a string. This is used for getting the length of <see cref="ProfilerMetric"/>, <see cref="ThreadMetric"/> and <see cref="IterationMetric"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int TraceLength(this string value)
        {
            return value switch
            {
                "TimeStamp" => 27,
                "Avg. Time" => 16,
                "Fastest" => 16,
                "Slowest" => 16,
                "Throughput" => 14,
                "Total Time" => 16,
                "Duration" => 16,
                _ => value != null ? value.Length : 0
            };
        }
    }
}
