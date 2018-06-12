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
    public class ProfilesSessionTaskHandlerExtensionTests
    {
        [Test]
        public void ProfilesSessionExtension_BeforeExecuteTask()
        {
            bool initialized = false;

            var session = ProfilerSession.StartSession()
                .BeforeExecute(() => initialized = true)
                .Task(() => Thread.Sleep(TimeSpan.FromSeconds(0.05)))
                .RunSession();

            Assert.That(initialized);
        }

        [Test]
        public void ProfilesSessionExtension_BeforeExecuteTask_EnsureBeforeTask()
        {
            string current = null;
            string last = null;

            var session = ProfilerSession.StartSession()
                .BeforeExecute(() => current = "before")
                .Task(() =>
                {
                    last = current;
                    current = "task";
                })
                .RunSession();

            Assert.That(current == "task");
            Assert.That(last == "before");
        }

        [Test]
        public void ProfilesSessionExtension_AfterExecuteTask()
        {
            bool initialized = false;

            var session = ProfilerSession.StartSession()
                .AfterExecute(() => initialized = true)
                .Task(() => Thread.Sleep(TimeSpan.FromSeconds(0.05)))
                .RunSession();

            Assert.That(initialized);
        }

        [Test]
        public void ProfilesSessionExtension_AfterExecuteTask_EnsureAfterTask()
        {
            string current = null;
            string last = null;

            var session = ProfilerSession.StartSession()
                .AfterExecute(() =>
                {
                    last = current;
                    current = "after";
                })
                .Task(() => current = "task")
                .RunSession();

            Assert.That(current == "after");
            Assert.That(last == "task");
        }

        [Test]
        public void ProfilesSessionExtension_AfterAfterExecuteTask_EnsureOrder()
        {
            string one = null;
            string two = null;
            string three = null;

            var session = ProfilerSession.StartSession()
                .BeforeExecute(()=> three = "before")
                .AfterExecute(() =>
                {
                    one = two;
                    two = three;
                    three = "after";
                })
                .Task(() =>
                {
                    two = three;
                    three = "task";
                })
                .RunSession();

            Assert.That(three == "after");
            Assert.That(two == "task");
            Assert.That(one == "before");
        }
    }
}
