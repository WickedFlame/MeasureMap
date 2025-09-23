using MeasureMap.IterationStack;

namespace MeasureMap.UnitTest.IterationStack
{
    [TestFixture]
    public class MemoryCollectionIterationHandlerTests
    {
        private IIterationMiddleware _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new MemoryCollectionIterationHandler();
        }

        [Test]
        public void MemoryCollectionIterationHandler_Initial()
        {
            var result = _handler.Run(new ExecutionContext());

            result.InitialSize.Should().BeGreaterThan(0);
        }

        [Test]
        public void MemoryCollectionIterationHandler_AfterExecution()
        {
            var result = _handler.Run(new ExecutionContext());

            result.AfterExecution.Should().BeGreaterThan(0);
        }
    }
}
