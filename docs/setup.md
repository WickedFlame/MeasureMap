---
title: Setup
layout: "default"
nav_order: 2
---
# Setup
MeasuerMap can run in different setups.  
  
### Run a Task once
Without any configuration, the task is run once.  
```csharp
var result = ProfilerSession.StartSession()
    .Task(() => 
    {
        // This represents the Task
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
        // This represents the Task
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
        // This represents the Task
        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.001));
    })
    .SetIterations(200)
    .SetThreads(10)
    .RunSession();
```
  
### Duration
Set a duration that the task is run for
```csharp
var result = ProfilerSession.StartSession()
    .Task(() => 
    {
        // This represents the Task
        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.001));
    })
    .SetDuration(TimeSpan.FromMinutes(5))
    .RunSession();
```
  
### Interval
Set a interval that the task is run at
```csharp
var result = ProfilerSession.StartSession()
    .Task(() => 
    {
        // This represents the Task
        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.001));
    })
    .SetInterval(TimeSpan.FromMilliseconds(50))
    .RunSession();
```