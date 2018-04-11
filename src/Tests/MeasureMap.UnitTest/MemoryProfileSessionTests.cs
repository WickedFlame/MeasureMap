﻿using NUnit.Framework;
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
            var session = ProfilerSession.StartSession();

            Assert.IsNotNull(session);
        }

        [Test]
        public void MemoryProfileSession_AddTask()
        {
            var session = ProfilerSession.StartSession()
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
            var result = ProfilerSession.StartSession()
                .Task(() => count++)
                .RunSingleSession();

            // the task is rune once more to be able to initialize properly
            Assert.AreEqual(result.Iterations.Count() + 1, count);
        }

        [Test]
        public void MemoryProfileSession_RunSessionMultipleTimes()
        {
            int count = 0;
            var result = ProfilerSession.StartSession()
                .Task(() => count++)
                .SetIterations(20)
                .RunSingleSession();

            // the task is rune once more to be able to initialize properly
            Assert.AreEqual(result.Iterations.Count() + 1, count);
        }

        [Test]
        public void MemoryProfileSession_AllocateMemory()
        {
            var result = ProfilerSession.StartSession()
                .Task(() =>
                {
                    // create some objects to allocate memory
                    var list = new List<byte[]>();
                    for (int i = 0; i < 10000; i++)
                    {
                        list.Add(new byte[1024]);
                    }
                })
                .SetIterations(1000)
                .RunSingleSession();

            Thread.Sleep(TimeSpan.FromSeconds(5));

            Assert.IsTrue(result.Increase < 200000L && result.Increase > 30000L, result.Increase.ToString());
        }
    }
}
