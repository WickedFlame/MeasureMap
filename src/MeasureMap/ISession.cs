namespace MeasureMap
{
    /// <summary>
    /// Represents a Profile Session
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// Start profiling the session
        /// </summary>
        /// <returns>The result of the profile</returns>
        ProfileResult RunSession();
    }
}
