# MeasureMap
[![Build Status](https://travis-ci.org/WickedFlame/MeasureMap.svg?branch=master)](https://travis-ci.org/WickedFlame/MeasureMap)

Simple Performance Benchmarking Framework

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
