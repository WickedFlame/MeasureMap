using FluentAssertions;
using MeasureMap.Threading;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;

namespace MeasureMap.UnitTest.Threading
{
    public class WorkerTaskTests
    {
        [Test]
        public void WorkerTask_ctor()
        {
            new WorkerTask(1, _ => new Result()).Should().NotBeNull();
        }

        [Test]
        public void WorkerTask_IsAlive_Initial()
        {
            var thread = new WorkerTask(1, _ => new Result());
            thread.IsAlive.Should().BeFalse();
        }

        [Test]
        public void WorkerTask_IsAlive()
        {
            var thread = new WorkerTask(1, _ => new Result());
            thread.Start();
            thread.IsAlive.Should().BeTrue();
        }

        [Test]
        public void WorkerTask_IsAlive_AfterExecute()
        {
            var thread = new WorkerTask(1, _ => new Result());
            thread.Start();
            thread.WaitHandle.WaitOne();

            System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(10)).Wait();

            thread.IsAlive.Should().BeFalse();
        }

        [Test]
        public void WorkerTask_WaitHandle()
        {
            var thread = new WorkerTask(1, _ => new Result());
            thread.WaitHandle.WaitOne(0).Should().BeFalse();
        }

        [Test]
        public void WorkerTask_WaitHandle_AfterExecute()
        {
            var thread = new WorkerTask(1, _ => new Result());
            thread.Start();
            thread.WaitHandle.WaitOne();

            thread.WaitHandle.WaitOne(0).Should().BeTrue();
        }

        [Test]
        public void WorkerTask_Id_Initial()
        {
            var thread = new WorkerTask(1, _ => new Result());
            thread.Id.Should().Be(0);
        }

        [Test]
        public void WorkerTask_Id()
        {
            var thread = new WorkerTask(1, _ => new Result());
            thread.Start();

            thread.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void WorkerTask_Dispose()
        {
            var thread = new WorkerTask(1, _ => new Result());
            Action action = () => thread.Dispose();

            action.Should().NotThrow();
        }

        [Test]
        public void WorkerTask_Dispose_ThreadEnded()
        {
            var thread = new WorkerTask(1, _ => new Result());
            Action action = () => thread.Dispose();
            thread.Start();
            action.Should().NotThrow();
        }

        [Test]
        public void WorkerTask_Dispose_Started()
        {
            var thread = new WorkerTask(1, _ =>
            {
                System.Threading.Tasks.Task.Delay(TimeSpan.FromMinutes(1)).Wait();
                return new Result();
            });
            Action action = () => thread.Dispose();
            thread.Start();
            action.Should().NotThrow();
        }

        [Test]
        public void WorkerTask_Start_MultipleTimes()
        {
            var thread = new WorkerTask(1, _ =>
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
        public void WorkerTask_Start_EndedThread()
        {
            var thread = new WorkerTask(1, _ =>
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
        public void WorkerTask_Result()
        {
            var result = new Result();
            var thread = new WorkerTask(1, _ => result);
            thread.Start();
            thread.WaitHandle.WaitOne();

            thread.Result.Should().BeSameAs(result);
        }

        [Test]
        public void WorkerTask_ThreadNumberInResult()
        {
            var thread = new WorkerTask(1, nbr => new Result { ThreadNumber = nbr });
            thread.Start();
            thread.WaitHandle.WaitOne();

            thread.Result.ThreadNumber.Should().Be(1);
        }

        [Test]
        public void WorkerTask_ThreadNumberIsPassed()
        {
            var action = new Mock<Func<int, IResult>>();
            var thread = new WorkerTask(1, action.Object);
            thread.Start();
            thread.WaitHandle.WaitOne();

            action.Verify(x => x.Invoke(1), Times.Once);
        }
    }
}
