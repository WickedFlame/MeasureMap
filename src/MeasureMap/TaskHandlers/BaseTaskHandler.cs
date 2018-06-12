
namespace MeasureMap
{
    public interface ITaskHandler
    {
        /// <summary>
        /// Set the next execution item
        /// </summary>
        /// <param name="next">The next executor</param>
        void SetNext(ITaskHandler next);

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <returns>The resulting collection of the executions</returns>
        IIterationResult Run();
    }


    public abstract class BaseTaskHandler : ITaskHandler
    {
        private ITaskHandler _next;

        /// <summary>
        /// Set the next execution item
        /// </summary>
        /// <param name="next">The next executor</param>
        public void SetNext(ITaskHandler next)
        {
            _next = next;
        }

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <returns>The resulting collection of the executions</returns>
        public virtual IIterationResult Run()
        {
            if(_next == null)
            {
                return new IterationResult();
            }

            return _next.Run();
        }
    }
}
