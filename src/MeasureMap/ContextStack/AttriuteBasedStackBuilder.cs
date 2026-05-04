using System;
using System.Collections.Generic;

namespace MeasureMap.ContextStack
{
    /// <summary>
    /// Builds a context middleware stack using attribute-based configuration for a specified type.
    /// </summary>
    /// <remarks>This builder allows dynamic construction of middleware pipelines for context processing,
    /// leveraging attributes on the specified type parameter to determine stack behavior. It is typically used to
    /// compose and execute a sequence of context middleware components in profiling or task execution
    /// scenarios.</remarks>
    /// <typeparam name="T">The type of the context object to be used in the stack. Must be a reference type with a parameterless
    /// constructor.</typeparam>
    public class AttriuteBasedStackBuilder<T> : IContextStackBuilder where T : class, new()
    {
        private readonly List<Func<T, int, ProfilerSettings, IContextMiddleware>> _stack = [];
        private readonly Func<T, ITask> _taskFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskFactory"></param>
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

            var runner = new AttributeBasedStackRunner<T>(task);

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
