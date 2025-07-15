---
layout: "default"
---
# MeasureMap

.NET Benchmarking made simple  
  
MeasureMap is a lightweight assembly that allows profiling and benchmarking code.
  
## Profiling
The Profiler is used to test the performance of code.  
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
Benchmarks are used to compare the performance of different implementations of code.  
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
  
The result is traced to the console and is written in the Markdown format.
```
### MeasureMap Benchmark
Iterations:		10
#### Summary
| Name   | Avg Time         | Avg Ticks | Total            | Fastest | Slowest | Memory Increase |
|------- |----------------: |---------: |----------------: |-------: |-------: |---------------: |
| sha256 | 00:00:00.0000924 | 924       | 00:00:00.0009243 | 776     | 1471    | 1392            |
| Md5    | 00:00:00.0000485 | 485       | 00:00:00.0004858 | 409     | 534     | 1392            |
```