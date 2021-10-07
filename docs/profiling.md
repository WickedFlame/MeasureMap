---
title: Profiling
layout: "default"
nav_order: 2
---
The profiler is used to measure the performance of a piece of code.  
  
### Run a Task
ProfilerSession.StartSession starts a new Profilingsession that measures the code that is passed to the Task method.  
```csharp
var result = ProfilerSession.StartSession()
    .Task(() => 
    {
        // This represents the Task that needs testint
        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.001));
    })
    .RunSession();
```
  
### Run a Task for n iterations
Execute the Task for n iterations where n is 200 in this example.
```csharp
var result = ProfilerSession.StartSession()
    .Task(() => 
    {
        // This represents the Task that needs testint
        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.001));
    })
    .SetIterations(200)
    .RunSession();
```
The result contains the summary of all iterations
  
### Run a Task for n iterations on n Threads
Execute the Task for n iterations each on y Threads. That means n*y iterations (200*10);
```csharp
var result = ProfilerSession.StartSession()
    .Task(() => 
    {
        // This represents the Task that needs testint
        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.001));
    })
    .SetIterations(200)
    .SetThreads(10)
    .RunSession();
```
  
### Define a condition that is checked with every iteration
Add conditions that are tested
```csharp
ProfilerSession.StartSession()
    .Task(() => 
    {
        // This represents the Task that needs testint
        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.001));
    })
    .Assert(pr => pr.Iterations.Count() == 1)
    .RunSession();
```
  
### Setup and teardown of tasks
Execute a setup/cleanup Task before each execution of the profiling Task.
```csharp
var output = string.Empty;
var result = ProfilerSession.StartSession()
    .BeforeExcute(() => output += "before")
    .AfterExcute(() => output += " after")
    .Task(() => 
    {
        // This represents the Task that needs testint
        output += " task"
    })
    .RunSession();
```

### Trace the result
Trace the result as Markdown text
```csharp
var result = ProfilerSession.StartSession()
    .Task(() => System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.001)))
    .SetIterations(200)
    .RunSession();

var md = result.Trace();
```
  
result.Trace() traces the following Markdown to the console:
```
### MeasureMap - Profiler result for Profilesession
##### Summary
	Warmup ========================================
		Duration Warmup:			00:00:00.0169524
	Setup ========================================
		Iterations:			200
	Duration ========================================
		Duration Total:			00:00:00.3211689
		Average Time:			00:00:00.0016058
		Average Milliseconds:		1
		Average Ticks:			16058
		Fastest:			00:00:00.0010291
		Slowest:			00:00:00.0127350
	Memory ==========================================
		Memory Initial size:		1232032
		Memory End size:		1338672
		Memory Increase:		106640
```
  
### Task with ExecutionContext
The ExecutionContext is a object containing information about the current run. The Context can be used to store and pass Data to each Taskexecution.
```csharp
ProfilerSession.StartSession()
    .Task(ctx => {
        // ctx is passed to all iterations
        var iteration = ctx.Get<int>(ContextKeys.Iteration);
    });
```
  
### Passing a return value to the next Task
Define a datatype that is returned from a Task and is passed to the task in the next iteration.
```csharp
ProfilerSession.StartSession()
    .Task<int>(i =>
    {
        // do something
        System.Threading.Thread.Sleep(50);
        return ++i;
    })
    .SetIterations(20)
    .RunSession();
```
  
The Task accepts all types. It is also possible to pass a anonymous objet to the next iteration.
```csharp
ProfilerSession.StartSession()
    .Task(itm =>
    {
        var tmp = new { Count = itm.Count + 1 };
        // do something
        return tmp;
    })
    .SetIterations(20)
    .RunSession();
```
