using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MeasureMap.UnitTest
{
    [TestFixture]
    public class MemoryProfileSessionTests
    {
        [Test]
        public void MemoryProfileSession_StartSessionTest()
        {
            var session = MemoryProfileSession.StartSession();

            Assert.IsNotNull(session);
        }

        [Test]
        public void MemoryProfileSession_AddTask()
        {
            var session = MemoryProfileSession.StartSession()
                .Task(() =>
                {
                    // allocate some memory
                });

            // TODO: is it neccesary to run the session just to check if a task is set???
            session.RunSession();

            Assert.IsNotNull(session);
        }

        [Test]
        public void MemoryProfileSession_RunSessionOnce()
        {
            int count = 0;
            var result = MemoryProfileSession.StartSession()
                .Task(() => count++)
                .RunSession();

            // the task is rune once more to be able to initialize properly
            Assert.AreEqual(result.Iterations.Count() + 1, count);
        }

        [Test]
        public void MemoryProfileSession_RunSessionMultipleTimes()
        {
            int count = 0;
            var result = MemoryProfileSession.StartSession()
                .Task(() => count++)
                .SetIterations(20)
                .RunSession();

            // the task is rune once more to be able to initialize properly
            Assert.AreEqual(result.Iterations.Count() + 1, count);
        }

        [Test]
        public void MemoryProfileSession_AllocateMemory()
        {
            var result = MemoryProfileSession.StartSession()
                .Task(() =>
                {
                    var list = new List<byte[]>();
                    for (int i = 0; i < 10000; i++)
                    {
                        list.Add(new byte[1024]); // Change the size here.
                    }
                })
                .SetIterations(1000)
                .RunSession();

            //Thread.Sleep(TimeSpan.FromSeconds(10));

            Assert.IsTrue(result.Increase < 50000L, result.Increase.ToString());
        }
    }
}
