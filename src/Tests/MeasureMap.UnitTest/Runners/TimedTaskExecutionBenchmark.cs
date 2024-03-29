﻿using System;
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
                execution.Execute(context, d => { });
            }).Setup(() =>
            {
                var time = TimeSpan.FromMilliseconds(1);
                execution = new TimedTaskExecution(time);
            });

            runner.Task("10 ms delay", c =>
            {
                execution.Execute(context, d => { });
            }).Setup(() =>
            {
                var time = TimeSpan.FromMilliseconds(10);
                execution = new TimedTaskExecution(time);
            });

            runner.Task("100 ms delay", c =>
            {
                execution.Execute(context, d => { });
            }).Setup(() =>
            {
                var time = TimeSpan.FromMilliseconds(100);
                execution = new TimedTaskExecution(time);
            });

            var result = runner.RunSessions();
            result.Trace();

            result.Should().HaveCount(3);
        }

        [Test]
        public void TimedTaskExecution_Benchmark_Timer()
        {
            var benchmarks = new Dictionary<string, TimeSpan[]>();

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
            benchmarks["1 ms delay"].Skip(2).Average(v => v.TotalMilliseconds).Should().BeGreaterThan(1, TraceResult(benchmarks["1 ms delay"])).And.BeLessThan(20);
            benchmarks["10 ms delay"].Skip(2).Average(v => v.TotalMilliseconds).Should().BeGreaterThan(10, TraceResult(benchmarks["1 ms delay"])).And.BeLessThan(20);
            benchmarks["100 ms delay"].Skip(2).Average(v => v.TotalMilliseconds).Should().BeGreaterThan(100, TraceResult(benchmarks["1 ms delay"])).And.BeLessThan(120);
            benchmarks["1000 ms delay"].Skip(2).Average(v => v.TotalMilliseconds).Should().BeGreaterThan(1000, TraceResult(benchmarks["1 ms delay"])).And.BeLessThan(1020);
        }

        private string TraceResult(IEnumerable<TimeSpan> skip)
        {
            return $"{Environment.NewLine} - " + string.Join($"{Environment.NewLine} - ", skip);
        }

        private TimeSpan[] Measure(TimeSpan interval)
        {
            var context = new ExecutionContext();
            context.Logger.MinLogLevel = MeasureMap.Diagnostics.LogLevel.Debug;

            var times = new TimeSpan[10];

            // Start Stopwatch before creating TimedTaskExecution
            // The ctor of TimedTaskExecution starts the Stopwatch to track the timer
            // 
            var sw = Stopwatch.StartNew();

            var execution = new TimedTaskExecution(interval);
            for (var i = 0; i < 10; i++)
            {
                execution.Execute(context, c =>
                {
                    
                });

                times[i] = sw.Elapsed;
                sw.Restart();
            }

            return times;
        }
    }
}
