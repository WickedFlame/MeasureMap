using MeasureMap.RunnerHandlers;
using System.Diagnostics;

namespace MeasureMap.ContextStack
{
    public class ProcessDataContextHandler : IRunnerMiddleware
    {
        private IRunnerMiddleware _next;

        public void SetNext(IRunnerMiddleware next)
        {
            if (_next != null)
            {
                _next.SetNext(next);
                return;
            }

            _next = next;
        }

        public IResult Run(ITask task, IExecutionContext context)
        {
            var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            var processId = Process.GetCurrentProcess().Id;

            context.Set(ContextKeys.ThreadId, threadId);
            context.Set(ContextKeys.ProcessId, processId);

            var result = _next != null ? _next.Run(task, context) : null;

            return result;
        }
    }
}
