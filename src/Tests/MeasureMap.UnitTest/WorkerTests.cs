using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MeasureMap.UnitTest
{
    [TestFixture]
    public class WorkerTests
    {
        [Test]
        public void MeasureMap_Worker()
        {
            int i = 0;
            var task = new Task(() => i++);

            var worker = new Worker();
            var result = worker.Run(task, 10);

            Assert.That(result.Iterations.Count() == 10);
            Assert.That(i == 10);
        }

        [Test]
        public void MeasureMap_Worker_ElapsedTime()
        {
            int i = 0;
            var task = new Task(() => i++);
            var handler = new ElapsedTimeTaskHandler();
            handler.SetNext(task);

            var worker = new Worker();
            var result = worker.Run(handler, 10);

            Assert.That(result.AverageTicks > 0);
            Assert.That(result.AverageTime.Ticks > 0);
            Assert.That(result.TotalTime.Ticks > 0);

            Assert.That(result.TotalTime > result.AverageTime);
        }

        [Test]
        public void MeasureMap_Worker_Memory()
        {
            int i = 0;
            var task = new Task(() => i++);

            var worker = new Worker();
            var result = worker.Run(task, 10);

            Assert.That(result.InitialSize > 0, () => "InitialSize is smaller than 0");
            Assert.That(result.EndSize > 0, () => "EndSize is smaller than 0");
            Assert.That(result.Increase != 0, () => "Increase is 0");
        }
    }
}
