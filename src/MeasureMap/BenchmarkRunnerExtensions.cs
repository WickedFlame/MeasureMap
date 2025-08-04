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
        /// Defines if the tasks are initially run as a warmup. Defaults to true
        /// </summary>
        /// <param name="runner"></param>
        /// <param name="run"></param>
        /// <returns></returns>
        public static BenchmarkRunner RunWarmup(this BenchmarkRunner runner, bool run)
        {
            runner.Settings.RunWarmup = run;

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

        /// <summary>
        /// Set the <see cref="ThreadBehaviour"/> to the settings of the <see cref="BenchmarkRunner"/>.<br></br>
        /// Using <see cref="ThreadBehaviour.RunOnMainThread"/> overwrites the ThreadingHandler defined in each ProfilerSession that is run in the Benchmark.
        /// </summary>
        /// <param name="runner"></param>
        /// <param name="behaviour"></param>
        /// <returns></returns>
        public static BenchmarkRunner SetThreadBehaviour(this BenchmarkRunner runner, ThreadBehaviour behaviour)
        {
            runner.Settings.ThreadBehaviour = behaviour;
            return runner;
        }


        /// <summary>
        /// Writes all logs to the console output
        /// </summary>
        /// <param name="runner"></param>
        /// <returns></returns>
        public static BenchmarkRunner LogToConsole(this BenchmarkRunner runner)
        {
            var writer = new Diagnostics.TraceLogWriter();
            runner.Settings.Logger.AddWriter(writer);
            GlobalConfiguration.LogWriters.Add(writer);
            return runner;
        }

        /// <summary>
        /// Event that is executed at the start of each thread run
        /// </summary>
        /// <param name="runner"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static BenchmarkRunner OnStartPipeline(this BenchmarkRunner runner, Func<ProfilerSettings, IExecutionContext> factory)
        {
            runner.Settings.OnStartPipelineEvent = factory;
            return runner;
        }

        /// <summary>
        /// Event that is executed at the end of each thread run
        /// </summary>
        /// <param name="runner"></param>
        /// <param name="event"></param>
        /// <returns></returns>
        public static BenchmarkRunner OnEndPipeline(this BenchmarkRunner runner, Action<IExecutionContext> @event)
        {
            runner.Settings.OnEndPipelineEvent = @event;
            return runner;
        }
    }
}
