using System;
using MeasureMap.Serialization;
using MeasureMap.Verification;

namespace MeasureMap.UnitTest.Verification
{
    public class BenchmarkVerifierTests
    {
        [Test]
        public void Verify_NoDifference_DoesNotThrow()
        {
            // baseline: res1 = 10ms, res2 = 12ms (distance +20%)
            var baseline = CreateModel(("res1", 10), ("res2", 12));
            // current: identical distance -> difference of 0%
            var result = CreateResult(("res1", 10), ("res2", 12));

            var verifier = new BemchmarkVerifier();

            var act = () => verifier.Verify(result, baseline);

            act.Should().NotThrow();
        }

        [Test]
        public void Verify_WithinThreshold_DoesNotThrow()
        {
            // baseline: res1 = 10ms, res2 = 12ms (distance +20%)
            var baseline = CreateModel(("res1", 10), ("res2", 12));
            // current: res1 = 10ms, res2 = 12.5ms (distance +25%) -> difference 5% < 10%
            var result = CreateResult(("res1", 10), ("res2", 12.5));

            var verifier = new BemchmarkVerifier();

            var act = () => verifier.Verify(result, baseline);

            act.Should().NotThrow();
        }

        [Test]
        public void Verify_ExceedsThreshold_Throws()
        {
            // baseline: res1 = 10ms, res2 = 12ms (distance +20%)
            var baseline = CreateModel(("res1", 10), ("res2", 12));
            // current: res1 = 10ms, res2 = 20ms (distance +100%) -> difference 80% > 10%
            var result = CreateResult(("res1", 10), ("res2", 20));

            var verifier = new BemchmarkVerifier();

            var act = () => verifier.Verify(result, baseline);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("*exceeded the allowed*");
        }

        [Test]
        public void Verify_ExceedsCustomThreshold_Throws()
        {
            // baseline distance +20%, current distance +25% -> difference 5%
            var baseline = CreateModel(("res1", 10), ("res2", 12));
            var result = CreateResult(("res1", 10), ("res2", 12.5));

            var verifier = new BemchmarkVerifier();

            var act = () => verifier.Verify(result, baseline, thresholdPercentage: 1);

            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Verify_WithinNoiseFloor_DoesNotThrow()
        {
            // baseline distance 0.5ms, current distance 0.6ms - both below the 1ms noise floor
            var baseline = CreateModel(("res1", 10), ("res2", 10.5));
            var result = CreateResult(("res1", 10), ("res2", 10.6));

            var verifier = new BemchmarkVerifier();

            var act = () => verifier.Verify(result, baseline);

            act.Should().NotThrow();
        }

        [Test]
        public void Verify_SingleBenchmark_DoesNotThrow()
        {
            var baseline = CreateModel(("res1", 10));
            var result = CreateResult(("res1", 20));

            var verifier = new BemchmarkVerifier();

            var act = () => verifier.Verify(result, baseline);

            act.Should().NotThrow();
        }

        [Test]
        public void Verify_MissingBenchmarkInCurrentRun_DoesNotThrow()
        {
            // baseline has three benchmarks but the current run misses one
            var baseline = CreateModel(("res1", 10), ("res2", 12), ("res3", 14));
            var result = CreateResult(("res1", 10), ("res2", 12));

            var verifier = new BemchmarkVerifier();

            var act = () => verifier.Verify(result, baseline);

            act.Should().NotThrow();
        }

        private static BenchmarkModel CreateModel(params (string Name, double Milliseconds)[] benchmarks)
        {
            var model = new BenchmarkModel();
            foreach (var benchmark in benchmarks)
            {
                model.Benchmarks.Add(new Benchmark
                {
                    Name = benchmark.Name,
                    AverageMilliseconds = benchmark.Milliseconds,
                    AverageTicks = ToTicks(benchmark.Milliseconds)
                });
            }

            return model;
        }

        private static IBenchmarkResult CreateResult(params (string Name, double Milliseconds)[] benchmarks)
        {
            var result = new BenchmarkResult(new ProfilerSettings { Iterations = 1 });
            foreach (var benchmark in benchmarks)
            {
                result.Add(benchmark.Name, CreateProfilerResult(benchmark.Milliseconds));
            }

            return result;
        }

        private static IProfilerResult CreateProfilerResult(double milliseconds)
        {
            var iteration = new Mock<IIterationResult>();
            iteration.SetupGet(i => i.Duration).Returns(TimeSpan.FromMilliseconds(milliseconds));

            var profilerResult = new Mock<IProfilerResult>();
            profilerResult.SetupGet(r => r.AverageTicks).Returns(ToTicks(milliseconds));
            profilerResult.SetupGet(r => r.AverageTime).Returns(TimeSpan.FromMilliseconds(milliseconds));
            profilerResult.SetupGet(r => r.TotalTime).Returns(TimeSpan.FromMilliseconds(milliseconds));
            profilerResult.SetupGet(r => r.Fastest).Returns(iteration.Object);
            profilerResult.SetupGet(r => r.Slowest).Returns(iteration.Object);

            return profilerResult.Object;
        }

        private static long ToTicks(double milliseconds)
        {
            // ToMilliseconds() divides ticks by 10000, so invert that here
            return (long)Math.Round(milliseconds * 10000);
        }
    }
}
