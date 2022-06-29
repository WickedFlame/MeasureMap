using MeasureMap.Diagnostics;
using System;
using System.Diagnostics;

namespace MeasureMap.Runners
{
    /// <summary>
    /// Simple and direct execution ot the task
    /// </summary>
    public class SimpleTaskExecution : ITaskExecution
    {
        private readonly Logger _logger;
        private readonly Stopwatch _stopWatch;

        /// <summary>
        /// 
        /// </summary>
        public SimpleTaskExecution()
        {
            _logger = Logger.Setup();
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
        }

        /// <summary>
        /// Executes the task directly
        /// </summary>
        /// <param name="context"></param>
        /// <param name="execution"></param>
        public void Execute(IExecutionContext context, Action<IExecutionContext> execution)
        {
            execution(context);

            _logger.Write($"Elapsed {_stopWatch.Elapsed.TotalMilliseconds}", LogLevel.Info, "SimpleExecution");

            // reset the time for the next execution
            _stopWatch.Restart();
        }
    }
}
