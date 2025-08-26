using MeasureMap.ContextStack;

namespace MeasureMap.SessionStack
{
    /// <summary>
    /// Warmup for the task
    /// </summary>
    public interface IWarmupSessionHandler : ISessionMiddleware
    {
        /// <summary>
        /// Gets or sets the <see cref="IContextStackBuilder"/> to create the ContextStack that runs the task
        /// </summary>
        IContextStackBuilder StackBuilder { get; }
    }
}
