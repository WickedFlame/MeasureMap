using System;
using FluentAssertions;
using NUnit.Framework;

namespace MeasureMap.UnitTest
{
    public class ResultExtensionTests
    {
        [Test]
        public void ResultExtensions_Elapsed()
        {
            var result = new Result();
            result.ResultValues.Add(ResultValueType.Elapsed, TimeSpan.Zero);

            result.Elapsed().Should().Be(TimeSpan.Zero);
        }

        [Test]
        public void ResultExtensions_Throughput()
        {
            var result = new Result();
            result.Add(new IterationResult());
            result.ResultValues.Add(ResultValueType.Elapsed, TimeSpan.FromTicks(20));

            result.Throughput().Should().Be(500000.0);
        }

        [Test]
        public void ResultExtensions_Throughput_FromTotalTime()
        {
            var result = new Result();
            result.Add(new IterationResult { Ticks = 20 });
            result.ResultValues.Add(ResultValueType.Elapsed, TimeSpan.MinValue);

            result.Throughput().Should().Be(500000.0);
        }
    }
}
