using System;

namespace MeasureMap
{
    /// <summary>
    /// Extension class for ProfilerSession
    /// </summary>
    public static class ProfilerSessionExtensions
    {
        /// <summary>
        /// Sets the Task that will be profiled
        /// </summary>
        /// <param name="session">The current session</param>
        /// <param name="task">The Task</param>
        /// <returns>The current profiling session</returns>
        public static ProfilerSession Task(this ProfilerSession session, Action task)
        {
            session.Task(new Task(task));

            return session;
        }

        /// <summary>
        /// Sets the Task that will be profiled
        /// </summary>
        /// <param name="session">The current session</param>
        /// <typeparam name="T">The return and parameter value</typeparam>
        /// <param name="task">The task to execute</param>
        /// <returns>The current profiling session</returns>
        public static ProfilerSession Task<T>(this ProfilerSession session, Func<T, T> task)
        {
            session.Task(new Task<T>(task));

            return session;
        }

        /// <summary>
        /// Sets the Task that will be profiled
        /// </summary>
        /// <param name="session">The current session</param>
        /// <typeparam name="T">The return and parameter value</typeparam>
        /// <param name="task">The task to execute</param>
        /// <param name="parameter">The parameter that is passed to the task</param>
        /// <returns>The current profiling session</returns>
        public static ProfilerSession Task<T>(this ProfilerSession session, Func<T, T> task, T parameter)
        {
            session.Task(new Task<T>(task, parameter));

            return session;
        }

        /// <summary>
        /// Sets the Task that will be profiled passing the current iteration index as parameter
        /// </summary>
        /// <param name="session">The current session</param>
        /// <param name="task">The task to execute</param>
        /// <returns>The current profiling session</returns>
        public static ProfilerSession Task(this ProfilerSession session, Action<int> task)
        {
            session.Task(new IteratedTask(task));

            return session;
        }

        /// <summary>
        /// Sets the Task that will be profiled passing the current iteration index as parameter
        /// </summary>
        /// <param name="session">The current session</param>
        /// <param name="task">The task to execute</param>
        /// <returns>The current profiling session</returns>
        public static ProfilerSession Task(this ProfilerSession session, Action<int, ProfilerOptions> task)
        {
            session.Task(new OptionsTask(task));

            return session;
        }
    }
}
