// See https://aka.ms/new-console-template for more information

using MeasureMap;
using MeasureMap.Tracers;

var benchmark = new BenchmarkRunner();
benchmark.SetIterations(10);
benchmark.Task("Setup 1 Thread", () =>
{
    ProfilerSession.StartSession()
        .SetThreads(1)
        .Task(c2 =>
        {
            // do nothing
        })
        .RunSession();
}).SetThreads(10);

benchmark.Task("Setup 10 Threads", () =>
{
    ProfilerSession.StartSession()
        .SetThreads(1)
        .Task(c2 =>
        {
            // do nothing
        })
        .RunSession();
}).SetThreads(10);


var options = new TraceOptions();
options.Metrics = new MeasureMap.Benchmark.TraceMetrics();

benchmark.RunSessions()
    .Trace(options);


