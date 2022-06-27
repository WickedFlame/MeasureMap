using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FluentAssertions;
using MeasureMap.Runners;
using NUnit.Framework;
using MeasureMap;

namespace MeasureMap.UnitTest.Runners
{
    public class TimedTaskExecutionBenchmark
    {
        [Test]
        public void TimedTaskExecution_Benchmark()
        {
            TimedTaskExecution execution = null;
            var context = new ExecutionContext();

            var runner = new BenchmarkRunner();
            runner.SetIterations(10);
            runner.Task("1 ms delay", c =>
            {
                execution.Execute(context, c => { });
            }).Setup(() =>
            {
                var time = TimeSpan.FromMilliseconds(1);
                execution = new TimedTaskExecution(time);
            });

            runner.Task("10 ms delay", c =>
            {
                execution.Execute(context, c => { });
            }).Setup(() =>
            {
                var time = TimeSpan.FromMilliseconds(10);
                execution = new TimedTaskExecution(time);
            });

            runner.Task("100 ms delay", c =>
            {
                execution.Execute(context, c => { });
            }).Setup(() =>
            {
                var time = TimeSpan.FromMilliseconds(100);
                execution = new TimedTaskExecution(time);
            });

            var result = runner.RunSessions();
            result.Trace();
        }

        [Test]
        public void TimedTaskExecution_Benchmark_Timer()
        {
            var benchmarks = new Dictionary<string, List<TimeSpan>>();

            benchmarks.Add("1 ms delay", Measure(TimeSpan.FromMilliseconds(1)));
            benchmarks.Add("10 ms delay", Measure(TimeSpan.FromMilliseconds(10)));
            benchmarks.Add("100 ms delay", Measure(TimeSpan.FromMilliseconds(100)));
            benchmarks.Add("1000 ms delay", Measure(TimeSpan.FromMilliseconds(1000)));

            Trace.WriteLine("| Key | Avg ms | Avg ticks |");
            foreach (var benchmark in benchmarks)
            {
                Trace.WriteLine($"| {benchmark.Key} | {benchmark.Value.Average(v => v.TotalMilliseconds)} | {benchmark.Value.Average(v => v.Ticks)} |");
            }

            // skip first because that one is a direct runthroug and fakes statistics
            benchmarks["1 ms delay"].Skip(1).Average(v => v.TotalMilliseconds).Should().BeGreaterThan(1).And.BeLessThan(20);
            benchmarks["10 ms delay"].Skip(1).Average(v => v.TotalMilliseconds).Should().BeGreaterThan(10).And.BeLessThan(20);
            benchmarks["100 ms delay"].Skip(1).Average(v => v.TotalMilliseconds).Should().BeGreaterThan(100).And.BeLessThan(120);
            benchmarks["1000 ms delay"].Skip(1).Average(v => v.TotalMilliseconds).Should().BeGreaterThan(1000).And.BeLessThan(1020);
        }

        private List<TimeSpan> Measure(TimeSpan interval)
        {
            var context = new ExecutionContext();

            var times = new TimeSpan[10];
            var sw = new Stopwatch();

            var execution = new TimedTaskExecution(interval);

            sw.Start();
            for (var i = 0; i < 10; i++)
            {
                execution.Execute(context, c => { });
                times[i] = sw.Elapsed;
                sw.Restart();
            }

            return times.ToList();
        }
    }
}
