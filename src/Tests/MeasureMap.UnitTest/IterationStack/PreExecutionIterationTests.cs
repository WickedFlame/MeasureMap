using MeasureMap.IterationStack;

namespace MeasureMap.UnitTest.IterationStack
{
    [TestFixture]
    public class PreExecutionIterationTests
    {
        [Test]
        public void BeforeExecutionTaskHandler()
        {
            string two = null;
            string one = null;
            
            var task = new Task(() =>
            {
                one = two;
                two = "task";
            });

            var handler = new PreExecutionIterationHandler(() => two = "before");
            handler.SetNext(task);

            handler.Run(new ExecutionContext());

            two.Should().Be("task");
            one.Should().Be("before");
        }

        [Test]
        public void BeforeExecutionTaskHandler_ContextParameter()
        {
            string two = null;
            string one = null;

            var task = new Task(() =>
            {
                one = two;
                two = "task";
            });

            var handler = new PreExecutionIterationHandler(c => two = "before");
            handler.SetNext(task);

            handler.Run(new ExecutionContext());

            two.Should().Be("task");
            one.Should().Be("before");
        }
    }
}
