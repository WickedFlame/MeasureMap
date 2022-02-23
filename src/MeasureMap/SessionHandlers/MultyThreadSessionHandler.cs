using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MeasureMap
{
    /// <summary>
    /// A multy threaded task session handler
    /// </summary>
    public class MultyThreadSessionHandler : SessionHandler, IThreadSessionHandler, IDisposable
	{
		private readonly int _threadCount;
		private readonly List<System.Threading.Tasks.Task<Result>> _threads;

		/// <summary>
		/// Creates a new threaded task executor
		/// </summary>
		/// <param name="threadCount">The amount of threads to run the task</param>
		public MultyThreadSessionHandler(int threadCount)
		{
			_threadCount = threadCount;
			_threads = new List<System.Threading.Tasks.Task<Result>>();
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
			lock (_threads)
			{
				for (int i = 0; i < _threadCount; i++)
				{
					var thread = ThreadHelper.QueueTask(i, threadIndex =>
					{
						var worker = new Worker();
						var p = worker.Run(task, settings);
						return p;
					});

					System.Diagnostics.Trace.WriteLine($"MeasureMap - Start thread {thread.Id}");

					_threads.Add(thread);
				}

				foreach (var thread in _threads)
				{
					thread.Start();
				}
			}

			while (CountOpenThreads() > 0)
			{
				System.Threading.Tasks.Task.WaitAll(GetAwaitableThreads(), -1, CancellationToken.None);
			}

			var results = _threads.Select(s => s.Result);

			var collectîon = new ProfilerResult();
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

		private System.Threading.Tasks.Task[] GetAwaitableThreads()
		{
			lock (_threads)
			{
				return _threads.Where(t => !t.IsCanceled && !t.IsFaulted && !t.IsCompleted).ToArray();
			}
		}

		private int CountOpenThreads()
		{
			lock (_threads)
			{
				return _threads.Count(t => !t.IsCanceled && !t.IsFaulted && !t.IsCompleted);
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

		~MultyThreadSessionHandler()
		{
			DisposeThreads();
		}
	}
}
