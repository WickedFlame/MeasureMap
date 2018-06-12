using NUnit.Framework;

namespace MeasureMap.UnitTest.Tasks
{
    [TestFixture]
    public class TaskOfTTests
    {
        [Test]
        public void TaskOfT_WithParameter()
        {
            var item = new Item { IsRun = false };
            var runner = new Task<Item>(i =>
            {
                i.IsRun = true;
                return i;
            }, item);

            runner.Run(new ExecutionContext());

            Assert.That(item.IsRun);
        }

        [Test]
        public void TaskOfT_WithParameter_Outpout()
        {
            var runner = new Task<Item>(i =>
            {
                i.IsRun = true;
                return i;
            }, new Item { IsRun = false });

            var item = runner.Run(new ExecutionContext()).Data as Item;

            Assert.That(item.IsRun);
        }
        
        public class Item
        {
            public bool IsRun { get; set; }
        }
    }
}
