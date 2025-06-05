using System;
using System.Linq;

namespace MeasureMap
{
    /// <summary>
    /// Extensions on <see cref="IResult"/>
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// Returns the timespan that the complete Session took
        /// </summary>
        /// <param name="result">The ProfilerResult</param>
        /// <returns>The timespan that the session took</returns>
        public static TimeSpan Elapsed(this IResult result)
        {
            if (result.ResultValues.ContainsKey(ResultValueType.Elapsed))
            {
                return (TimeSpan)result.ResultValues[ResultValueType.Elapsed];
            }

            return TimeSpan.FromMilliseconds(result.Iterations.Sum(i => i.Ticks).ToMilliseconds());
        }

        /// <summary>
        /// Returns the average throughput per second
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static double Throughput(this IResult result)
        {
            var elapsed = result.TotalTime;
            if (elapsed == TimeSpan.Zero)
            {
                elapsed = result.Elapsed();
            }

            return Math.Round(result.Iterations.Count() / elapsed.TotalSeconds, 5);
        }

        /// <summary>
        /// Gets the average milliseconds that the <see cref="IResult"/> recorded
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static double AverageMilliseconds(this IResult result)
        {
            return result.AverageTicks.ToMilliseconds();
        }
    }
}
