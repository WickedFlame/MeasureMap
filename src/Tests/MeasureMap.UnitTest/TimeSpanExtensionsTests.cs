using NUnit.Framework;
using FluentAssertions;

namespace MeasureMap.UnitTest
{
    public class TimeSpanExtensionsTests
    {
        [Test]
        public void TimeSpan_ToNanoseconds()
        {
            ((long)20).ToNanoseconds().Should().Be(2000);
        }

        [Test]
        public void TimeSpan_ToMicroseconds()
        {
            ((long)20).ToMicroseconds().Should().Be(2);
        }

        [Test]
        public void TimeSpan_ToMilliseconds()
        {
            ((long)20).ToMilliseconds().Should().Be(0.002);
        }
    }
}
