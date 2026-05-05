using MeasureMap.Attributes.Builder;
using MeasureMap.ContextStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MeasureMap.Attributes;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class AttributeSessionBuilder<T> where T : class, new()
{
    private readonly BenchmarkRunner _runner;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="runner"></param>
    public AttributeSessionBuilder(BenchmarkRunner runner)
    {
        _runner = runner;
    }

    /// <summary>
    /// Build <see cref="ProfilerSession"/> based on the Attributes on the benchmark class
    /// </summary>
    public void BuildSessions()
    {
        var methods = typeof(T).GetMethods();
        foreach (var method in methods.Where(m => m.GetCustomAttribute<BenchmarkAttribute>() != null))
        {

            IEnumerable<IBenchmarkBuilderElement> _benchmarkBuilders =
                [
                    new DurationBuilderElement(),
                    new IterationsBuilderElement(),
                    new OnStartPipelineBuilderElement(),
                    new OnEndPipelineBuilderElement(),
                    new ThreadsBuilderElement(),
                    new RunWarmupBuilderElement()
                ];


            var contextBuilder = new InstanceBasedStackBuilder<T>(CreateTaskFactory(method));

            foreach (var element in _benchmarkBuilders)
            {
                element.Initialize<T>();
                element.Append(_runner);
                element.Append(contextBuilder);
            }

            var session = ProfilerSession.StartSession()
                .SetContextStackBuilder(contextBuilder)
                .AppendSettings(_runner.Settings);

            foreach (var builder in _benchmarkBuilders)
            {
                builder.Append(session);
            }

            // add a pseudo task to ensure the session is executed and the context stack is built
            session.Task(() => { });

            _runner.AddSession(method.Name, session);
        }
    }

    private Func<T, ITask> CreateTaskFactory(MethodInfo method)
    {
        if (method.GetParameters().Any(p => p.ParameterType == typeof(IExecutionContext)))
        {
            return obj => new ContextTask(ctx => method.Invoke(obj, [ctx]));
        }

        return obj => new Task(() => method.Invoke(obj, null));
    }
}