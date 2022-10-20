using MeasureMap.Diagnostics;
using System;

namespace MeasureMap
{
    /// <summary>
    /// 
    /// </summary>
    public static class BenchmarkRunnerExtensions
    {
        /// <summary>
        /// Set the amount of iterations that the benchmarks run
        /// </summary>
        /// <param name="runner">The benchmark runner</param>
        /// <param name="iterations">The amount of iterations</param>
        /// <returns></returns>
        public static BenchmarkRunner SetIterations(this BenchmarkRunner runner, int iterations)
        {
            if (iterations < 1)
            {
                throw new InvalidOperationException("Invalid amount of iterations. There have to be at lease 1 iteration");
            }

            runner.Settings.Iterations = iterations;

            return runner;
        }

        /// <summary>
        /// Sets the duration that the profileing session should run the task for
        /// </summary>
        /// <param name="runner">The benchmark runner</param>
        /// <param name="duration">The iterations to run the task</param>
        /// <returns>The current profiling session</returns>
        public static BenchmarkRunner SetDuration(this BenchmarkRunner runner, TimeSpan duration)
        {
            runner.Settings.Duration = duration;

            return runner;
        }

        /// <summary>
        /// Sets the amount of iterations that the profileing session should run the task
        /// </summary>
        /// <param name="runner">The benchmark runner</param>
        /// <param name="settings">The settings for thr profiler</param>
        /// <returns>The current profiling session</returns>
        public static BenchmarkRunner SetSettings(this BenchmarkRunner runner, ProfilerSettings settings)
        {
            settings.MergeChangesTo(runner.Settings);

            return runner;
        }

		/// <summary>
		/// Set values in the settings
		/// </summary>
		/// <param name="runner">The benchmark runner</param>
		/// <param name="settings">The settings for thr profiler</param>
		/// <returns></returns>
		public static BenchmarkRunner Settings(this BenchmarkRunner runner, Action<ProfilerSettings> settings)
        {
	        settings(runner.Settings);

	        return runner;
        }

        /// <summary>
        /// Defines the minimal <see cref="LogLevel"/>. All higher levels are writen to the log
        /// </summary>
        /// <param name="runner"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static BenchmarkRunner SetMinLogLevel(this BenchmarkRunner runner, LogLevel level)
        {
            runner.Settings.Logger.MinLogLevel = level;
            return runner;
        }
    }
}
