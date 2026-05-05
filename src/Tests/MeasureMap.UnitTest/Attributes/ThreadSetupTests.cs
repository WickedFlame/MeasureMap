using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MeasureMap.UnitTest.Attributes
{
    [SingleThreaded]
    public class ThreadSetupTests
    {
        [Test]
        public void ExecuteTest()
        {
            Results = [];

            var runner = new BenchmarkRunner();
            var result = runner.RunSession<ThreadSetupBenchmark>();

            result.Trace();

            result.First().AverageTicks.Should().BeGreaterThan(0);

            Results.Should().HaveCount(4);
            Results.Where(r => r.Name == "Parse_1").Should().HaveCount(2);
            Results.Where(r => r.Name == "Parse_2").Should().HaveCount(2);
            Results.All(r => r.OnStartCalled && r.OnEndCalled && r.BenchmarkCalls == 10).Should().BeTrue();
        }

        internal static List<BenchmarkTestResult> Results { get; private set; } = [];
    }

    [RunWarmup(false)]
    [Iterations(10)]
    [Threads(2)]
    public class ThreadSetupBenchmark
    {
        private int _counter = 0;
        private string _id = Guid.NewGuid().ToString();

        private BenchmarkTestResult _result = new();

        public ThreadSetupBenchmark()
        {
            Debug.WriteLine($"-> Constructor { _id }");
        }

        [OnStartPipeline]
        public IExecutionContext Setup(ProfilerSettings settings)
        {
            Debug.WriteLine($"-> {_id} Start Pipeline {_counter}");
            _result.OnStartCalled = true;
            return settings.CreateContext();
        }

        [OnEndPipeline]
        public void Teardown()
        {
            Debug.WriteLine($"-> {_id} End Pipeline {_counter}");
            _result.OnEndCalled = true;
            _result.BenchmarkCalls = _counter;

            ThreadSetupTests.Results.Add(_result);
        }

        [Benchmark]
        public void Parse_1()
        {
            _result.Name = "Parse_1";
            _counter++;
            Debug.WriteLine($"-> { _id } Benchmark 1 {_counter}");
        }

        [Benchmark]
        public void Parse_2()
        {
            _result.Name = "Parse_2";
            _counter++;
            Debug.WriteLine($"-> {_id} Benchmark 2 {_counter}");
        }
    }

    public class BenchmarkTestResult
    {
        public string Name { get; set; }

        public bool OnStartCalled { get; set; }

        public bool OnEndCalled { get; set; }

        public int BenchmarkCalls { get; set; }
    }
}
