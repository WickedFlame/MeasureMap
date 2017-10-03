using System;

namespace MeasureMap
{
    public interface ITaskRunner
    {
        void Run();
    }

    public class TaskRunner : ITaskRunner
    {
        private readonly Action _task;

        public TaskRunner(Action task)
        {
            _task = task;
        }

        public void Run()
        {
            _task();
        }
    }

    public class TaskRunner<T> : ITaskRunner
    {
        private readonly Func<T, T> _task;
        private T _parameter;

        public TaskRunner(Func<T, T> task)
        {
            _task = task;
            _parameter = GetObject();
        }

        public void Run()
        {
            _parameter = _task(_parameter);
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
}
