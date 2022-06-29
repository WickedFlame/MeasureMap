using System;
using System.Threading;

namespace MeasureMap
{
    public class SessionThread
    {
        private Thread _thread;

        public SessionThread(int index, Func<Result> action)
        {
            _thread = new Thread(new ThreadStart(() =>
            {
                Result = action();

            }))
            {
                IsBackground = true,
                Name = $"MeasuerMap_{index}",
                Priority = ThreadPriority.Highest
            };
        }

        public Result Result { get; private set; }

        public int Id => _thread.ManagedThreadId;

        public bool IsAlive => _thread.IsAlive;

        public void Start()
        {
            _thread.Start();
        }

        internal void Dispose()
        {
        }
    }
}
