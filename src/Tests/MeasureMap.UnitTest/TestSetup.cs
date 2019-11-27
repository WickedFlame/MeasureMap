using MeasureMap.Diagnostics;
using NUnit.Framework;

namespace MeasureMap.UnitTest
{
    [SetUpFixture]
    public class TestSetup
    {
        [OneTimeSetUp]
        public void OnetimeSetup()
        {
            GlobalConfiguration.AddLogWriter(new TraceLogWriter());
        }
    }
}
