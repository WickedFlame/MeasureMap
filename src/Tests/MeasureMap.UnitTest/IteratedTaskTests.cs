using System;
using NUnit.Framework;
using System.Linq;

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

            Assert.AreEqual(output, 19);
        }
    }
}
