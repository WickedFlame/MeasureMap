# MeasureMap
Profiling and Benchmarking .NET Code made simple  
MeasureMap allows profiling and benchmarking from simple code fragments to full applications.
  
Visit [https://wickedflame.github.io/MeasureMap/](https://wickedflame.github.io/MeasureMap/) for the full documentation.
  
MeasureMap uses the builder pattern and a fluent API to make profiling or benchmarking as simple as possible.
  
## Profiling
The Builder for profiling is initiated with the ProfilerSession when running the StartSession method.
  
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
RunSession executes the profiler for the registered Task.

## Benchmarking
Benchmarks are basically just multiple Profiler-Sessions that are run in one Session.  
The Benchmarks are registered to and executed by the BenchmarkRunner.
  
Benchmarks can be run in two ways
- FluentAPI
- Attributes

### FluentAPI
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
### Attributes
```csharp
[Iterations(10)]
[Threads(10)]
//[Duration(10)]
[RunWarmup(false)]
public class  WorkflowBenchmark
{
    private readonly SHA256 _sha256;
    private readonly MD5 _md5;
    private readonly byte[] _data;
    
    public WorkflowBenchmark()
    {
        _sha256 = SHA256.Create();
        _md5 = MD5.Create();

        _data = new byte[10000];
        new Random(42).NextBytes(_data);
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

## Tracing the Result
The result is by default traced as Markdown  
```
### MeasureMap Benchmark
 Iterations:		10
#### Summary
| Name   | Avg Time         | Avg Ticks | Total            | Fastest | Slowest | Memory Increase |
|------- |----------------: |---------: |----------------: |-------: |-------: |---------------: |
| sha256 | 00:00:00.0000924 | 924       | 00:00:00.0009243 | 776     | 1471    | 1392            |
| md5    | 00:00:00.0000485 | 485       | 00:00:00.0004858 | 409     | 534     | 1392            |
```


# Workflow

## SessionPipeline (SessionHandler / ISessionHandler)
Run once per Session
* ElapsedTimeSessionHandler
* PreExecutionSessionHandler
* WarmupSessionHandler
* BasicSessionHandler / MainThreadSessionHandler / MultyThreadSessionHandler
### To be defined...
Run once per Thread
* OnStartPipeline (Event)
* Worker
* OnEndPipeline (Event)
#### ProcessingPipeline (TaskHandler / ITaskMiddleware)
Run once every Iteration
* ProcessDataTaskHandler (ITaskMiddleware)
* MemoryCollectionTaskHandler (ITaskMiddleware)
* ElapsedTimeTaskHandler (ITaskMiddleware)
* _task (ITask)


Factory that creates a instance per thread for:
* OnStartPipeline (Event)
* Run Worker
* OnEndPipeline (Event)
PipelineRunner ?