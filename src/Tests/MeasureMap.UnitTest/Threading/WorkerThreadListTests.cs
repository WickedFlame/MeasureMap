using MeasureMap.Threading;
using System;
using System.Linq;

namespace MeasureMap.UnitTest.Threading
{
    public class WorkerThreadListTests
    {
        [Test]
        public void WorkerThreadList_StartNew_Thread()
        {
            var list = new WorkerThreadList();
            var thread = list.StartNew(1, _ => new Result(), WorkerThread.Factory);

            thread.Should().NotBeNull();
        }

        [Test]
        public void WorkerThreadList_StartNew_Task()
        {
            var list = new WorkerThreadList();
            var thread = list.StartNew(1, _ => new Result(), WorkerTask.Factory);

            thread.Should().NotBeNull();
        }

        [Test]
        public void WorkerThreadList_StartNew_Added()
        {
            var list = new WorkerThreadList();
            var thread = list.StartNew(1, _ => new Result(), WorkerThread.Factory);

            list.Single().Should().BeSameAs(thread);
        }

        [Test]
        public void WorkerThreadList_StartNew_Task_Added()
        {
            var list = new WorkerThreadList();
            var thread = list.StartNew(1, _ => new Result(), WorkerTask.Factory);

            list.Single().Should().BeSameAs(thread);
        }

        [Test]
        public void WorkerThreadList_Add()
        {
            var thread = new WorkerThread(1, _ => new Result());
            var list = new WorkerThreadList();
            list.Add(thread);

            list.Single().Should().BeSameAs(thread);
        }

        [Test]
        public void WorkerThreadList_Add_ListInitializer()
        {
            var thread = new WorkerThread(1, _ => new Result());
            var list = new WorkerThreadList
            {
                thread
            };

            list.Single().Should().BeSameAs(thread);
        }

        [Test]
        public void WorkerThreadList_Remove()
        {
            var thread = new WorkerThread(1, _ => new Result());
            var list = new WorkerThreadList
            {
                thread
            };
            list.Remove(thread);
            list.Should().BeEmpty();
        }

        [Test]
        public void WorkerThreadList_WaitAll()
        {
            var thread = new WorkerThread(1, _ =>
            {
                System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                return new Result();
            });
            var list = new WorkerThreadList
            {
                thread
            };
            thread.Start();

            list.WaitAll();

            list.Should().OnlyContain(t => !t.IsAlive);
        }

        [Test]
        public void WorkerThreadList_Task_WaitAll()
        {
            var thread = new WorkerTask(1, _ =>
            {
                System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                return new Result();
            });
            var list = new WorkerThreadList
            {
                thread
            };
            thread.Start();

            list.WaitAll();

            list.Should().OnlyContain(t => !t.IsAlive);
        }

        [Test]
        public void WorkerThreadList_WaitAll_MultipleTreads()
        {
            var list = new WorkerThreadList();
            list.StartNew(1, _ =>
            {
                System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                return new Result();
            }, WorkerThread.Factory).Start();
            list.StartNew(1, _ =>
            {
                System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                return new Result();
            }, WorkerThread.Factory).Start();

            list.WaitAll();

            list.Should().OnlyContain(t => !t.IsAlive);
        }

        [Test]
        public void WorkerThreadList_WaitAll_MultipleTasks()
        {
            var list = new WorkerThreadList();
            list.StartNew(1, _ =>
            {
                System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                return new Result();
            }, WorkerTask.Factory).Start();
            list.StartNew(1, _ =>
            {
                System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                return new Result();
            }, WorkerTask.Factory).Start();

            list.WaitAll();

            list.Should().OnlyContain(t => !t.IsAlive);
        }
    }
}
