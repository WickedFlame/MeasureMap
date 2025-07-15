using System;
using System.Reflection;

namespace MeasureMap.Attributes.Builder;

/// <summary>
/// Builder element for <see cref="IterationsAttribute"/>
/// </summary>
public class IterationsBuilderElement : IBenchmarkBuilderElement
{
    private Type _type;

    /// <summary>
    /// Initialize the builder element
    /// </summary>
    /// <param name="instance"></param>
    /// <typeparam name="T"></typeparam>
    public void Initialize<T>(T instance)
    {
        _type = typeof(T);
    }
    
    /// <summary>
    /// Append settings to the <see cref="BenchmarkRunner"/>
    /// </summary>
    /// <param name="runner"></param>
    public void Append(BenchmarkRunner runner)
    {
        var iterationAttr = _type.GetCustomAttribute<IterationsAttribute>();
        if (iterationAttr != null)
        {
            runner.SetIterations(iterationAttr.Iterations);
        }
    }

    /// <summary>
    /// Append settings to the <see cref="ProfilerSession"/>
    /// </summary>
    /// <param name="session"></param>
    public void Append(ProfilerSession session)
    {
    }
}