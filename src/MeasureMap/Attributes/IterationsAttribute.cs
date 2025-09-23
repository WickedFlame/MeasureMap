using System;

namespace MeasureMap;


/// <summary>
/// Attribute to set the duration of the benchmark
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class IterationsAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="iterations"></param>
    public IterationsAttribute(int iterations)
    {
        Iterations = iterations;
    }

    /// <summary>
    /// Gets the amount of iterations
    /// </summary>
    public int Iterations { get; }
}