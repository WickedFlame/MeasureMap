# MeasureMap
[![Build Status](https://travis-ci.org/WickedFlame/MeasureMap.svg?branch=master)](https://travis-ci.org/WickedFlame/MeasureMap)
[![Build status](https://ci.appveyor.com/api/projects/status/x0u2yu08pq7xye9w/branch/master?svg=true)](https://ci.appveyor.com/project/chriswalpen/measuremap/branch/master)

Simple Framework that helps easily create Performance Benchmarks.

ProfileSession is always the starting point of each Benchmark test.
This builds the profiler and allows execution of the Task. 

Execute the Task for n iterations where n is 200 in this example.
```csharp
var result = ProfileSession.StartSession()
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
var result = ProfileSession.StartSession()
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

Execute a setup/cleanup Task before each execution of the profiling Task.
```csharp
var output = string.Empty;
var result = ProfileSession.StartSession()
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
var result = ProfileSession.StartSession()
		.Task(() => 
		{
			// This represents the Task that needs testint
			System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.001));
		})
		.SetIterations(200)
		.SetThreads(10)
		.AddCondition(pr => pr.Iterations.Count() == 1)
		.RunSession();

var md = reult.Trace();
```

 