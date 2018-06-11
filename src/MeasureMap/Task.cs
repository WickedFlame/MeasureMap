using System;
using System.Diagnostics;

namespace MeasureMap
{
    public interface ITask
    {
        object Run(int iteration);
    }

    public class Task : ITask
    {
        private readonly Action _task;

        public Task(Action task)
        {
            _task = task;
        }

        public object Run(int iteration)
        {
            _task();
            return iteration;
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

        public object Run(int iteration)
        {
            _parameter = _task(_parameter);
            return _parameter;
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

        public object Run(int iteration)
        {
            _task(iteration);
            return iteration;
        }
    }

    public class OptionsTask : ITask
    {
        private readonly Action<int, ProfilerOptions> _task;

        public OptionsTask(Action<int, ProfilerOptions> task)
        {
            _task = task;
        }

        public object Run(int iteration)
        {
            var parameter = new ProfilerOptions
            {
                Iteration = iteration,
                ThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId,
                ProcessId = Process.GetCurrentProcess().Id
            };
            _task(iteration, parameter);
            return parameter;
        }
    }
}
