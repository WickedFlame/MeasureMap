using System.Linq;

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
        public void ExecutionContext_Set_Key_CaseInsensitive()
        {
            var context = new ExecutionContext();
            context.Set("SomeKey", 1);

            context.SessionData.Keys.First().Should().Be("somekey");
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
        public void ExecutionContext_Get_Key_CaseInsensitive()
        {
            var context = new ExecutionContext();
            context.SessionData.Add("somekey", 4);

            context.Get("SomeKey").Should().Be(4);
        }

        [Test]
        public void ExecutionContext_Get_Generic_Key_CaseInsensitive()
        {
            var context = new ExecutionContext();
            context.SessionData.Add("somekey", 4);

           context.Get<int>("SomeKey").Should().Be(4);
        }
        
        [Test]
        public void ExecutionContext_Get_NullValue()
        {
            var context = new ExecutionContext();
            context.SessionData.Add("somekey", null);

            context.Get<int>("SomeKey").Should().Be(0);
        }
        
        [Test]
        public void ExecutionContext_Get_InvalidType()
        {
            var context = new ExecutionContext();
            context.SessionData.Add("somekey", "test");

            context.Get<int>("SomeKey").Should().Be(0);
        }
    }
}
