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

            return TimeSpan.FromTicks(result.Iterations.Sum(i => i.Duration.Ticks));
        }

        /// <summary>
        /// Returns the average throughput per second
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static double Throughput(this IResult result)
        {
            var elapsed = result.Elapsed();
            if (elapsed == TimeSpan.MinValue)
            {
                elapsed = result.TotalTime;
            }

            return Math.Round(result.Iterations.Count() / elapsed.TotalSeconds, 5);
        }

        public static double AverageMilliseconds(this IResult result)
        {
            return result.AverageTicks.ToMilliseconds();
        }
    }
}
