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
        public void ResultExtensions_Throughput_Elapsed()
        {
            var result = new Result();
            result.Add(new IterationResult());
            result.ResultValues.Add(ResultValueType.Elapsed, TimeSpan.FromTicks(20));

            result.Throughput().Should().Be(500000.0);
        }

        [Test]
        public void ResultExtensions_Throughput_OneSecond()
        {
            var result = new Result();
            result.Add(new IterationResult { Ticks = TimeSpan.FromSeconds(1).Ticks });
            result.Add(new IterationResult { Ticks = TimeSpan.FromSeconds(1).Ticks });
            result.Add(new IterationResult { Ticks = TimeSpan.FromSeconds(1).Ticks });
            result.Add(new IterationResult { Ticks = TimeSpan.FromSeconds(1).Ticks });
            result.Add(new IterationResult { Ticks = TimeSpan.FromSeconds(1).Ticks });

            result.Throughput().Should().Be(1);
        }

        [Test]
        public void ResultExtensions_Throughput_TwoSecond()
        {
            var result = new Result();
            result.Add(new IterationResult { Ticks = TimeSpan.FromSeconds(2).Ticks });
            result.Add(new IterationResult { Ticks = TimeSpan.FromSeconds(2).Ticks });
            result.Add(new IterationResult { Ticks = TimeSpan.FromSeconds(2).Ticks });
            result.Add(new IterationResult { Ticks = TimeSpan.FromSeconds(2).Ticks });
            result.Add(new IterationResult { Ticks = TimeSpan.FromSeconds(2).Ticks });

            result.Throughput().Should().Be(.5);
        }

        [Test]
        public void ResultExtensions_Throughput_HalfSecond()
        {
            var result = new Result();
            result.Add(new IterationResult { Ticks = TimeSpan.FromSeconds(.5).Ticks });
            result.Add(new IterationResult { Ticks = TimeSpan.FromSeconds(.5).Ticks });
            result.Add(new IterationResult { Ticks = TimeSpan.FromSeconds(.5).Ticks });
            result.Add(new IterationResult { Ticks = TimeSpan.FromSeconds(.5).Ticks });
            result.Add(new IterationResult { Ticks = TimeSpan.FromSeconds(.5).Ticks });

            result.Throughput().Should().Be(2);
        }

        [Test]
        public void ResultExtensions_Throughput_ProfilerResult_OneSecond()
        {
            var res = new Result();
            res.Add(new IterationResult { Ticks = TimeSpan.FromSeconds(1).Ticks });

            var res2 = new Result();
            res2.Add(new IterationResult { Ticks = TimeSpan.FromSeconds(1).Ticks });

            var result = new ProfilerResult
            {
                res,
                res2
            };

            result.Throughput().Should().Be(1);
        }

        [Test]
        public void ResultExtensions_Throughput_ProfilerResult_TwoSecond()
        {
            var res = new Result();
            res.Add(new IterationResult { Ticks = TimeSpan.FromSeconds(2).Ticks });

            var res2 = new Result();
            res2.Add(new IterationResult { Ticks = TimeSpan.FromSeconds(2).Ticks });

            var result = new ProfilerResult
            {
                res,
                res2
            };

            result.Throughput().Should().Be(.5);
        }

        [Test]
        public void ResultExtensions_Throughput_ProfilerResult_HalfSecond()
        {
            var res = new Result();
            res.Add(new IterationResult { Ticks = TimeSpan.FromSeconds(.5).Ticks });

            var res2 = new Result();
            res2.Add(new IterationResult { Ticks = TimeSpan.FromSeconds(.5).Ticks });

            var result = new ProfilerResult
            {
                res,
                res2
            };

            result.Throughput().Should().Be(2);
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
