using System;

namespace MeasureMap.Threading
{
    public interface IThreadList
    {
        void StartNew(Action execution);

        /// <summary>
        /// Wait for all threads to end
        /// </summary>
        void WaitAll();
    }
}
