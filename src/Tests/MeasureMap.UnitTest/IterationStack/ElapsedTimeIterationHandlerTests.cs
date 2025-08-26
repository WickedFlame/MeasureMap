using MeasureMap.IterationStack;

namespace MeasureMap.UnitTest.IterationStack
{
    public class ElapsedTimeIterationHandlerTests
    {
        private IIterationMiddleware _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new ElapsedTimeIterationHandler();
        }

        [Test]
        public void ElapsedTimeIterationHandler_Run_Ticks()
        {
            var result = _handler.Run(new ExecutionContext());

            result.Ticks.Should().BeGreaterThan(0);
        }

        [Test]
        public void ElapsedTimeIterationHandler_Run_Duration()
        {
            var result = _handler.Run(new ExecutionContext());

            result.Duration.Ticks.Should().BeGreaterThan(0);
            Assert.That(result.Duration.Ticks > 0);
        }
    }
}
