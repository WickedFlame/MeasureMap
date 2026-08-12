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
            //
            // Bind the method into a compiled open delegate once instead of invoking
            // it through reflection on every run. MethodInfo.Invoke has a large lazy
            // one-time cost that would otherwise be charged to the first benchmark.
            var invoker = (Action<T, IExecutionContext>)Delegate.CreateDelegate(typeof(Action<T, IExecutionContext>), method);
            return obj => new ContextTask(ctx => invoker(obj, ctx));
        }

        var action = (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), method);
        return obj => new Task(() => action(obj));
    }
}