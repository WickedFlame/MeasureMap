using MeasureMap.Benchmark;

var bm = "ThroughputBenchmarks";//"BenchmarkSample";

switch (bm)
{
    case nameof(BenchmarkSample):
        new BenchmarkSample().RunBenchmarks();
        break;
    case nameof(StartSessionBenchmarks):
        new StartSessionBenchmarks().RunBenchmarks();
        break;
    case nameof(WorkerThreadListBenchmarks):
        new WorkerThreadListBenchmarks().RunBenchmarks();
        break;
    case nameof(ThroughputBenchmarks):
        new ThroughputBenchmarks().RunBenchmarks();
        break;
}

