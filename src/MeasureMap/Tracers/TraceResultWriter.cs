using System;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;

namespace MeasureMap.Tracers
{
    /// <summary>
    /// Trace the output to the <see cref="Trace"/>
    /// </summary>
    public class TraceResultWriter : IResultWriter
    {
        private static Action<string> _write = Environment.UserInteractive ? s => Console.Write(s) : s => Trace.Write(s);
        private static Action<string> _writeLine = Environment.UserInteractive ? s => Console.WriteLine(s) : s => Trace.WriteLine(s);

        /// <summary>
        /// Write a value to the <see cref="Trace"/>
        /// </summary>
        /// <param name="value"></param>
        public void Write(string value)
        {
            _write(value);
        }

        /// <summary>
        /// Write a value to the <see cref="Trace"/> as a new line
        /// </summary>
        /// <param name="value"></param>
        public void WriteLine(string value)
        {
            _writeLine(value);
        }
    }
}
