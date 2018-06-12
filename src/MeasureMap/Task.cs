using System;
using System.Diagnostics;

namespace MeasureMap
{

    public interface ITask
    {
        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="context">The current execution context</param>
        /// <returns>The resulting collection of the executions</returns>
        IIterationResult Run(IExecutionContext context);
    }

    public class ContextTask : ITask
    {
        private readonly Action<IExecutionContext> _task;

        public ContextTask(Action<IExecutionContext> task)
        {
            _task = task;
        }

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="context">The current execution context</param>
        /// <returns>The resulting collection of the executions</returns>
        public IIterationResult Run(IExecutionContext context)
        {
            _task(context);

            return new IterationResult();
        }
    }
    
    public class Task : ITask
    {
        private readonly Action _task;

        public Task(Action task)
        {
            _task = task;
        }

        public IIterationResult Run(IExecutionContext context)
        {
            _task();

            var result = new IterationResult()
            {
                //Data = iteration
            };

            return result;
        }
    }

    public class Task<T> : ITask
    {
        private readonly Func<T, T> _task;
        private T _parameter;

        public Task(Func<T, T> task)
        {
            _task = task;
            _parameter = GetObject();
        }

        public Task(Func<T, T> task, T parameter)
        {
            _task = task;
            _parameter = parameter;
        }

        public IIterationResult Run(IExecutionContext context)
        {
            // ATENTION
            // TODO: Check if using _parameter is threadsafe

            _parameter = _task(_parameter);

            var result = new IterationResult()
            {
                Data = _parameter
            };

            return result;
        }

        private T GetObject()
        {
            try
            {
                if (typeof(T).IsValueType || typeof(T) == typeof(string))
                {
                    return default(T);
                }

                return (T) Activator.CreateInstance(typeof(T));
            }
            catch (MissingMethodException e)
            {
                throw new InvalidOperationException($"The object of Type {typeof(T).Name} does not contain a parameterless constructor", e);
            }
        }
    }

    public class IteratedTask : ITask
    {
        private readonly Action<int> _task;

        public IteratedTask(Action<int> task)
        {
            _task = task;
        }

        public IIterationResult Run(IExecutionContext context)
        {
            var iteration = context.Get<int>(ContextKeys.Iteration);

            _task(iteration);

            var result = new IterationResult()
            {
                Data = iteration
            };

            return result;
        }
    }

    public class OptionsTask : ITask
    {
        private readonly Action<int, ProfilerOptions> _task;

        public OptionsTask(Action<int, ProfilerOptions> task)
        {
            _task = task;
        }

        public IIterationResult Run(IExecutionContext context)
        {
            var iteration = context.Get<int>(ContextKeys.Iteration);

            var parameter = new ProfilerOptions
            {
                Iteration = iteration,
                ThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId,
                ProcessId = Process.GetCurrentProcess().Id
            };
            _task(iteration, parameter);

            var result = new IterationResult()
            {
                Data = parameter
            };

            return result;
        }
    }
}
