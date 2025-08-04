using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MeasureMap.Attributes.Builder;

namespace MeasureMap.Attributes;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class AttributeSessionBuilder<T> where T : class, new()
{
    private readonly BenchmarkRunner _runner;

    private readonly IEnumerable<IBenchmarkBuilderElement> _benchmarkBuilders =
    [
        new DurationBuilderElement(),
        new IterationsBuilderElement(),
        new OnStartPipelineBuilderElement(),
        new OnEndPipelineBuilderElement(),
        new ThreadsBuilderElement(),
        new RunWarmupBuilderElement()
    ];

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
        var instance = Activator.CreateInstance<T>();

        foreach (var builder in _benchmarkBuilders)
        {
            builder.Initialize<T>(instance);
            builder.Append(_runner);
        }

        var methods = typeof(T).GetMethods();
        foreach (var method in methods.Where(m => m.GetCustomAttribute<BenchmarkAttribute>() != null))
        {
            var session = ProfilerSession.StartSession()
                .AppendSettings(_runner.Settings);
            
            foreach (var builder in _benchmarkBuilders)
            {
                builder.Append(session);
            }
            
            if (method.GetParameters().Any(p => p.ParameterType == typeof(IExecutionContext)))
            {
                session.Task(ctx => method.Invoke(instance, [ctx]));
            }
            else
            {
                session.Task(() => method.Invoke(instance, null));
            }

            _runner.AddSession(method.Name, session);
        }
    }
}