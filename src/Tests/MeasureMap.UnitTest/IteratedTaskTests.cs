
namespace MeasureMap.UnitTest
{
    [TestFixture]
    public class IteratedTaskTests
    {
        [Test]
        public void IteratedTask_Int()
        {
            var output = 0;
            ProfilerSession.StartSession()
                .Task(c =>
                {
                    // do something
                    output = c.Get<int>(ContextKeys.Iteration); 
                })
                .SetIterations(20)
                .RunSession();

            output.Should().Be(20);
        }
    }
}
