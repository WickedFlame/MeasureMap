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
            return base.Run(_task, context);
        }
    }
}
