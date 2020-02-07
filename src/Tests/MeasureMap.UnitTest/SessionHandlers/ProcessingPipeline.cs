using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MeasureMap.UnitTest.SessionHandlers
{
    public class ProcessingPipeline
    {
        [Test]
        public void AddMiddleware()
        {
            int calls = 0;

            ProfilerSession.StartSession()
                .AddMiddleware(new CustomMiddleware(() => calls++))
                .Task(() => calls++)
                .RunSession();

            // 2 x CustomMiddleware on warmup
            // 1 x task on warmup
            // 2 x CustomMiddleware on execution
            // 1 x task on execution
            Assert.AreEqual(6, calls);
        }

        public class CustomMiddleware : ITaskMiddleware
        {
            private readonly Action _delegate;
            private ITask _next;

            public CustomMiddleware(Action delega)
            {
                _delegate = delega;
            }
            public IIterationResult Run(IExecutionContext context)
            {
                _delegate();
                var result = _next.Run(context);
                _delegate();

                return result;
            }

            public void SetNext(ITask next)
            {
                _next = next;
            }
        }
    }
}
