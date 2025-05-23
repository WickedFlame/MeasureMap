﻿using MeasureMap.Benchmark;

var bm = "RampupBenchmarks";
//var bm = "ThreadBehaviourBenchmarks";
//var bm = "BenchmarkUsingMultipleThreads";
//var bm = "BenchmarkSample";
//var bm = "WorkerThreadListBenchmarks";
//var bm =  "ThroughputBenchmarks";
//var bm =  "BenchmarkSample";

switch (bm)
{
    case nameof(BenchmarkSample):
        new BenchmarkSample().RunBenchmarks();
        break;
    case nameof(BenchmarkUsingMultipleThreads):
        new BenchmarkUsingMultipleThreads().RunBenchmarks();
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
    case nameof(ThreadBehaviourBenchmarks):
        new ThreadBehaviourBenchmarks().RunBenchmarks();
        break;
    case nameof(RampupBenchmarks):
        new RampupBenchmarks().RunBenchmarks();
        break;
}


Console.ReadLine();
