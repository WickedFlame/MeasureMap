using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MeasureMap.UnitTest.SessionHandlers
{
    [TestFixture]
    public class WarmupSessionHandlerTests
    {
        [Test]
        public void WarmupSessionHandler_Execute()
        {
            var task = new Task(() => Thread.Sleep(TimeSpan.FromSeconds(0.5)));
            var handler = new WarmupSessionHandler();

            var result = handler.Execute(task, new ProfilerSettings { Iterations = 10 });

            Assert.That(result.Warmup().Ticks > 0);
        }

        [Test]
        public void WarmupSessionHandler_Execute_EnsureBaseIsCalled()
        {
            var next = new Mock<ISessionHandler>();
            next.Setup(exp => exp.Execute(It.IsAny<ITask>(), It.IsAny<ProfilerSettings>())).Returns(() => new ProfilerResult());

            var settings = new ProfilerSettings { Iterations = 10 };

            var task = new Task(() => Thread.Sleep(TimeSpan.FromSeconds(0.5)));
            var handler = new WarmupSessionHandler();
            handler.SetNext(next.Object);

            var result = handler.Execute(task, settings);

            next.Verify(exp => exp.Execute(task, settings), Times.Once);
        }

        [Test]
        public void WarmupSessionHandler_Execute_EnsureTaskIsRun()
        {
            var settings = new ProfilerSettings { Iterations = 10 };

            var task = new Mock<ITask>();
            var handler = new WarmupSessionHandler();

            var result = handler.Execute(task.Object, settings);

            task.Verify(exp => exp.Run(It.IsAny<IExecutionContext>()), Times.Once);
        }

        [Test]
        public void WarmupSessionHandler_Enabled()
        {
            var count = 0;

            ProfilerSession.StartSession()
                .Task(() => count += 1)
                .SetSettings(new ProfilerSettings { Iterations = 10 })
                .RunSession();

            Assert.That(count == 11);
        }

        [Test]
        public void WarmupSessionHandler_Disable()
        {
            var count = 0;

            ProfilerSession.StartSession()
                .Task(() => count += 1)
                .SetSettings(new ProfilerSettings { Iterations = 10, RunWarmup = false })
                .RunSession();

            Assert.That(count == 10);
        }

        [Test]
        public void WarmupSessionHandler_IsWarmup()
        {
            var isWarmup = false;

            var settings = new ProfilerSettings { Iterations = 1 };
            var task = new ContextTask(ex => isWarmup = ex.Settings.IsWarmup);
            var handler = new WarmupSessionHandler();

            handler.Execute(task, settings);

            isWarmup.Should().BeTrue();
        }
    }
}
