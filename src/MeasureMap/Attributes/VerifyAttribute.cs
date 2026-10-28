using System;

namespace MeasureMap
{
    /// <summary>
    /// Verify the Benchmarkresult against the saved baseline result.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class VerifyAttribute : Attribute
    {
    }
}
