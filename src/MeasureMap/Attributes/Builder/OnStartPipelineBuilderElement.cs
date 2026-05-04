using MeasureMap.ContextStack;
using System.Linq;
using System.Reflection;

namespace MeasureMap.Attributes.Builder;

/// <summary>
/// Builder element for <see cref="OnStartPipelineAttribute"/>
/// </summary>
public class OnStartPipelineBuilderElement : IBenchmarkBuilderElement
{
    private MethodInfo _method;

    /// <summary>
    /// Initialize the builder element
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void Initialize<T>()
    {
        _method = typeof(T).GetMethods()
            .FirstOrDefault(m => m.GetCustomAttribute<OnStartPipelineAttribute>() != null);
    }

    /// <summary>
    /// Append elements to the <see cref="BenchmarkRunner"/>
    /// </summary>
    /// <param name="runner"></param>
    public void Append(BenchmarkRunner runner)
    {
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
        if (_method == null)
        {
            return;
        }

        stackBuilder.Add((instance, i, s) => new OnStartPipelineContextHandler(i, s, (e) =>
        {
            var param = _method.GetParameters().Any(p => p.ParameterType == typeof(ProfilerSettings)) ? new object[] { s } : null;
            var context = _method.Invoke(instance, param) as IExecutionContext;
            return context ?? e.CreateContext();
        }));
    }
}