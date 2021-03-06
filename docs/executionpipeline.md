---
title: Execution Pipeline
layout: "default"
nav_order: 2
---

## MeasureMap execution pipeline
The execution pipeline of MeasureMap followes the Chain of Responsibility Pattern
There are 2 kinds of Pipelines in MeasureMap
1. The SessionPipeline is the main pipeline that is run for each session.
2. The ExecutionPipeline is run for each execution.

The ExecutionPipeline is run as part of the SessionPipeline.

The Task-Pipeline is executed twice
1. In the WarmupSessionHandler pipeline
2. In the execution pipeline

### Pipeline setup
1. All custom handlers that are added in the setup
2. ProcessDataTaskHandler
3. MemoryCollectionTaskHandler
4. ElapsedTimeTaskHandler
5. Execution of the Task


## Extend the execution pipeline
Implement ITaskMiddleware for the middleware
```csharp
public class CustomMiddleware : ITaskMiddleware
{
    private readonly Action _delegate;
    private ITask _next;

    public CustomMiddleware(Action delega)
    {
        _delegate = delega;
    }
    public IIterationResult Run(IExecutionContext context)
    {
        _delegate();
        var result = _next.Run(context);
        _delegate();

        return result;
    }

    public void SetNext(ITask next)
    {
        _next = next;
    }
}
```

```csharp
int calls = 0;

ProfilerSession.StartSession()
    .AddMiddleware(new CustomMiddleware(() => calls++))
    .Task(() => calls++)
    .RunSession();
```

