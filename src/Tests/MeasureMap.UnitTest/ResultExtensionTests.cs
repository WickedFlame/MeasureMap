using System;
using System.Diagnostics;

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
            result.Add(new IterationResult { Ticks = SecondsToTicks(1) });
            result.Add(new IterationResult { Ticks = SecondsToTicks(1) });
            result.Add(new IterationResult { Ticks = SecondsToTicks(1) });
            result.Add(new IterationResult { Ticks = SecondsToTicks(1) });
            result.Add(new IterationResult { Ticks = SecondsToTicks(1) });

            result.Throughput().Should().Be(1);
        }

        [Test]
        public void ResultExtensions_Throughput_TwoSecond()
        {
            var result = new Result();
            result.Add(new IterationResult { Ticks = SecondsToTicks(2) });
            result.Add(new IterationResult { Ticks = SecondsToTicks(2) });
            result.Add(new IterationResult { Ticks = SecondsToTicks(2) });
            result.Add(new IterationResult { Ticks = SecondsToTicks(2) });
            result.Add(new IterationResult { Ticks = SecondsToTicks(2) });

            result.Throughput().Should().Be(.5);
        }

        [Test]
        public void ResultExtensions_Throughput_HalfSecond()
        {
            var result = new Result();
            result.Add(new IterationResult { Ticks = SecondsToTicks(.5) });
            result.Add(new IterationResult { Ticks = SecondsToTicks(.5) });
            result.Add(new IterationResult { Ticks = SecondsToTicks(.5) });
            result.Add(new IterationResult { Ticks = SecondsToTicks(.5) });
            result.Add(new IterationResult { Ticks = SecondsToTicks(.5) });

            result.Throughput().Should().Be(2);
        }

        [Test]
        public void ResultExtensions_Throughput_ProfilerResult_OneSecond()
        {
            var res = new Result();
            res.Add(new IterationResult { Ticks = SecondsToTicks(1) });

            var res2 = new Result();
            res2.Add(new IterationResult { Ticks = SecondsToTicks(1) });

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
            res.Add(new IterationResult { Ticks = SecondsToTicks(2) });

            var res2 = new Result();
            res2.Add(new IterationResult { Ticks = SecondsToTicks(2) });

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
            res.Add(new IterationResult { Ticks = SecondsToTicks(.5) });

            var res2 = new Result();
            res2.Add(new IterationResult { Ticks = SecondsToTicks(.5) });

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

        private static long SecondsToTicks(double seconds)
        {
            // IterationResult.Ticks holds Stopwatch ticks in production, so tests must
            // build ticks from Stopwatch.Frequency to stay platform-independent.
            return (long)(seconds * Stopwatch.Frequency);
        }
    }
}
