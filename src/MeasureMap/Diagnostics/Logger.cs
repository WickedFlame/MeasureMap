using System.Collections.Generic;

namespace MeasureMap.Diagnostics
{
    /// <inheritdoc />
    public class Logger : ILogger
    {
        private readonly List<ILogWriter> _writers;

        /// <summary>
        /// 
        /// </summary>
        public Logger()
        {
            _writers = new List<ILogWriter>();
        }

        /// <inheritdoc />
        public void Write(string message, LogLevel level = LogLevel.Info, string source = null)
        {
            if (level < MinLogLevel)
            {
                return;
            }

            foreach(var writer in _writers)
            {
                writer.Write(message, level, source);
            }
        }

        /// <summary>
        /// Defines the minimal <see cref="LogLevel"/>. All higher levels are writen to the log
        /// </summary>
        public LogLevel MinLogLevel { get; set; } = LogLevel.Warning;

        /// <summary>
        /// Add a <see cref="ILogWriter"/> to the logger
        /// </summary>
        /// <param name="writer"></param>
        /// <returns></returns>
        public ILogger AddWriter(ILogWriter writer)
        {
            _writers.Add(writer);
            return this;
        }

        /// <summary>
        /// Create a new logger
        /// </summary>
        /// <returns></returns>
        public static Logger Setup()
        {
            var logger = new Logger();
            foreach(var writer in GlobalConfiguration.LogWriters)
            {
                logger._writers.Add(writer);
            }

            return logger;
        }
    }
}
