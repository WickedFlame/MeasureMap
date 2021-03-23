---
title: Warmup
layout: "default"
nav_order: 2
---
Each Session is started with a warmup.
The Task will be executed once and then all will will be reset.

The Warmup can be disabled in the Settings

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
