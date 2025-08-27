using MeasureMap.IterationStack;
using System;
using System.Diagnostics;

namespace MeasureMap.UnitTest.IterationStack
{
    [NonParallelizable]
    public class DelayIterationHandlerTests
    {
        [Test]
        public void DelayIterationHandler_Run()
        {
            var time = TimeSpan.FromSeconds(10);
            var handler = new DelayIterationHandler(time);

            var ctx = new ExecutionContext();

            var sw = Stopwatch.StartNew();
            handler.Run(ctx);
            sw.Stop();

            sw.Elapsed.Should().BeGreaterThan(time);
        }
    }
}
