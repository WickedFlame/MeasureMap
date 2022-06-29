using MeasureMap.Diagnostics;
using System;
using System.Diagnostics;

namespace MeasureMap.Runners
{
    /// <summary>
    /// Executes the task at a given interval. To ensure the interval can be met the Task is executed in a own thread.
    /// </summary>
    public class TimedTaskExecution : ITaskExecution
    {
        private readonly long _interval;
        private readonly Logger _logger;
        private readonly Stopwatch _stopWatch;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interval"></param>
        public TimedTaskExecution(TimeSpan interval)
        {
            _interval = interval.Ticks;
            _logger = Logger.Setup();
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
        }

        /// <summary>
        /// Execute the task in a interval
        /// </summary>
        /// <param name="context"></param>
        /// <param name="execution"></param>
        public void Execute(IExecutionContext context, Action<IExecutionContext> execution)
        {
            while (_stopWatch.ElapsedTicks < _interval)
            {
                // just wait
                // this was found to be the most accurate way to delay the thread.
                // This has a max overhead of 4ms when using iterations of 1ms.
                // Intervals over 100ms only produce 0.1ms - 0.3ms overhead
                // 
                // Task.Delay(time).Wait() has a overhead of about 15ms which is too much
                //
            }

            _logger.Write($"Elapsed {_stopWatch.Elapsed.TotalMilliseconds}", LogLevel.Info, "TimedExecution");

            // reset the time for the next execution
            _stopWatch.Restart();

            execution(context);
        }
    }
}
