using MeasureMap.Threading;
using System;

namespace MeasureMap
{
    /// <summary>
    /// Extensions for <see cref="ProfilerSettings"/>
    /// </summary>
    public static class ProfilerSettingsExtensions
    {
        /// <summary>
        /// Get the ThreadFactory based on the <see cref="ThreadBehaviour"/> setting in <see cref="ProfilerSettings"/>
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static Func<int, Func<int, IResult>, IWorkerThread> GetThreadFactory(this ProfilerSettings settings)
        {
            return settings.ThreadBehaviour switch
            {
                ThreadBehaviour.Thread => WorkerThread.Factory,
                ThreadBehaviour.Task => WorkerTask.Factory,
                _ => WorkerThread.Factory
            };
        }

        /// <summary>
        /// Create a new ExecutionContext based on the Settings. This can be used in the OnStartPipeline Event to create the IExecutionContext for the Pipeline
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static IExecutionContext CreateContext(this IProfilerSettings settings)
        {
            return new ExecutionContext(settings);
        }
    }
}
