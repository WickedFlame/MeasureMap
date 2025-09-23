using System;
using System.Collections.Generic;

namespace MeasureMap.ContextStack
{
    /// <summary>
    /// Builder to create a Context Stack that is run per thread
    /// Creates a new Instance of the IContextMiddleware per thread
    /// </summary>
    public class DefaultContextStackBuilder : IContextStackBuilder
    {
        private readonly List<Func<int, ProfilerSettings, IContextMiddleware>> _stack = [];

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
            var runner = new DefaultContextStackRunner();
            
            foreach(var middleware in _stack)
            {
                runner.SetNext(middleware.Invoke(threadNumber, settings));
            }

            runner.SetNext(new ProcessDataContextHandler());
            runner.SetNext(new WorkerContextHandler());

            return runner;
        }
    }
}
