using System;

namespace MeasureMap;

/// <summary>
/// Attribute to define if a warmup is run
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class RunWarmupAttribute : Attribute
{
    public RunWarmupAttribute(bool run = true)
    {
        RunWarmup = run;
    }

    public bool RunWarmup { get; }
}