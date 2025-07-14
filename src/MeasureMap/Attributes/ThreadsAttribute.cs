using System;

namespace MeasureMap;

/// <summary>
/// Attribute to define the amount of threads the benchmark is run on
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ThreadsAttribute : Attribute
{
    public ThreadsAttribute(int threads)
    {
        Threads = threads;
    }

    public int Threads { get; }
}