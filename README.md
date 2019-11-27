# MeasureMap
[![Build Status](https://travis-ci.org/WickedFlame/MeasureMap.svg?branch=master)](https://travis-ci.org/WickedFlame/MeasureMap)
[![Build status](https://ci.appveyor.com/api/projects/status/x0u2yu08pq7xye9w/branch/master?svg=true)](https://ci.appveyor.com/project/chriswalpen/measuremap/branch/master)

Powerful library that makes performance, benchmark and loadtesting simple.

ProfileSession is always the starting point of each Benchmark test.
This builds the profiler and allows execution of the Task. 

Execute the Task for n iterations where n is 200 in this example.
```csharp
var result = ProfilerSession.StartSession()
		.Task(() => 
		{
			// This represents the Task that needs testint
			System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.001));
		})
		.SetIterations(200)
		.AddCondition(pr => pr.Iterations.Count() == 1)
		.RunSession();

Assert.IsTrue(result.AverageMilliseconds < 20);
```

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
		.AddCondition(pr => pr.Iterations.Count() == 1)
		.RunSession();

Assert.IsTrue(result.Iterations.Count() == 2000);
```

Add conditions that are tested
```csharp
ProfilerSession.StartSession()
		.Task(() => 
		{
			// This represents the Task that needs testint
			System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.001));
		})
		.AddCondition(pr => pr.Iterations.Count() == 1)
		.RunSession();
```

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
		.SetIterations(3)
		.RunSession();

Assert.IsTrue(output == "before task task task after");
```

Trace the result as Markdown text
```csharp
var result = ProfilerSession.StartSession()
		.Task(() => System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.001)))
		.SetIterations(200)
		.RunSession();

var md = result.Trace();
```
which produces the following Markup:
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

#### ExecutionContext
The ExecutionContext is a object containing information about the current run. The Context can be used to store and pass Data to each Taskexecution.
```csharp
ProfilerSession.StartSession()
		.Task(ctx => {
			// ctx is passed to all iterations
			var iteration = ctx.Get<int>(ContextKeys.Iteration);
		});
```