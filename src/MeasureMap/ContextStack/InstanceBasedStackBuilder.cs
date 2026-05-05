using System;
using System.Collections.Generic;

namespace MeasureMap.ContextStack
{
    /// <summary>
    /// Builds a context middleware stack using instance-based configuration for a specified type.
    /// Each call to Create will create a new instance of the specified type and use it to configure the middleware stack.
    /// </summary>
    /// <typeparam name="T">The type of the context object to be used in the stack. Must be a reference type with a parameterless
    /// constructor.</typeparam>
    public class InstanceBasedStackBuilder<T> : IContextStackBuilder where T : class, new()
    {
        private readonly List<Func<T, int, ProfilerSettings, IContextMiddleware>> _stack = [];
        private readonly Func<T, ITask> _taskFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskFactory"></param>
        public InstanceBasedStackBuilder(Func<T, ITask> taskFactory)
        {
            _taskFactory = taskFactory;
        }

        /// <summary>
        /// Add a new middleware to the Context Stack
        /// </summary>
        /// <param name="middleware"></param>
        public void Add(Func<int, ProfilerSettings, IContextMiddleware> middleware)
        {
            _stack.Add((_, i, s) => middleware(i, s));
        }

        /// <summary>
        /// Add a new middleware to the Context Stack
        /// </summary>
        /// <param name="middlewareFactory"></param>
        public void Add(Func<T, int, ProfilerSettings, IContextMiddleware> middlewareFactory)
        {
            _stack.Add(middlewareFactory);
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

            var runner = new InstanceBasedStackRunner<T>(task);

            foreach (var middlewareFactory in _stack)
            {
                runner.SetNext(middlewareFactory(instance, threadNumber, settings));
            }

            runner.SetNext(new ProcessDataContextHandler());
            runner.SetNext(new WorkerContextHandler());

            return runner;
        }
    }
}
