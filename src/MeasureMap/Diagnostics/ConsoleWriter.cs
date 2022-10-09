using System;
using System.Diagnostics;

namespace MeasureMap.Diagnostics
{
    /// <summary>
    /// 
    /// </summary>
    internal static class ConsoleWriter
    {
        /// <summary>
        /// Static init for consolewriter
        /// </summary>
        static ConsoleWriter()
        {
            var consoleOpen = true;
            try
            {
                int window_height = Console.WindowHeight;
            }
            catch
            {
                consoleOpen = false;
            }

            _write = consoleOpen ? s => Console.Write(s) : s => Trace.Write(s);
            _writeLine = consoleOpen ? s => Console.WriteLine(s) : s => Trace.WriteLine(s);
        }

        private static readonly Action<string> _write;
        private static readonly Action<string> _writeLine;

        /// <summary>
        /// Write to the console or to trace
        /// </summary>
        /// <param name="value"></param>
        public static void Write(string value)
            => _write(value);

        /// <summary>
        /// Write a line to the console or to trace
        /// </summary>
        /// <param name="value"></param>
        public static void WriteLine(string value) 
            => _writeLine(value);
    }
}
