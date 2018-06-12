﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasureMap.UnitTest
{
    [TestFixture]
    public class ThreadedTaskExecutorTests
    {
        [Test]
        public void ThreadedTaskExcutor_SingleThread()
        {
            var iterations = new List<int>();
            var cnt = 0;
            var executor = new MultyThreadExecutionHandler(1);
            var results = executor.Execute(new TaskHandler(new Task(() =>
            {
                cnt = cnt++;
                iterations.Add(cnt);
            })), 10);

            Assert.That(results.Count() == 1);
            Assert.That(iterations.Count == 10);
        }

        [Test]
        public void ThreadedTaskExcutor_MultipleThreads()
        {
            var iterations = new List<int>();
            var cnt = 0;
            var executor = new MultyThreadExecutionHandler(10);
            var results = executor.Execute(new TaskHandler(new Task(() =>
            {
                cnt = cnt++;
                iterations.Add(cnt);
            })), 10);

            Assert.That(results.Count() == 10);
            Assert.That(iterations.Count == 10*10);
        }
    }
}
