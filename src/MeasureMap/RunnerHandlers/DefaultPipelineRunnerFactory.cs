using System;

namespace MeasureMap.RunnerHandlers
{
    public class DefaultPipelineRunnerFactory : IPipelineRunnerFactory
    {
        public IRunnerMiddleware Create(int threadNumber, ProfilerSettings settings)
        {
            var runner = new DefaultPipelineRunner(threadNumber, settings);
            
            if(settings.OnStartPipeline != null)
            {
                runner.SetNext(new OnStartPipelineRunner(threadNumber, settings.OnStartPipeline));
            }

            if(settings.OnEndPipelineEvent != null)
            {
                runner.SetNext(new OnEndPipelineRunner(settings.OnEndPipelineEvent));
            }

            runner.SetNext(new WorkerPipelineRunner());

            return runner;
        }
    }

    public class WorkerPipelineRunner : IRunnerMiddleware
    {
        public void SetNext(IRunnerMiddleware next)
        {
            throw new InvalidOperationException();
        }

        public IResult Run(ITask task, IExecutionContext context)
        {
            var worker = new Worker();
            var result = worker.Run(task, context);

            return result;
        }
    }

    public class OnStartPipelineRunner : IRunnerMiddleware
    {
        private IRunnerMiddleware _next;
        private readonly int _threadNumber;
        private readonly Func<IExecutionContext> _onStartPipeline;

        public OnStartPipelineRunner(int threadNumber, Func<IExecutionContext> onStartPipeline)
        {
            _threadNumber = threadNumber;
            _onStartPipeline = onStartPipeline;
        }

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
            var ctx = _onStartPipeline();
            ctx.Set(ContextKeys.ThreadNumber, _threadNumber);
            context.Set(ContextKeys.ThreadNumber, _threadNumber);

            var result = _next != null ? _next.Run(task, ctx != null ? ctx : context) : null;

            return result;
        }
    }

    public class OnEndPipelineRunner : IRunnerMiddleware
    {
        private readonly Action<IExecutionContext> _onEndPipelineEvent;
        private IRunnerMiddleware _next;

        public OnEndPipelineRunner(Action<IExecutionContext> onEndPipelineEvent)
        {
            _onEndPipelineEvent = onEndPipelineEvent;
        }

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
            var result = _next != null ? _next.Run(task, context) : null;

            _onEndPipelineEvent(context);

            return result;
        }
    }
}
