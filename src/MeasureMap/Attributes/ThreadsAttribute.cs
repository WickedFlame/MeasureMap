using System;

namespace MeasureMap;

/// <summary>
/// Attribute to define the amount of threads the benchmark is run on
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ThreadsAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="threads"></param>
    public ThreadsAttribute(int threads)
    {
        Threads = threads;
    }

    /// <summary>
    /// Gets the amount of threads to run the tests on
    /// </summary>
    public int Threads { get; }
}