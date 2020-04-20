using System;

namespace MeasureMap
{
    /// <summary>
    /// Extension methods for BenchmarkRunner
    /// </summary>
    public static class BenchmarkRunnerExtensions
    {
        /// <summary>
        /// Sets the Task that will be benchmarked
        /// </summary>
        /// <param name="runner">The BenchmarkRunner</param>
        /// <param name="name">The name that defines the execution</param>
        /// <param name="task">The Task to benchmark</param>
        /// <returns></returns>
        public static BenchmarkRunner Task(this BenchmarkRunner runner, string name, Action task)
        {
            var session = ProfilerSession.StartSession()
                .Task(task);
            runner.AddSession(name, session);

            return runner;
        }

        /// <summary>
        /// Sets the Task that will be benchmarked
        /// </summary>
        /// <param name="runner">The BenchmarkRunner</param>
        /// <param name="name">The name that defines the execution</param>
        /// <param name="task">The Task to benchmark</param>
        /// <returns></returns>
        public static BenchmarkRunner Task<T>(this BenchmarkRunner runner, string name, Func<T, T> task)
        {
            var session = ProfilerSession.StartSession()
                .Task(task);
            runner.AddSession(name, session);

            return runner;
        }

        /// <summary>
        /// Sets the Task that will be benchmarked
        /// </summary>
        /// <param name="runner">The BenchmarkRunner</param>
        /// <param name="name">The name that defines the execution</param>
        /// <param name="task">The Task to benchmark</param>
        /// <param name="parameter">The parameter that is passed to the tasks</param>
        /// <returns></returns>
        public static BenchmarkRunner Task<T>(this BenchmarkRunner runner, string name, Func<T, T> task, T parameter)
        {
            var session = ProfilerSession.StartSession()
                .Task(task, parameter);
            runner.AddSession(name, session);

            return runner;
        }

        /// <summary>
        /// Sets the Task that will be benchmarked
        /// </summary>
        /// <param name="runner">The BenchmarkRunner</param>
        /// <param name="name">The name that defines the execution</param>
        /// <param name="task">The Task to benchmark</param>
        /// <returns></returns>
        public static BenchmarkRunner Task(this BenchmarkRunner runner, string name, Action<IExecutionContext> task)
        {
            var session = ProfilerSession.StartSession()
                .Task(task);
            runner.AddSession(name, session);

            return runner;
        }

        /// <summary>
        /// Sets the Task that will be benchmarked
        /// </summary>
        /// <param name="runner">The BenchmarkRunner</param>
        /// <param name="name">The name that defines the execution</param>
        /// <param name="task">The Task to benchmark</param>
        /// <returns></returns>
        public static BenchmarkRunner Task<T>(this BenchmarkRunner runner, string name, Func<IExecutionContext, T> task)
        {
            var session = ProfilerSession.StartSession()
                .Task(task);
            runner.AddSession(name, session);

            return runner;
        }
    }
}
