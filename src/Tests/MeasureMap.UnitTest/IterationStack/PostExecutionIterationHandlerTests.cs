using MeasureMap.IterationStack;

namespace MeasureMap.UnitTest.IterationStack
{
    public class PostExecutionIterationHandlerTests
    {
        [Test]
        public void PostExecutionIterationHandler()
        {
            string two = null;
            string one = null;
            
            var handler = new PostExecutionIterationHandler(() =>
            {
                one = two;
                two = "task";
            });
            handler.SetNext(new Task(() => two = "before"));

            handler.Run(new ExecutionContext());

            two.Should().Be("task");
            one.Should().Be("before");
        }

        [Test]
        public void PostExecutionIterationHandler_WithContext()
        {
            string two = null;
            string one = null;

            var handler = new PostExecutionIterationHandler(c =>
            {
                one = two;
                two = "task";
            });
            handler.SetNext(new Task(() => two = "before"));

            handler.Run(new ExecutionContext());

            two.Should().Be("task");
            one.Should().Be("before");
        }
    }
}
