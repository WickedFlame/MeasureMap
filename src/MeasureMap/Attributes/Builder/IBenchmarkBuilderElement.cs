using System;

namespace MeasureMap.Attributes.Builder;

/// <summary>
/// 
/// </summary>
public interface IBenchmarkBuilderElement
{
    /// <summary>
    /// Initialize the builder element
    /// </summary>
    /// <param name="instance"></param>
    /// <typeparam name="T"></typeparam>
    void Initialize<T>(T instance);
    
    /// <summary>
    /// Append settings to the <see cref="BenchmarkRunner"/>
    /// </summary>
    /// <param name="runner"></param>
    void Append(BenchmarkRunner runner);

    /// <summary>
    /// Append settings to the <see cref="ProfilerSession"/>
    /// </summary>
    /// <param name="session"></param>
    void Append(ProfilerSession session);
}