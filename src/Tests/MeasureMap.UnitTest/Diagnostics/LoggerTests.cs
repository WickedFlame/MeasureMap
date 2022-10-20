using MeasureMap.Diagnostics;
using Moq;
using NUnit.Framework;

namespace MeasureMap.UnitTest.Diagnostics
{
    public class LoggerTests
    {
        [TestCase(LogLevel.Debug, 5)]
        [TestCase(LogLevel.Info, 4)]
        [TestCase(LogLevel.Warning, 3)]
        [TestCase(LogLevel.Error, 2)]
        [TestCase(LogLevel.Critical, 1)]
        public void Logger_MinLogLevel(LogLevel minLevel, int calls)
        {
            var writer = new Mock<ILogWriter>();
            
            var logger = new Logger
            {
                MinLogLevel = minLevel
            };

            logger.AddWriter(writer.Object);

            logger.Write("Debug", LogLevel.Debug);
            logger.Write("Info", LogLevel.Info);
            logger.Write("Warning", LogLevel.Warning);
            logger.Write("Error", LogLevel.Error);
            logger.Write("Critical", LogLevel.Critical);

            writer.Verify(x => x.Write(It.IsAny<string>(), It.IsAny<LogLevel>(), null), Times.Exactly(calls));
            writer.Verify(x => x.Write(It.IsAny<string>(), It.Is<LogLevel>(l => l < minLevel), null), Times.Never);
        }

        [Test]
        public void Logger_LogLevel_Default()
        {
            var writer = new Mock<ILogWriter>();

            var logger = new Logger();

            logger.AddWriter(writer.Object);

            logger.Write("Debug", LogLevel.Debug);
            logger.Write("Info", LogLevel.Info);
            logger.Write("Warning", LogLevel.Warning);
            logger.Write("Error", LogLevel.Error);
            logger.Write("Critical", LogLevel.Critical);

            writer.Verify(x => x.Write(It.IsAny<string>(), It.IsAny<LogLevel>(), null), Times.Exactly(3));
            writer.Verify(x => x.Write(It.IsAny<string>(), It.Is<LogLevel>(l => l < LogLevel.Warning), null), Times.Never);
        }
    }
}
