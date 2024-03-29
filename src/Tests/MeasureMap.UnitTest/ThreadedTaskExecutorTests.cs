﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MeasureMap.Diagnostics;

namespace MeasureMap.UnitTest
{
    [TestFixture]
    [SingleThreaded]
    public class ThreadedTaskExecutorTests
    {
        private static object _lockObject = new object();

        [Test]
        public void ThreadedTaskExcutor_SingleThread()
        {
            var iterations = new List<int>();
            var cnt = 0;
            var executor = new MultyThreadSessionHandler(1);
            var results = executor.Execute(new Task(() =>
            {
                lock (_lockObject)
                {
                    cnt++;
                    iterations.Add(cnt);
                }
            }), new ProfilerSettings { Iterations = 10 });

            results.Count().Should().Be(1);
            iterations.Count.Should().Be(10);
        }

        [Test]
        public void ThreadedTaskExcutor_MultipleThreads()
        {
            var iterations = new List<int>();
            var cnt = 0;
            var executor = new MultyThreadSessionHandler(10);
            var results = executor.Execute(new Task(() =>
            {
                lock (_lockObject)
                {
                    cnt++;
                    iterations.Add(cnt);
                }
            }), new ProfilerSettings { Iterations = 10 });

            executor.DisposeThreads();
            
            results.Count().Should().Be(10);
            iterations.Count.Should().Be(10*10);
        }
    }
}
