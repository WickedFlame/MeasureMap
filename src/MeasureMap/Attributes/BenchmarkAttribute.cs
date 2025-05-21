using System;

namespace MeasureMap
{
    /// <summary>
    /// Attribute to mark a method as a benchmark
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class BenchmarkAttribute : Attribute
    {
    }
}
