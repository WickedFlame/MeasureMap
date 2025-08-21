namespace MeasureMap.RunnerHandlers
{
    public interface IRunnerMiddleware
    {
        /// <summary>
        /// Set the next execution item
        /// </summary>
        /// <param name="next">The next handler for the thread pipeline</param>
        void SetNext(IRunnerMiddleware next);

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="task">The task to run</param>
        /// <param name="settings">The settings for the profiler</param>
        /// <returns>The resulting collection of the executions</returns>
        IResult Execute(ITask task);
    }
}
