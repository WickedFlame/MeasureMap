using System;
using System.Reflection;

namespace MeasureMap.Attributes.Builder;

/// <summary>
/// Builder element for <see cref="ThreadsAttribute"/>
/// </summary>
public class ThreadsBuilderElement : IBenchmarkBuilderElement
{
    private int _threads;

    /// <summary>
    /// Initialize the builder element
    /// </summary>
    /// <param name="instance"></param>
    /// <typeparam name="T"></typeparam>
    public void Initialize<T>(T instance)
    {
        var threads = typeof(T).GetCustomAttribute<ThreadsAttribute>();
        if (threads != null)
        {
            _threads = threads.Threads;
        }
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
        if (_threads > 0)
        {
            session.SetThreads(_threads);
        }
    }
}