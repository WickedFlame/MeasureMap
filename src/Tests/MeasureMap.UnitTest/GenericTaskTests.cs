using System;
using NUnit.Framework;
using System.Linq;

namespace MeasureMap.UnitTest
{
    [TestFixture]
    public class GenericTaskTests
    {
        [Test]
        public void GenericTask_Int()
        {
            var output = 0;
            ProfilerSession.StartSession()
                .Task<int>(i =>
                {
                    // do something
                    output = i;
                    System.Threading.Thread.Sleep(50);
                    return ++i;
                })
                .SetIterations(20)
                .RunSession();

            Assert.AreEqual(20, output);
        }

        [Test]
        public void GenericTask_Object()
        {
            var output = 0;
            ProfilerSession.StartSession()
                .Task<Item>(i =>
                {
                    // do something
                    output = i.Count;
                    i.Count = ++i.Count;

                    return i;
                })
                .SetIterations(20)
                .RunSession();

            Assert.AreEqual(20, output);
        }

        [Test]
        public void GenericTask_NewObject()
        {
            var output = 0;
            var session = ProfilerSession.StartSession()
                .Task<Item>(i =>
                {
                    // do something
                    output = i.Count;

                    var item = new Item
                    {
                        Count = ++i.Count
                    };

                    return item;
                })
                .SetIterations(20)
                .RunSession();

            session.Trace();

            Assert.AreEqual(20, output);
        }

        [Test]
        public void GenericTask_NoEmptyCtor()
        {
            Assert.Throws<InvalidOperationException>(() => ProfilerSession.StartSession()
                .Task<ItemUnemtyConstructor>(i => i));
        }

        [Test]
        public void GenericTask_SetParameter()
        {
            var output = 0;
            var param = new Item { Count = 3 };
            ProfilerSession.StartSession()
                .Task<Item>(i =>
                {
                    // do something
                    output = i.Count;

                    i.Count++;
                    return i;
                }, param)
                .SetIterations(20)
                .RunSession();

            Assert.AreEqual(24, param.Count);
        }

        [Test]
        public void GenericTask_SetParameter_AnonymousObject()
        {
            var output = 0;
            var param = new { Count = 3 };
            ProfilerSession.StartSession()
                .Task(i =>
                {
                    // do something
                    var tmp = new { Count = i.Count + 1 };
                    
                    output = tmp.Count;

                    return tmp;
                }, param)
                .SetIterations(20)
                .RunSession();

            Assert.AreEqual(24, output);
        }

        public class Item
        {
            public int Count { get; set; }
        }

        public class ItemUnemtyConstructor
        {
            public ItemUnemtyConstructor(int id)
            {
                // this should throw an exception
            }

            public int Count { get; set; }
        }
    }
}
