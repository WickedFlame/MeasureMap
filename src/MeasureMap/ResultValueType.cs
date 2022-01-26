
namespace MeasureMap
{
    public class ResultValueType : Enumeration
    {
        public static readonly ResultValueType Warmup = new ResultValueType(1, "Warmup");

        public static readonly ResultValueType Elapsed = new ResultValueType(2, "Elapsed");

        public ResultValueType(int id, string name) : base(id, name)
        {
        }
    }
}
