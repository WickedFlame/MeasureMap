using System;
using System.Linq;
using System.Reflection;

namespace MeasureMap.Attributes.Builder;

/// <summary>
/// Builder element for <see cref="OnEndPipelineAttribute"/>
/// </summary>
public class OnEndPipelineBuilderElement : IBenchmarkBuilderElement
{
    private Action _action;
    
    /// <summary>
    /// Initialize the builder element
    /// </summary>
    /// <param name="instance"></param>
    /// <typeparam name="T"></typeparam>
    public void Initialize<T>(T instance)
    {
        var tmp = typeof(T).GetMethods()
            .FirstOrDefault(m => m.GetCustomAttribute<OnEndPipelineAttribute>() != null);
        _action = tmp != null ? () => tmp.Invoke(instance, null) : null;
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
        if (_action == null)
        {
            return;
        }
        
        session.OnEndPipeline(e => _action.Invoke());
    }
}