# MeasureMap
.NET Benchmarking made simple

[![Build status](https://img.shields.io/appveyor/build/chriswalpen/measuremap/master?label=Master&logo=appveyor&style=for-the-badge)](https://ci.appveyor.com/project/chriswalpen/measuremap/branch/master)
[![Build status](https://img.shields.io/appveyor/build/chriswalpen/measuremap/dev?label=Dev&logo=appveyor&style=for-the-badge)](https://ci.appveyor.com/project/chriswalpen/measuremap/branch/dev)
  
[![NuGet Version](https://img.shields.io/nuget/v/measuremap.svg?style=for-the-badge&label=Latest)](https://www.nuget.org/packages/measuremap/)
[![NuGet Version](https://img.shields.io/nuget/vpre/measuremap.svg?style=for-the-badge&label=RC)](https://www.nuget.org/packages/measuremap/)
  
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=WickedFlame_MeasureMap&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=WickedFlame_MeasureMap)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=WickedFlame_MeasureMap&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=WickedFlame_MeasureMap)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=WickedFlame_MeasureMap&metric=coverage)](https://sonarcloud.io/summary/new_code?id=WickedFlame_MeasureMap)
  
Measuremap allows profiling and benchmarking from simple code fragmets to full applications.
  
Visit [https://wickedflame.github.io/MeasureMap/](https://wickedflame.github.io/MeasureMap/) for the full documentation.
  
MeasureMap uses the builder pattern and a fluent API to make profiling or benchmarking as simple as possible.
  
## Profiling
Profiles are initiated with ProfilerSession.StartSession().
  
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
Benchmarks are a collection of ProfilerSessions. 
These are initiated and started with the BenchmarkRunner.
  
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
