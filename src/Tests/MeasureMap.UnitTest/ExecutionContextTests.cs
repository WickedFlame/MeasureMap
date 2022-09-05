using NUnit.Framework;
using System.Linq;
using FluentAssertions;

namespace MeasureMap.UnitTest
{
    [TestFixture]
    public class ExecutionContextTests
    {
        [Test]
        public void ExecutionContext_Set()
        {
            var context = new ExecutionContext();
            context.Set("somevalue", 1);

            Assert.That(context.SessionData.Keys.First() == "somevalue");
            Assert.That((int)context.SessionData.Values.First() == 1);
        }

        [Test]
        public void ExecutionContext_Get()
        {
            var context = new ExecutionContext();
            context.SessionData.Add("somevalue", 3);

            var value = context.Get("somevalue");

            Assert.That((int)value == 3);
        }

        [Test]
        public void ExecutionContext_Get_TypeSafe()
        {
            var context = new ExecutionContext();
            context.SessionData.Add("somevalue", 4);

            var value = context.Get<int>("somevalue");

            Assert.That(value == 4);
        }

        [Test]
        public void ExecutionContext_Get_Default()
        {
            var context = new ExecutionContext();

            var value = context.Get<int>("somevalue");

            Assert.That(value == 0);
        }

        [Test]
        public void ExecutionContext_ThreadList_Default()
        {
            var context = new ExecutionContext();
            context.Threads.Should().NotBeNull();
        }
    }
}
