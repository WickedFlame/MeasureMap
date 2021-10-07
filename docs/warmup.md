---
title: Warmup
layout: "default"
nav_order: 2
---
# Warmup
Each Session is started with a warmup.  
The Task will be executed once for the warmup. After the warmup all will be reset.  
That means the task will be executed the amount of itterations + the warmup. This has to be kept in mind when a count is used in the task.  
  
It is possible to disable the Warmup in the Settings
```csharp
ProfilerSession.StartSession()
    .Task(itm =>
    {
        var tmp = new { Count = itm.Count + 1 };
        // do something
        return tmp;
    })
    .Settings(s => {
        s.RunWarmup = false;
    })
    .RunSession();
```
