using System;

namespace MeasureMap;

/// <summary>
/// Attribute to mark a method that is called at each end of a thread
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class OnEndPipelineAttribute : Attribute
{
    
}