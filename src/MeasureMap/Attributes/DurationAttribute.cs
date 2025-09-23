using System;

namespace MeasureMap;

/// <summary>
/// Attribute to set the duration of the benchmark
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class DurationAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="duration"></param>
    public DurationAttribute(int duration)
    {
        Duration = duration;
    }

    /// <summary>
    /// Gets the duration
    /// </summary>
    public int Duration { get; }
}