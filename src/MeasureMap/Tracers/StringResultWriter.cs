using System.Text;

namespace MeasureMap.Tracers
{
    /// <summary>
    /// 
    /// </summary>
    public class StringResultWriter : IResultWriter
    {
        private readonly StringBuilder _stringBuilder = new();

        /// <summary>
        /// 
        /// </summary>
        public string Value => _stringBuilder.ToString();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void Write(string value)
        {
            _stringBuilder.Append(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void WriteLine(string value)
        {
            _stringBuilder.AppendLine(value);
        }
    }
}
