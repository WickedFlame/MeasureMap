﻿using MeasureMap.Diagnostics;
using System.Diagnostics;

namespace MeasureMap.Tracers
{
    /// <summary>
    /// Trace the output to the <see cref="Trace"/>
    /// </summary>
    public class TraceResultWriter : IResultWriter
    {
        /// <summary>
        /// Write a value to the <see cref="Trace"/>
        /// </summary>
        /// <param name="value"></param>
        public void Write(string value)
        {
            ConsoleWriter.Write(value);
        }

        /// <summary>
        /// Write a value to the <see cref="Trace"/> as a new line
        /// </summary>
        /// <param name="value"></param>
        public void WriteLine(string value)
        {
            ConsoleWriter.WriteLine(value);
        }
    }
}
