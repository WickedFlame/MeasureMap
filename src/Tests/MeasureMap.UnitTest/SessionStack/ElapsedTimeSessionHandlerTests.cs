using MeasureMap.SessionStack;
using System;

namespace MeasureMap.UnitTest.SessionStack
{
    [TestFixture]
    public class ElapsedTimeSessionHandlerTests
    {
        private ISessionMiddleware _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new ElapsedTimeSessionHandler();
        }

        [Test]
        public void ElapsedTimeSessionHandler_Execute()
        {
            var task = new Task(() => { });

            var result = _handler.Execute(task, new ProfilerSettings { Iterations = 10 });

            result.Elapsed().Ticks.Should().BeGreaterThan(0);
        }

        [Test]
        public void ElapsedTimeSessionHandler_Execute_Elapsed()
        {
            var task = new Task(() => { });

            var result = _handler.Execute(task, new ProfilerSettings { Iterations = 10 });

            ((TimeSpan)result.ResultValues[ResultValueType.Elapsed]).Ticks.Should().BeGreaterThan(0);
        }

        [Test]
        public void ElapsedTimeSessionHandler_Execute_EnsureBaseIsCalled()
        {
            var next = new Mock<ISessionMiddleware>();
            next.Setup(exp => exp.Execute(It.IsAny<ITask>(), It.IsAny<ProfilerSettings>())).Returns(() => new ProfilerResult());

            var settings = new ProfilerSettings {Iterations = 10};

            var task = new Task(() => { });
            _handler.SetNext(next.Object);

            _handler.Execute(task, settings);

            next.Verify(exp => exp.Execute(task, settings), Times.Once);
        }
    }
}
