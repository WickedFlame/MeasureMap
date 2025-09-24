using System.Diagnostics;
using MeasureMap.Runners;

namespace MeasureMap.BenchmarkTests
{
    public class TimedTaskExecutionBenchmark
    {
        [Test]
        public void TimedTaskExecution_Benchmark_Timer()
        {
            var benchmarks = new Dictionary<string, TimeSpan[]>
            {
                { "1 ms delay", Measure(TimeSpan.FromMilliseconds(1)) },
                { "10 ms delay", Measure(TimeSpan.FromMilliseconds(10)) },
                { "100 ms delay", Measure(TimeSpan.FromMilliseconds(100)) },
                { "1000 ms delay", Measure(TimeSpan.FromMilliseconds(1000)) }
            };

            Trace.WriteLine("| Key | Avg ms | Avg ticks |");
            foreach (var benchmark in benchmarks)
            {
                Trace.WriteLine($"| {benchmark.Key} | {benchmark.Value.Average(v => v.TotalMilliseconds)} | {benchmark.Value.Average(v => v.Ticks)} |");
            }

            // skip first because that one is a direct runthrough and fakes statistics
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
            var context = new ExecutionContext
            {
                Logger =
                {
                    MinLogLevel = MeasureMap.Diagnostics.LogLevel.Debug
                }
            };

            var times = new TimeSpan[10];

            // Start Stopwatch before creating TimedTaskExecution
            // The ctor of TimedTaskExecution starts the Stopwatch to track the timer
            // 
            var sw = Stopwatch.StartNew();

            var execution = new TimedTaskExecution(interval);
            for (var i = 0; i < 10; i++)
            {
                execution.Execute(context, c => { });
                times[i] = sw.Elapsed;
                sw.Restart();
            }

            return times;
        }
    }
}
