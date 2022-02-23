using System;

namespace MeasureMap
{
    /// <summary>
    /// Exception that is thrown when a condition is not met
    /// </summary>
#pragma warning disable S3925 // "ISerializable" should be implemented correctly
    public class AssertionException : Exception
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
    {
        /// <summary>
        /// Exception that is thrown when a condition is not met
        /// </summary>
        /// <param name="message">The message of the exception</param>
        public AssertionException(string message)
            : base(message)
        {
        }
    }
}
