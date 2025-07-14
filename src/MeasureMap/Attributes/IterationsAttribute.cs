using System;

namespace MeasureMap;


/// <summary>
/// Attribute to set the duration of the benchmark
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class IterationsAttribute : Attribute
{
    public IterationsAttribute(int iterations)
    {
        Iterations = iterations;
    }

    public int Iterations { get; }
}