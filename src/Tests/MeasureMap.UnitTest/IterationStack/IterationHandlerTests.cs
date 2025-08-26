using MeasureMap.IterationStack;

namespace MeasureMap.UnitTest.IterationStack
{
    public class IterationHandlerTests
    {
        private IIterationMiddleware _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new TestHandler();
        }

        [Test]
        public void IterationHandler_SetNext()
        {
            var next = new Mock<ITask>();
            _handler.SetNext(next.Object);

            _handler.Run(new ExecutionContext());

            next.Verify(x => x.Run(It.IsAny<IExecutionContext>()));
        }

        [Test]
        public void IterationHandler_Run_NoNext()
        {
            _handler.Run(new ExecutionContext())
                .Should().NotBeNull();
        }

        private class TestHandler : IterationHandler
        {
        }
    }
}
