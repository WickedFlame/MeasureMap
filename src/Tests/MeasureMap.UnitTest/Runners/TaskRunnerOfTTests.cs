using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasureMap.UnitTest.Runners
{
    [TestFixture]
    public class TaskRunnerOfTTests
    {
        [Test]
        public void TaskRunnerOfT_WithParameter()
        {
            var item = new Item { IsRun = false };
            var runner = new TaskRunner<Item>(i =>
            {
                i.IsRun = true;
                return i;
            }, item);

            runner.Run(0);

            Assert.That(item.IsRun);
        }

        [Test]
        public void TaskRunnerOfT_WithParameter_Outpout()
        {
            var runner = new TaskRunner<Item>(i =>
            {
                i.IsRun = true;
                return i;
            }, new Item { IsRun = false });

            var item = runner.Run(0) as Item;

            Assert.That(item.IsRun);
        }
        
        public class Item
        {
            public bool IsRun { get; set; }
        }
    }
}
