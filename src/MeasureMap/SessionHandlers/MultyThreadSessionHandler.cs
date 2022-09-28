using System;
using System.Linq;
using MeasureMap.Threading;

namespace MeasureMap
{
    /// <summary>
    /// A multy threaded task session handler
    /// </summary>
    public class MultyThreadSessionHandler : SessionHandler, IThreadSessionHandler, IDisposable
	{
		private readonly int _threadCount;
		private readonly WorkerThreadList _threads;

		/// <summary>
		/// Creates a new threaded task executor
		/// </summary>
		/// <param name="threadCount">The amount of threads to run the task</param>
		public MultyThreadSessionHandler(int threadCount)
		{
			_threadCount = threadCount;
			_threads = new WorkerThreadList();
		}

		/// <summary>
		/// Gets the amount of threads that the task is run in
		/// </summary>
		public int ThreadCount => _threadCount;

		/// <summary>
		/// Executes the task
		/// </summary>
		/// <param name="task">The task to run</param>
		/// <param name="settings">The settings for the profiler</param>
		/// <returns>The resulting collection of the executions</returns>
		public override IProfilerResult Execute(ITask task, ProfilerSettings settings)
        {
            var threads = new ThreadList();

			lock (_threads)
			{
				for (int i = 0; i < _threadCount; i++)
				{
					var thread = _threads.StartNew(i, () =>
					{
						var worker = new Worker(threads);
						var p = worker.Run(task, settings);
						return p;
					});

					System.Diagnostics.Trace.WriteLine($"[{DateTime.Now:o}] [MeasureMap] [Info] [{nameof(MultyThreadSessionHandler)}] Start thread {thread.Id}");
				}
			}

			while (CountOpenThreads() > 0)
			{
				_threads.WaitAll();
			}

			var results = _threads.Select(s => s.Result);

            threads.WaitAll();

			var collectîon = new ProfilerResult();
			collectîon.ResultValues.Add(ResultValueType.Threads, _threadCount);

			foreach (var result in results)
			{
				collectîon.Add(result);
			}

			return collectîon;
		}

		/// <summary>
		/// Dispose all threads
		/// </summary>
		public void DisposeThreads()
		{
			if (_threads == null)
			{
				return;
			}

			lock (_threads)
			{
				foreach (var thread in _threads.ToList())
				{
					System.Diagnostics.Trace.WriteLine($"MeasureMap - End thread {thread.Id}");
					thread.Dispose();
					_threads.Remove(thread);
				}
			}
		}

		private int CountOpenThreads()
		{
			lock (_threads)
			{
				return _threads.Count(t => t.IsAlive);
			}
		}

		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
        {
            Dispose(true);
			GC.SuppressFinalize(this);
        }

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            DisposeThreads();
		}
	}
}
