
namespace MeasureMap.Tracers
{
    /// <summary>
    /// Writes the result to a desired output
    /// </summary>
    public interface IResultWriter
    {
        /// <summary>
        /// Write the value to the output
        /// </summary>
        /// <param name="value"></param>
        void Write(string value);

        /// <summary>
        /// Write the value to the output as a single line
        /// </summary>
        /// <param name="value"></param>
        void WriteLine(string value);
    }
}
