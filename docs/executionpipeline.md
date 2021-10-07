---
title: Execution Pipeline
layout: "default"
nav_order: 2
---

## MeasureMap execution pipeline
The execution pipeline of MeasureMap followes the Chain of Responsibility Pattern. Each pipeline can be extended with custom handlers.  
There are 2 kinds of Pipelines in MeasureMap that operate in different scopes  
1. The SessionPipeline is the main pipeline that is run for each session.
2. The ExecutionPipeline is run for each execution.
  
The ExecutionPipeline is the pipeline that runs the tasks and is run as the last part of the SessionPipeline.
  
The Task-Pipeline is executed twice
1. In the WarmupSessionHandler pipeline
2. In the execution pipeline
  
The Warmup is run so that the system can be started and all components are warm and prepared for the execution. Without the Warmup, some Tasks run slower on the firs run.  
If this is not wanted the Warmup can be deactivated in the ProfilerSession.  
  
### Pipeline setup
1. All custom handlers that are added in the setup
2. ProcessDataTaskHandler
3. MemoryCollectionTaskHandler - Measures the amount of Memory used by the execution. This measurement is due to GC not accurate.
4. ElapsedTimeTaskHandler - Measures the amount of time used by the execution.
5. Execution of the Task

  
## Extend the execution pipeline
The Executiopipeline can be extended very easily.  
Create a class that implements ITaskMiddleware for the middleware and add the desired logic in there
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
  
This simple example of a Middleware just counts the amout of times the task is run.  
```csharp
int calls = 0;

ProfilerSession.StartSession()
    .AddMiddleware(new CustomMiddleware(() => calls++))
    .Task(() => calls++)
    .RunSession();
```

