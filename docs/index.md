---
layout: "default"
---
# MeasureMap

Profiling and Benchmarking .NET Code made simple  
  
MeasureMap is a lightweight assembly that allows profiling and benchmarking code.

**Profiling** is the process of measuring and analyzing the performance characteristics of your code, such as execution time, memory usage, and resource consumption. It helps developers identify bottlenecks, optimize performance, and ensure their applications meet performance requirements.  
  
**Benchmarking** is the process of systematically measuring and comparing the performance of different implementations, algorithms, or approaches to solve the same problem. It provides quantitative data to help developers make informed decisions about which solution performs better under specific conditions.  
  
## How MeasureMap Helps

MeasureMap provides developers with powerful tools to measure and track code performance through:

- **Automated Measurement**: Automatically captures execution time, memory usage, and other performance metrics without manual instrumentation
- **Statistical Analysis**: Runs multiple iterations to provide reliable averages, identify performance variations, and detect outliers
- **Memory Tracking**: Monitors memory allocation and garbage collection impact during code execution
- **Flexible Testing**: Supports both single-method profiling and multi-implementation benchmarking scenarios
- **Clear Reporting**: Generates detailed reports in markdown format with performance summaries, timing breakdowns, and comparative analysis
- **Assertion Support**: Allows you to set performance expectations and automatically validate that your code meets specified criteria

Whether you're optimizing a single algorithm or comparing multiple approaches, MeasureMap simplifies the process of gathering actionable performance data.
  
## Profiling

The Profiler enables precise measurement of code execution characteristics, such as runtime duration and resource utilization, by instrumenting and analyzing targeted code segments under controlled conditions.  
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
```
Profiling uses a Builder, that has a FluentAPI, to collect all settings before running the Session.  
  
## Benchmarking
Benchmarking systematically evaluates and compares the performance characteristics—such as execution time, resource usage, and memory allocation—of multiple code implementations or algorithms under controlled conditions. This process provides quantitative metrics that help identify the most efficient solution for a given task or workload.  
```csharp
var sha256 = SHA256.Create();
var md5 = MD5.Create();

var data = new byte[10000];
new Random(42).NextBytes(data);

var runner = new BenchmarkRunner();
runner.SetIterations(10);
runner.Task("sha256", () => sha256.ComputeHash(data));
runner.Task("md5", () => md5.ComputeHash(data));

var result = runner.RunSessions();

result.Trace();
```
  
Alternatively to the FluentAPI, Benchmarks can be defined with Attributes.
```csharp
[Iterations(10)]
[Threads(10)]
//[Duration(10)]
public class  WorkflowBenchmark
{
    private SHA256 _sha256;
    private MD5 _md5;
    
    public WorkflowBenchmark()
    {
        _sha256 = SHA256.Create();
        _md5 = MD5.Create();

        _data = new byte[10000];
        new Random(42).NextBytes(data);
    }
    
    [OnStartPipeline]
    public void Setup()
    {
    }

    [OnEndPipeline]
    public void End()
    {
    }

    [Benchmark]
    public void sha256()
    {
        // Simulate some work
        _sha256.ComputeHash(_data);
    }

    [Benchmark]
    public void md5(IExecutionContext ctx)
    {
        // Simulate some work
        _md5.ComputeHash(_data);
    }
}
```
```csharp
[Test]
public void WorkflowTest_Benchmark()
{
    var runner = new BenchmarkRunner();
    var result = runner.RunSession<WorkflowBenchmark>();
    result.Trace();
}
```


The result is traced to the console and is written in the Markdown format.
```
### MeasureMap Benchmark
Iterations:		10
#### Summary
| Name   | Avg Time         | Avg Ticks | Total            | Fastest | Slowest | Memory Increase |
|------- |----------------: |---------: |----------------: |-------: |-------: |---------------: |
| sha256 | 00:00:00.0000924 | 924       | 00:00:00.0009243 | 776     | 1471    | 1392            |
| md5    | 00:00:00.0000485 | 485       | 00:00:00.0004858 | 409     | 534     | 1392            |
```
  
