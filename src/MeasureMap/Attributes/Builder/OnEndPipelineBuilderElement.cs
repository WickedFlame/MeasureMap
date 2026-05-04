using MeasureMap.ContextStack;
using System;
using System.Linq;
using System.Reflection;
using static System.Collections.Specialized.BitVector32;

namespace MeasureMap.Attributes.Builder;

/// <summary>
/// Builder element for <see cref="OnEndPipelineAttribute"/>
/// </summary>
public class OnEndPipelineBuilderElement : IBenchmarkBuilderElement
{
    private MethodInfo _method;

    /// <summary>
    /// Initialize the builder element
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void Initialize<T>()
    {
        _method = typeof(T).GetMethods()
            .FirstOrDefault(m => m.GetCustomAttribute<OnEndPipelineAttribute>() != null);
        
    }
    
    /// <summary>
    /// Append settings to the <see cref="BenchmarkRunner"/>
    /// </summary>
    /// <param name="runner"></param>
    public void Append(BenchmarkRunner runner)
    {
    }

    /// <summary>
    /// Append settings to the <see cref="ProfilerSession"/>
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
        if(_method == null)
        {
            return;
        }

        stackBuilder.Add((instance, i, s) => new OnEndPipelineContextHandler((e) => _method.Invoke(instance, null)));
    }
}