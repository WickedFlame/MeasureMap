namespace MeasureMap
{
    /// <summary>
    /// Represents a task handler that can be chained together with further task handlers
    /// </summary>
    public interface ITaskMiddleware : ITask
    {
        /// <summary>
        /// Set the next execution item
        /// </summary>
        /// <param name="next">The next executor</param>
        void SetNext(ITask next);
    }
}
