---
title: Benchmarking
layout: "default"
nav_order: 2
---
# Benchmarking
Benchmarking is used to compare the performance of multiple different implementations of the same logic.  
Benchmarking compares multiple [Profilingsessions](profiling).
```csharp
var sha256 = SHA256.Create();
var md5 = MD5.Create();

var data = new byte[10000];
new Random(42).NextBytes(data);

var runner = new BenchmarkRunner();
runner.SetIterations(10);
runner.Task("sha256", () => sha256.ComputeHash(data));
runner.Task("Md5", () => md5.ComputeHash(data));

var result = runner.RunSessions();

result.Trace();
```
### Resulting MarkDown
```
### MeasureMap Benchmark
 Iterations:		10
#### Summary
| Name   |         Avg Time | Avg Ticks |            Total | Fastest | Slowest | Memory Increase |
|------- |----------------: |---------: |----------------: |-------: |-------: |---------------: |
| sha256 | 00:00:00.0000924 |       924 | 00:00:00.0009243 |     776 |    1471 |            1392 |
| Md5    | 00:00:00.0000485 |       485 | 00:00:00.0004858 |     409 |     534 |            1392 |
```

Instead of only adding a Task, it is possible to add complete ProfilerSessions. 
This way it is possible to define all the properties that a ProfilerSession posesses.
```csharp
var sha256 = SHA256.Create();
var md5 = MD5.Create();

var data = new byte[10000];
new Random(42).NextBytes(data);

var runner = new BenchmarkRunner();
runner.SetIterations(10);
runner.AddSession("sha256",
    ProfilerSession.StartSession()
        .Task(() => sha256.ComputeHash(data))
        .SetThreads(5)
);
runner.AddSession("Md5",
    ProfilerSession.StartSession()
        .Task(() => Md5.ComputeHash(data))
        .SetThreads(5)
);
var result = runner.RunSessions();

result.Trace();
```
The iterations should be defined in the runner scope. Elese the number of iteration defined on the first ProfilerSession is used