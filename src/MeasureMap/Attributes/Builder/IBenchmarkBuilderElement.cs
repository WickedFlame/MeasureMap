using MeasureMap.ContextStack;

namespace MeasureMap.Attributes.Builder;

/// <summary>
/// 
/// </summary>
public interface IBenchmarkBuilderElement
{
    /// <summary>
    /// Initialize the builder element
    /// </summary>
    /// <typeparam name="T"></typeparam>
    void Initialize<T>();
    
    /// <summary>
    /// Append elements to the <see cref="BenchmarkRunner"/>
    /// </summary>
    /// <param name="runner"></param>
    void Append(BenchmarkRunner runner);

    /// <summary>
    /// Append elements to the <see cref="ProfilerSession"/>
    /// </summary>
    /// <param name="session"></param>
    void Append(ProfilerSession session);

    /// <summary>
    /// Append elements to the <see cref="IContextStackBuilder"/>
    /// </summary>
    /// <param name="stackBuilder"></param>
    void Append<T>(AttriuteBasedStackBuilder<T> stackBuilder) where T : class, new();
}