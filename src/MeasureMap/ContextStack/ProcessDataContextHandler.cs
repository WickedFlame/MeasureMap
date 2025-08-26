using System.Diagnostics;

namespace MeasureMap.ContextStack
{
    public class ProcessDataContextHandler : BaseContextHandler
    {
        public override IResult Run(ITask task, IExecutionContext context)
        {
            var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            var processId = Process.GetCurrentProcess().Id;

            context.Set(ContextKeys.ThreadId, threadId);
            context.Set(ContextKeys.ProcessId, processId);

            var result = base.Run(task, context);

            return result;
        }
    }
}
