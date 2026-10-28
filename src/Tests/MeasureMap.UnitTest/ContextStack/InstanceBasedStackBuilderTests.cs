using MeasureMap.ContextStack;
using System;

namespace MeasureMap.UnitTest.ContextStack
{
    public class InstanceBasedStackBuilderTests
    {
        private IContextStackBuilder _builder;

        [SetUp]
        public void SetUp()
        {
            _builder = new InstanceBasedStackBuilder<StackBuilderInstance>(i => new Task(() => i.TestMethod()));
        }

        [Test]
        public void InstanceBasedStackBuilder_AddCustom()
        {
            var mw = new Mock<IContextMiddleware>();
            Func<int, ProfilerSettings, IContextMiddleware> middleware = (e, s) => { return mw.Object; };
            
            _builder.Add(middleware);
            _builder.Create(0, new ProfilerSettings());

            mw.Verify(x => x.SetNext(It.IsAny<IContextMiddleware>()), Times.Exactly(2));
        }

        [Test]
        public void InstanceBasedStackBuilder_Create()
        {
            var mw = _builder.Create(0, new ProfilerSettings());

            mw.Should().BeOfType<InstanceBasedStackRunner<StackBuilderInstance>>();
        }

        public class  StackBuilderInstance
        {
            public void TestMethod()
            {
                // This is a placeholder method for testing purposes.
            }
        }
    }
}
