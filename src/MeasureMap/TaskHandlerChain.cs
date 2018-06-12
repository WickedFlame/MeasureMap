
namespace MeasureMap
{
    /// <summary>
    /// Chain of responnsibility Manager for running tasks
    /// </summary>
    public class TaskHandlerChain : ITaskHandler
    {
        private ITaskHandler _root;
        private ITaskHandler _last;

        /// <summary>
        /// Set the next execution item
        /// </summary>
        /// <param name="next">The next executor</param>
        public void SetNext(ITaskHandler next)
        {
            if (_root == null)
            {
                _root = next;
                _last = next;
            }
            else
            {
                _last.SetNext(next);
            }

            _last = next;
        }

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <returns>The resulting collection of the executions</returns>
        public IIterationResult Run()
        {
            return _root.Run();
        }
    }
}
