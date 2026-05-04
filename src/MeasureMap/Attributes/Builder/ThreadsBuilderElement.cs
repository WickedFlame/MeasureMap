using MeasureMap.ContextStack;
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
    /// <typeparam name="T"></typeparam>
    public void Initialize<T>()
    {
        var threads = typeof(T).GetCustomAttribute<ThreadsAttribute>();
        if (threads != null)
        {
            _threads = threads.Threads;
        }
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
        if (_threads > 0)
        {
            session.SetThreads(_threads);
        }
    }

    /// <summary>
    /// Append elements to the <see cref="IContextStackBuilder"/>
    /// </summary>
    /// <param name="stackBuilder"></param>
    public void Append<T>(AttriuteBasedStackBuilder<T> stackBuilder) where T : class, new()
    {
    }
}