﻿
namespace MeasureMap.Diagnostics
{
    /// <summary>
    /// The logger
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Write a message to the log
        /// </summary>
        /// <param name="message"></param>
        /// <param name="level"></param>
        /// <param name="source"></param>
        void Write(string message, LogLevel level = LogLevel.Info, string source = null);

        /// <summary>
        /// Defines the minimal <see cref="LogLevel"/>. All higher levels are writen to the log
        /// </summary>
        LogLevel MinLogLevel { get; set; }
    }
}
