using MeasureMap.SessionStack;

namespace MeasureMap.UnitTest.SessionStack
{
    public class PreExecutionSessionHandlerTests
    {
        [Test]
        public void PreExecutionSessionHandler_Execute()
        {
            var value = 0;
            var handler = new PreExecutionSessionHandler(() => value = 1);
            handler.Execute(Mock.Of<ITask>(), new ProfilerSettings());

            value.Should().Be(1);
        }

        [Test]
        public void PreExecutionSessionHandler_Execute_Order()
        {
            var value = 0;
            var next = new Mock<ISessionMiddleware>();
            next.Setup(x => x.Execute(It.IsAny<ITask>(), It.IsAny<ProfilerSettings>())).Callback<ITask, ProfilerSettings>((t, s) => value = value + 1);

            var handler = new PreExecutionSessionHandler(() => value = 1);
            handler.SetNext(next.Object);
            handler.Execute(Mock.Of<ITask>(), new ProfilerSettings());

            value.Should().Be(2);
        }
    }
}
