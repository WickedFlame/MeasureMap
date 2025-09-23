using System;

namespace MeasureMap;

/// <summary>
/// Attribute to define if a warmup is run
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class RunWarmupAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="run"></param>
    public RunWarmupAttribute(bool run = true)
    {
        RunWarmup = run;
    }

    /// <summary>
    /// Gets if the warmup is run or not
    /// </summary>
    public bool RunWarmup { get; }
}