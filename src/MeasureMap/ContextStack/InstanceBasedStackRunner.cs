using MeasureMap.IterationStack;

namespace MeasureMap.ContextStack
{
    /// <summary>
    /// StackRunner that runs the task based on the Attributes on the benchmark class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InstanceBasedStackRunner<T> : BaseContextHandler where T : class, new()
    {
        private readonly ITask _task;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        public InstanceBasedStackRunner(ITask task)
        {
            _task = task;
        }

        /// <summary>
        /// Creates a new instance of the benchmark class and runs the task with the context.
        /// </summary>
        /// <param name="_"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IResult Run(ITask _, IExecutionContext context)
        {
            //
            // Recreate the iterartionstack to ensure the task is run per thread and not shared between threads.
            // The task is created per thread and the context is passed to the task.

            var stack = new IterationStackBuilder();
            stack.SetNext(new ProcessDataIterationHandler());
            stack.SetNext(new MemoryCollectionIterationHandler());
            stack.SetNext(new ElapsedTimeIterationHandler());
            stack.SetNext(_task);

            return base.Run(stack, context);
        }
    }
}
