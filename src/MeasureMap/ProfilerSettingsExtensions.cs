﻿using MeasureMap.Threading;
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
        public static Func<int, Func<Result>, IWorkerThread> GetThreadFactory(this ProfilerSettings settings)
        {
            return settings.ThreadBehaviour switch
            {
                ThreadBehaviour.Thread => WorkerThread.Factory,
                ThreadBehaviour.Task => WorkerTask.Factory,
                _ => WorkerThread.Factory
            };
        }
    }
}
