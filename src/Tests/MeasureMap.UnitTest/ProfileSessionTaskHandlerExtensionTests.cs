namespace MeasureMap.UnitTest
{
    [TestFixture]
    public class ProfilesSessionTaskHandlerExtensionTests
    {
        [Test]
        public void ProfilesSessionExtension_PreExecuteTask()
        {
            bool initialized = false;

            ProfilerSession.StartSession()
                .PreExecute(() => initialized = true)
                .Task(() => { })
                .RunSession();

            initialized.Should().BeTrue();
        }

        [Test]
        public void ProfilesSessionExtension_PreExecuteTask_EnsureBeforeTask()
        {
            string current = null;
            string last = null;

            ProfilerSession.StartSession()
                .PreExecute(() => current = "before")
                .Task(() =>
                {
                    last = current;
                    current = "task";
                })
                .RunSession();

            current.Should().Be("task");
            last.Should().Be("before");
        }

        [Test]
        public void ProfilesSessionExtension_PostExecuteTask()
        {
            var initialized = false;

            ProfilerSession.StartSession()
                .PostExecute(() => initialized = true)
                .Task(() => { })
                .RunSession();

            initialized.Should().BeTrue();
        }

        [Test]
        public void ProfilesSessionExtension_PostExecuteTask_EnsureAfterTask()
        {
            string current = null;
            string last = null;

            ProfilerSession.StartSession()
                .PostExecute(() =>
                {
                    last = current;
                    current = "after";
                })
                .Task(() => current = "task")
                .RunSession();

            current.Should().Be("after");
            last.Should().Be("task");
        }

        [Test]
        public void ProfilesSessionExtension_AfterPostExecuteTask_EnsureOrder()
        {
            string one = null;
            string two = null;
            string three = null;

            ProfilerSession.StartSession()
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

            three.Should().Be("after");
            two.Should().Be("task");
            one.Should().Be("before");
        }

        [Test]
        public void ProfilesSessionExtension_AfterPostExecuteTask_EnsureContext()
        {
            ProfilerSession.StartSession()
                .PostExecute(c => c.Clear())
                .PreExecute(c =>
                {
                    c.Set("pre", "before");
                    c.Get<string>("post").Should().BeNull();
                    c.Get<string>("task").Should().BeNull();
                })
                .Task(c =>
                {
                    c.Set("task", "Task");
                    c.Get<string>("pre").Should().Be("before");
                    c.Get<string>("post").Should().BeNull();
                })
                .PostExecute(c =>
                {
                    c.Set("post", "after");
                    c.Get<string>("pre").Should().Be("before");
                    c.Get<string>("task").Should().Be("Task");
                })
                .SetIterations(10)
                .RunSession();
        }

        [Test]
        public void ProfilesSessionExtension_AfterPostExecuteTask_MultiplePrePost()
        {
            ProfilerSession.StartSession()
                // Iteration, processid, threadid and ThreadNumber
                .PreExecute(c => c.SessionData.Count.Should().Be(3))
                .PreExecute(c =>
                {
                    c.Set("pre", "before");
                    c.Get<string>("post").Should().BeNull();
                    c.Get<string>("task").Should().BeNull();
                })
                .PostExecute(c =>
                {
                    c.SessionData.Count.Should().BeGreaterThan(0);
                    c.Clear();
                })
                .PostExecute(c =>
                {
                    c.Set("post", "after");
                    c.Get<string>("pre").Should().Be("before");
                    c.Get<string>("task").Should().Be("Task");
                })
                .Task(c =>
                {
                    c.Set("task", "Task");
                    c.Get<string>("pre").Should().Be("before");
                    c.Get<string>("post").Should().BeNull();
                })
                .SetIterations(10)
                .RunSession();
        }
    }
}
