using MeasureMap.Threading;
using System;

namespace MeasureMap.UnitTest.Threading
{
    public class WorkerThreadTests
    {
        [Test]
        public void WorkerThread_ctor()
        {
            new WorkerThread(1, _ => new Result()).Should().NotBeNull();
        }

        [Test]
        public void WorkerThread_IsAlive()
        {
            var thread = new WorkerThread(1, _ => new Result());
            thread.IsAlive.Should().BeTrue();
        }

        [Test]
        public void WorkerThread_IsAlive_AfterExecute()
        {
            var thread = new WorkerThread(1, _ => new Result());
            thread.Start();
            thread.WaitHandle.WaitOne();
            thread.IsAlive.Should().BeFalse();
        }

        [Test]
        public void WorkerThread_WaitHandle()
        {
            var thread = new WorkerThread(1, _ => new Result());
            thread.WaitHandle.WaitOne(0).Should().BeFalse();
        }

        [Test]
        public void WorkerThread_WaitHandle_AfterExecute()
        {
            var thread = new WorkerThread(1, _ => new Result());
            thread.Start();
            thread.WaitHandle.WaitOne();

            thread.WaitHandle.WaitOne(0).Should().BeTrue();
        }

        [Test]
        public void WorkerThread_Id()
        {
            var thread = new WorkerThread(1, _ => new Result());
            thread.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void WorkerThread_Dispose()
        {
            var thread = new WorkerThread(1, _ => new Result());
            Action action = () => thread.Dispose();

            action.Should().NotThrow();
        }

        [Test]
        public void WorkerThread_Dispose_ThreadEnded()
        {
            var thread = new WorkerThread(1, _ => new Result());
            Action action = () => thread.Dispose();
            thread.Start();
            action.Should().NotThrow();
        }

        [Test]
        public void WorkerThread_Dispose_Started()
        {
            var thread = new WorkerThread(1, _ =>
            {
                System.Threading.Tasks.Task.Delay(TimeSpan.FromMinutes(1)).Wait();
                return new Result();
            });
            Action action = () => thread.Dispose();
            thread.Start();
            action.Should().NotThrow();
        }

        [Test]
        public void WorkerThread_Start_MultipleTimes()
        {
            var thread = new WorkerThread(1, _ =>
            {
                System.Threading.Tasks.Task.Delay(TimeSpan.FromMinutes(1)).Wait();
                return new Result();
            });
            thread.Start();

            Action action = () => thread.Start();
            action.Should().NotThrow();

            thread.Dispose();
        }

        [Test]
        public void WorkerThread_Start_EndedThread()
        {
            var thread = new WorkerThread(1, _ =>
            {
                return new Result();
            });
            thread.Start();

            while (thread.IsAlive)
            {
                thread.WaitHandle.WaitOne();
            }

            thread.IsAlive.Should().BeFalse();

            Action action = () => thread.Start();
            action.Should().NotThrow();
        }

        [Test]
        public void WorkerThread_Result()
        {
            var result = new Result();
            var thread = new WorkerThread(1, _ => result);
            thread.Start();
            thread.WaitHandle.WaitOne();

            thread.Result.Should().BeSameAs(result);
        }

        [Test]
        public void WorkerThread_ThreadNumberInResult()
        {
            var thread = new WorkerThread(1, nbr => new Result { ThreadNumber = nbr });
            thread.Start();
            thread.WaitHandle.WaitOne();

            thread.Result.ThreadNumber.Should().Be(1);
        }

        [Test]
        public void WorkerThread_ThreadNumberIsPassed()
        {
            var action = new Mock<Func<int, IResult>>();
            var thread = new WorkerThread(1, action.Object);
            thread.Start();
            thread.WaitHandle.WaitOne();

            action.Verify(x => x.Invoke(1), Times.Once);
        }
    }
}
