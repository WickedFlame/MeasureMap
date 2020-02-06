# MeasureMap
.NET Benchmarking made simple

[![Build Status](https://travis-ci.org/WickedFlame/MeasureMap.svg?branch=master)](https://travis-ci.org/WickedFlame/MeasureMap)
[![Build status](https://ci.appveyor.com/api/projects/status/x0u2yu08pq7xye9w/branch/master?svg=true)](https://ci.appveyor.com/project/chriswalpen/measuremap/branch/master)
[![NuGet Version](https://img.shields.io/nuget/v/measuremap.svg?style=flat)](https://www.nuget.org/packages/measuremap/)

Visit [https://wickedflame.github.io/MeasureMap/](https://wickedflame.github.io/MeasureMap/) for the full documentation.

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
