using System.Text;

namespace MeasureMap.Tracers
{
    public class StringResultWriter : IResultWriter
    {
        private readonly StringBuilder _stringBuilder = new();

        public string Value => _stringBuilder.ToString();

        public void Write(string value)
        {
            _stringBuilder.Append(value);
        }

        public void WriteLine(string value)
        {
            _stringBuilder.AppendLine(value);
        }
    }
}
