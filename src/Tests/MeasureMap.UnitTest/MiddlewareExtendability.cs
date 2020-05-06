using System.Diagnostics;
using NUnit.Framework;

namespace MeasureMap.UnitTest.SessionHandlers
{
    [TestFixture]
    public class MiddlewareExtendability
    {
        [Test]
        public void MiddlewareExtendability_ExtendSessionHandler()
        {
            var handler = new Handler();

            var session = ProfilerSession.StartSession()
                .Task(() => Debug.WriteLine("AddCustomSessionHandler call"))
                .SetIterations(100);
            
            session.SessionPipeline.SetNext(handler);

            session.RunSession();

            // handler is called once
            Assert.That(handler.Calls == 1);
        }

        [Test]
        public void MiddlewareExtendability_AddSessionHandler()
        {
            var handler = new Handler();

            ProfilerSession.StartSession()
                .AddMiddleware(handler)
                .Task(() => Debug.WriteLine("AddCustomSessionHandler call"))
                .SetIterations(100)
                .RunSession();

            // handler is called once
            Assert.That(handler.Calls == 1);
        }

        public class Handler : SessionHandler
        {
            public int Calls { get; private set; }

            public override IProfilerResult Execute(ITask task, int iterations)
            {
                Calls = Calls + 1;
                return base.Execute(task, iterations);
            }
        }
    }
}
