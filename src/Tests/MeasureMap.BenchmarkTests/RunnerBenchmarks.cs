using MeasureMap.Tracers;
using System.Security.Cryptography;

namespace MeasureMap.BenchmarkTests;

public class RunnerBenchmarks
{
    [Test]
    public void RunBenchmark()
    {
        var runner = new BenchmarkRunner();
        runner.SetIterations(10);

        
        var sha256 = SHA256.Create();
        var data = new byte[10000];
        new Random(42).NextBytes(data);
                
        runner.Task("Fluent Runner", () =>
        {
            var r = new BenchmarkRunner();
            r.SetIterations(10);
            // Simulate some work
            r.Task("sha256", () => sha256.ComputeHash(data));

            r.RunSessions();
        });

        runner.Task("Attribute Runner", () =>
        {
            var r = new BenchmarkRunner();
            r.RunSession<AttributeRunnerBenchmarks>();
        });
        
        var result = runner.RunSessions();
        TraceOptions.Default.TraceDetail = TraceDetail.FullDetail;
        result.Trace();
        /*
    # MeasureMap - After refactoring Pipelines to Stacks
     Iterations: 10
    ## Summary
    | Name             |        Avg. Time | Avg. Milliseconds |          Fastest |          Slowest | Iterations |     Throughput |
    | ---------------- | ---------------: | ----------------: | ---------------: | ---------------: | ---------- | -------------: |
    | Fluent Runner    | 00:00:00.0168516 |        16.8516 ms | 00:00:00.0155994 | 00:00:00.0185056 | 10         |     59.34151/s |
    | Attribute Runner | 00:00:00.0166107 |        16.6107 ms | 00:00:00.0156572 | 00:00:00.0191793 | 10         |     60.20194/s |
    

    # MeasureMap - Original
     Iterations: 10
    ## Summary
    | Name             |        Avg. Time | Avg. Milliseconds |          Fastest |          Slowest | Iterations |     Throughput |
    | ---------------- | ---------------: | ----------------: | ---------------: | ---------------: | ---------- | -------------: |
    | Fluent Runner    | 00:00:00.0241381 |        24.1381 ms | 00:00:00.0164602 | 00:00:00.0336516 | 10         |     41.42818/s |
    | Attribute Runner | 00:00:00.0165946 |        16.5946 ms | 00:00:00.0145283 | 00:00:00.0208026 | 10         |     60.26035/s |
         */
    }

    [Iterations(10)]
    public class AttributeRunnerBenchmarks
    {
        private readonly SHA256 _sha256;
        private readonly byte[] _data;

        public AttributeRunnerBenchmarks()
        {
            _sha256 = SHA256.Create();
            _data = new byte[10000];
            new Random(42).NextBytes(_data);
        }
        
        [Benchmark]
        public void Task()
        {
            // Simulate some work
            _sha256.ComputeHash(_data);
        }
    }
}