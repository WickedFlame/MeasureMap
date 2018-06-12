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
        public void ProfilesSessionExtension_PreExecuteTask()
        {
            bool initialized = false;

            var session = ProfilerSession.StartSession()
                .PreExecute(() => initialized = true)
                .Task(() => Thread.Sleep(TimeSpan.FromSeconds(0.05)))
                .RunSession();

            Assert.That(initialized);
        }

        [Test]
        public void ProfilesSessionExtension_PreExecuteTask_EnsureBeforeTask()
        {
            string current = null;
            string last = null;

            var session = ProfilerSession.StartSession()
                .PreExecute(() => current = "before")
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
        public void ProfilesSessionExtension_PostExecuteTask()
        {
            bool initialized = false;

            var session = ProfilerSession.StartSession()
                .PostExecute(() => initialized = true)
                .Task(() => Thread.Sleep(TimeSpan.FromSeconds(0.05)))
                .RunSession();

            Assert.That(initialized);
        }

        [Test]
        public void ProfilesSessionExtension_PostExecuteTask_EnsureAfterTask()
        {
            string current = null;
            string last = null;

            var session = ProfilerSession.StartSession()
                .PostExecute(() =>
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
        public void ProfilesSessionExtension_AfterPostExecuteTask_EnsureOrder()
        {
            string one = null;
            string two = null;
            string three = null;

            var session = ProfilerSession.StartSession()
                .PreExecute(() => three = "before")
                .PostExecute(() =>
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

        [Test]
        public void ProfilesSessionExtension_AfterPostExecuteTask_EnsureContext()
        {
            var session = ProfilerSession.StartSession()
                .PostExecute(c => c.Clear())
                .PreExecute(c =>
                {
                    c.Set("pre", "before");
                    Assert.That(c.Get<string>("post") == null);
                    Assert.That(c.Get<string>("task") == null);
                })
                .Task(c =>
                {
                    c.Set("task", "Task");
                    Assert.That(c.Get<string>("pre") == "before");
                    Assert.That(c.Get<string>("post") == null);
                })
                .PostExecute(c =>
                {
                    c.Set("post", "after");
                    Assert.That(c.Get<string>("pre") == "before");
                    Assert.That(c.Get<string>("task") == "Task");
                })
                .SetIterations(10)
                .RunSession();
        }

        [Test]
        public void ProfilesSessionExtension_AfterPostExecuteTask_MultiplePrePost()
        {
            var session = ProfilerSession.StartSession()
                .PreExecute(c => Assert.That(c.SessionData.Count <= 1))
                .PreExecute(c =>
                {
                    c.Set("pre", "before");
                    Assert.That(c.Get<string>("post") == null);
                    Assert.That(c.Get<string>("task") == null);
                })
                .PostExecute(c =>
                {
                    Assert.That(c.SessionData.Count > 0);
                    c.Clear();
                })
                .PostExecute(c =>
                {
                    c.Set("post", "after");
                    Assert.That(c.Get<string>("pre") == "before");
                    Assert.That(c.Get<string>("task") == "Task");
                })
                .Task(c =>
                {
                    c.Set("task", "Task");
                    Assert.That(c.Get<string>("pre") == "before");
                    Assert.That(c.Get<string>("post") == null);
                })
                .SetIterations(10)
                .RunSession();
        }
    }
}
