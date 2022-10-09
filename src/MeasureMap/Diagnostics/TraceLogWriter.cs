using System;
using System.Diagnostics;

namespace MeasureMap.Diagnostics
{
    /// <summary>
    /// Logwritert that writes all messages to the diagnostics console
    /// </summary>
    public class TraceLogWriter : ILogWriter
    {
        /// <inheritdoc />
        public void Write(string message, LogLevel level, string source = null)
        {
            ConsoleWriter.WriteLine($"[{DateTime.Now:o}] [MeasureMap] [{source}] [{level}] {message}");
        }
    }
}
