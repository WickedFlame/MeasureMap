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
        private readonly Stopwatch _stopWatch;

        /// <summary>
        /// 
        /// </summary>
        public SimpleTaskExecution()
        {
            _stopWatch = Stopwatch.StartNew();
        }

        /// <summary>
        /// Executes the task directly
        /// </summary>
        /// <param name="context"></param>
        /// <param name="execution"></param>
        public void Execute(IExecutionContext context, Action<IExecutionContext> execution)
        {
            execution(context);

            context.Logger.Write($"Elapsed {_stopWatch.Elapsed.TotalMilliseconds}", LogLevel.Debug, nameof(SimpleTaskExecution));

            // reset the time for the next execution
            _stopWatch.Restart();
        }
    }
}
