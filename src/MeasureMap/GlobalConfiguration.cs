using System.Collections.Generic;
using MeasureMap.Diagnostics;

namespace MeasureMap
{
    public static class GlobalConfiguration
    {
        public static void AddLogWriter(ILogWriter writer)
        {
            LogWriters.Add(writer);
        }

        internal static List<ILogWriter> LogWriters { get; } = new List<ILogWriter>();
    }
}
