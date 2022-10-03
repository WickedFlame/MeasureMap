using System;
using System.Diagnostics;

namespace MeasureMap.Diagnostics
{
    /// <summary>
    /// Logwritert that writes all messages to the diagnostics console
    /// </summary>
    public class TraceLogWriter : ILogWriter
    {
        private readonly Action<string> _writeLine = Environment.UserInteractive ? Console.WriteLine : s => Trace.WriteLine(s);

        /// <inheritdoc />
        public void Write(string message, LogLevel level, string source = null)
        {
            _writeLine($"[{DateTime.Now:o}] [MeasureMap] [{source}] [{level}] {message}");
        }
    }
}
