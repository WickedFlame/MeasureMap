using MeasureMap.ContextStack;
using System;
using System.Reflection;

namespace MeasureMap.Attributes.Builder;

/// <summary>
/// Builder element for <see cref="DurationAttribute"/>
/// </summary>
public class DurationBuilderElement : IBenchmarkBuilderElement
{
    private Type _type;

    /// <summary>
    /// Initialize the builder element
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void Initialize<T>()
    {
        _type = typeof(T);
    }
    
    /// <summary>
    /// Append elements to the <see cref="BenchmarkRunner"/>
    /// </summary>
    /// <param name="runner"></param>
    public void Append(BenchmarkRunner runner)
    {
        var durationAttr = _type.GetCustomAttribute<DurationAttribute>();
        if (durationAttr != null)
        {
            runner.SetDuration(TimeSpan.FromSeconds(durationAttr.Duration));
        }
    }
    
    /// <summary>
    /// Append elements to the <see cref="ProfilerSession"/>
    /// </summary>
    /// <param name="session"></param>
    public void Append(ProfilerSession session)
    {
    }

    /// <summary>
    /// Append elements to the <see cref="IContextStackBuilder"/>
    /// </summary>
    /// <param name="stackBuilder"></param>
    public void Append<T>(AttriuteBasedStackBuilder<T> stackBuilder) where T : class, new()
    {
    }
}