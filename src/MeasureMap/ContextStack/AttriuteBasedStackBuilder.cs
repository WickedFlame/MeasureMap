using System;
using System.Collections.Generic;
using System.Text;

namespace MeasureMap.ContextStack
{
    public class AttriuteBasedStackBuilder<T> : IContextStackBuilder where T : class, new()
    {
        private readonly List<Func<int, ProfilerSettings, IContextMiddleware>> _stack = [];
        private readonly Func<T, ITask> _taskFactory;

        public AttriuteBasedStackBuilder(Func<T, ITask> taskFactory)
        {
            _taskFactory = taskFactory;
        }

        /// <summary>
        /// Add a new middleware to the Context Stack
        /// </summary>
        /// <param name="middleware"></param>
        public void Add(Func<int, ProfilerSettings, IContextMiddleware> middleware)
        {
            _stack.Add(middleware);
        }

        /// <summary>
        /// Create a new instance of the Context Stack
        /// </summary>
        /// <param name="threadNumber"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public IContextMiddleware Create(int threadNumber, ProfilerSettings settings)
        {
            var instance = Activator.CreateInstance<T>();
            var task = _taskFactory(instance);

            var runner = new AttributeBasedStackRunner<T>(task);
            






            foreach (var middleware in _stack)
            {
                runner.SetNext(middleware.Invoke(threadNumber, settings));
            }

            runner.SetNext(new ProcessDataContextHandler());
            runner.SetNext(new WorkerContextHandler());

            return runner;
        }
    }
}
