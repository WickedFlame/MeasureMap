
using MeasureMap;

namespace Measuremap.IntegrationTest
{
    public class BdContext
    {
        public Dictionary<string, object> Context = new Dictionary<string, object>();
        public ProfilerSession Session { get; set; }
        public IProfilerResult Result { get; set; }

        public void Wait()
        {
            System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(5)).Wait();
        }
    }
}
