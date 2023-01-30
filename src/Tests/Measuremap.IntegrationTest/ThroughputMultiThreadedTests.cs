using BDTest.Attributes;
using BDTest.Test;
using FluentAssertions;
using MeasureMap;

namespace Measuremap.IntegrationTest
{
    [SingleThreaded]
    [Category("Integration")]
    [Story(AsA = "Tester", IWant = "To profile a task that takes 1 second", SoThat = "I can verify the throughput.")]
    public class ThroughputMultiThreadedTests : BDTestBase
    {
        [Test]
        [ScenarioText("Each iteration takes 1 second so the throughput is less than 1 per second")]
        public void TestThroughputOnePerSecond()
        {
            WithContext<BdContext>(context =>
                Given(() => InitSession(context, TimeSpan.FromSeconds(1)))
                    .When(() => RunSession(context))
                    .Then(() => EnsureThroughputIsOnePerSecond(context, .8, 1))
                    .BDTest()
            );
        }

        [Test]
        [ScenarioText("Each iteration takes .45 second so the throughput is about 2 per second")]
        public void TestThroughputTwoPerSecond()
        {
            WithContext<BdContext>(context =>
                Given(() => InitSession(context, TimeSpan.FromSeconds(.5)))
                    .When(() => RunSession(context))
                    .Then(() => EnsureThroughputIsOnePerSecond(context, 1.7, 2))
                    .BDTest()
            );
        }

        [Test]
        [ScenarioText("Each iteration takes 2 second so the throughput is less than 0.5 per second")]
        public void TestThroughputHalfPerSecond()
        {
            WithContext<BdContext>(context =>
                Given(() => InitSession(context, TimeSpan.FromSeconds(2)))
                    .When(() => RunSession(context))
                    .Then(() => EnsureThroughputIsOnePerSecond(context, .4, .5))
                    .BDTest()
            );
        }

        private void InitSession(BdContext context, TimeSpan delay)
        {
            context.Session = ProfilerSession.StartSession()
                .Task(() => System.Threading.Tasks.Task.Delay(delay).Wait())
                .SetIterations(5)
                .SetThreads(5);
        }

        private void RunSession(BdContext context)
        {
            context.Result = context.Session.RunSession();
            context.Result.Trace(o => o.TraceDetail = MeasureMap.Tracers.TraceDetail.FullDetail);
        }

        private void EnsureThroughputIsOnePerSecond(BdContext context, double min, double max)
        {
            context.Result.Throughput().Should().BeInRange(min, max);
        }
    }
}