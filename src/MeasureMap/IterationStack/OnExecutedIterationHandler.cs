using System;

namespace MeasureMap.IterationStack
{
    /// <summary>
    /// TaskHandler that executes a delegate after the taskrun
    /// </summary>
    public class OnExecutedIterationHandler : IterationHandler
    {
        private readonly Action<IIterationResult> _execution;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        public OnExecutedIterationHandler(Action<IIterationResult> execution)
        {
            _execution = execution;
        }
        
        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="context">The current execution context</param>
        /// <returns>The resulting collection of the executions</returns>
        public override IIterationResult Run(IExecutionContext context)
        {
            var result = base.Run(context);

            System.Threading.Tasks.Task.Run(() => _execution.Invoke(result));

            return result;
        }
    }
}
