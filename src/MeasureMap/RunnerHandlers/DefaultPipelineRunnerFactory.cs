using MeasureMap.ContextStack;
using System;
using System.Collections.Generic;

namespace MeasureMap.RunnerHandlers
{
    public class DefaultPipelineRunnerFactory : IPipelineRunnerFactory
    {
        private readonly List<Func<int, ProfilerSettings, IRunnerMiddleware>> _pipeline = [];

        public void Add(Func<int, ProfilerSettings, IRunnerMiddleware> middleware)
        {
            _pipeline.Add(middleware);
        }

        public IRunnerMiddleware Create(int threadNumber, ProfilerSettings settings)
        {
            var runner = new DefaultPipelineRunner();
            
            foreach(var middleware in _pipeline)
            {
                runner.SetNext(middleware.Invoke(threadNumber, settings));
            }

            runner.SetNext(new ProcessDataContextHandler());
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
        private readonly ProfilerSettings _settings;
        private readonly Func<ProfilerSettings, IExecutionContext> _onStartPipeline;

        public OnStartPipelineRunner(int threadNumber, ProfilerSettings settings, Func<ProfilerSettings, IExecutionContext> onStartPipeline)
        {
            _threadNumber = threadNumber;
            _settings = settings;
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
            var ctx = _onStartPipeline(_settings);
            if(ctx == null)
            {
                ctx = context;
            }

            ctx.Set(ContextKeys.ThreadNumber, _threadNumber);
            
            var result = _next != null ? _next.Run(task, ctx) : null;

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
