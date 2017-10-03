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
                .Task(i =>
                {
                    // do something
                    output = i;
                })
                .SetIterations(20)
                .RunSession();

            Assert.AreEqual(output, 19);
        }
    }
}
