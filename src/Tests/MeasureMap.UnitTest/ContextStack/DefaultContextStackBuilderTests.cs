using MeasureMap.ContextStack;
using System;

namespace MeasureMap.UnitTest.ContextStack
{
    public class DefaultContextStackBuilderTests
    {
        [Test]
        public void DefaultContextStackBuilder_AddCustom()
        {
            var mw = new Mock<IContextMiddleware>();
            Func<int, ProfilerSettings, IContextMiddleware> middleware = (e, s) => { return mw.Object; };

            var builder = new DefaultContextStackBuilder();
            builder.Add(middleware);

            builder.Create(0, new ProfilerSettings());

            mw.Verify(x => x.SetNext(It.IsAny<IContextMiddleware>()), Times.Exactly(2));
        }

        [Test]
        public void DefaultContextStackBuilder_AddCustom_Run()
        {
            var mw = new Mock<IContextMiddleware>();

            var builder = new DefaultContextStackBuilder();
            builder.Add((e, s) => { return mw.Object; });

            var stack = builder.Create(0, new ProfilerSettings());
            stack.Run(Mock.Of<ITask>(), new ExecutionContext());

            mw.Verify(x => x.Run(It.IsAny<ITask>(), It.IsAny<IExecutionContext>()));
        }
    }
}
