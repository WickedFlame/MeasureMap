using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using MeasureMap.ContextStack;
using MeasureMap.Diagnostics;
using MeasureMap.Threading;

namespace MeasureMap.SessionStack
{
	/// <summary>
	/// A multy threaded task session handler
	/// </summary>
	public class MultiThreadSessionHandler : SessionHandler, ISessionExecutor
	{
		private readonly int _threadCount;
        private readonly TimeSpan _rampupTime;
        private readonly WorkerThreadList _threads;
		private ILogger _logger;

		/// <summary>
		/// Creates a new threaded task executor
		/// </summary>
		/// <param name="threadCount">The amount of threads to run the task</param>
		public MultiThreadSessionHandler(int threadCount)
			: this(threadCount, TimeSpan.Zero)
		{
		}

        /// <summary>
        /// Creates a new threaded task executor
        /// </summary>
        /// <param name="threadCount">The amount of threads to run the task</param>
		/// <param name="rampupTime">The time it takes to setup all threads</param>
        public MultiThreadSessionHandler(int threadCount, TimeSpan rampupTime)
        {
            _threadCount = threadCount;
			_rampupTime = rampupTime;
            _threads = [];
        }

        /// <summary>
        /// Gets or sets the <see cref="IContextStackBuilder"/> to create the ContextStack that runs the task
        /// </summary>
        public IContextStackBuilder StackBuilder { get; set; } = new DefaultContextStackBuilder();

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
			var sw = Stopwatch.StartNew();
			_logger = settings.Logger;

			var threadWaitHandle = new ManualResetEvent(false);

            //The ramp-up time is the amount of time to get to the full number of virtual users for the load test. If the number of virtual users is 20, and the ramp-up time is 120 seconds, then it takes 120 seconds to get to all 20 virtual users
			var rampup = _rampupTime > TimeSpan.Zero ? _rampupTime.TotalSeconds / _threadCount : 0;

			lock (_threads)
			{
				for (var i = 0; i < _threadCount; i++)
				{
					System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(rampup)).Wait();
                    settings.Logger.Write($"Start Thread {i} of {_threadCount}", LogLevel.Info, nameof(MultiThreadSessionHandler));

                    var thread = _threads.StartNew(i, idx =>
					{
						if (idx == _threadCount)
						{
							//
							// Release all waiting threads to start work after all threads are started
							threadWaitHandle.Set();
						}

						if (idx < _threadCount)
						{
							//
							// Wait at max 5 Sec to continue
							// Thread creation can delay the whole process too long
							settings.Logger.Write($"Waiting for all threads to start. Current ThreadCount {idx} of {_threadCount}", LogLevel.Debug, nameof(MultiThreadSessionHandler));
							threadWaitHandle.WaitOne(5000, true);
						}

                        var runner = StackBuilder.Create(idx, settings);
                        return runner.Run(task, settings.CreateContext());
					}, settings.GetThreadFactory());

					settings.Logger.Write($"Start thread {thread.Id}", LogLevel.Debug, nameof(MultiThreadSessionHandler));
				}
			}

			settings.Logger.Write($"Starting {_threadCount} threads took {sw.ElapsedTicks.ToMilliseconds()} ms", LogLevel.Info, nameof(MultiThreadSessionHandler));

			while (CountOpenThreads() > 0)
			{
				_threads.WaitAll();
			}

			var results = _threads.Select(s => s.Result);

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
					if (_logger != null)
					{
						_logger.Write($"End thread {thread.Id}", LogLevel.Debug, nameof(MultiThreadSessionHandler));
					}

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
