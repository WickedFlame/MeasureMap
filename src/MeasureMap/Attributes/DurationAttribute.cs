using System;

namespace MeasureMap;

/// <summary>
/// Attribute to set the duration of the benchmark
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class DurationAttribute : Attribute
{
    public DurationAttribute(int duration)
    {
        Duration = duration;
    }

    public int Duration { get; }
}