using MeasureMap.IterationStack;

namespace MeasureMap.ContextStack
{
    /// <summary>
    /// StackRunner that runs the task based on the Attributes on the benchmark class. Uses <see cref="AttributeSessionBuilder{T}"/> to build the session.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InstanceBasedStackRunner<T> : BaseContextHandler where T : class, new()
    {
        private readonly ITask _task;

        public InstanceBasedStackRunner(ITask task)
        {
            _task = task;
        }

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
