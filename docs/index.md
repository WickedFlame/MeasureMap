---
layout: "default"
---
# MeasureMap

.NET Benchmarking made simple

[![Build Status](https://travis-ci.org/WickedFlame/MeasureMap.svg?branch=master)](https://travis-ci.org/WickedFlame/MeasureMap)
[![Build status](https://ci.appveyor.com/api/projects/status/x0u2yu08pq7xye9w/branch/master?svg=true)](https://ci.appveyor.com/project/chriswalpen/measuremap/branch/master)
[![NuGet Version](https://img.shields.io/nuget/v/measuremap.svg?style=flat)](https://www.nuget.org/packages/measuremap/)

## Profiling
```csharp
var result = ProfilerSession.StartSession()
	.Task(() => 
	{
		// This represents the Task that needs testint
		System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.001));
	})
	.SetIterations(200)
	.Assert(pr => pr.Iterations.Count() == 1)
	.RunSession();

result.Trace();

Assert.IsTrue(result.AverageMilliseconds < 20);
```


## Benchmarking
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
### MeasureMap Benchmark
Iterations:		10
#### Summary
| Name | Avg Time | Avg Ticks | Total | Fastest | Slowest | Memory Increase |
|--- |---: |---: |---: |---: |---: |---: |
| sha256 | 00:00:00.0000924 | 924 | 00:00:00.0009243 | 776 | 1471 | 1392 |
| Md5 | 00:00:00.0000485 | 485 | 00:00:00.0004858 | 409 | 534 | 1392 |