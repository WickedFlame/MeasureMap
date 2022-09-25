using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using MeasureMap.Tracers;
using MeasureMap.UnitTest.Tracers;
using NUnit.Framework;
using Polaroider;

namespace MeasureMap.UnitTest
{
    [SingleThreaded]
    public class TraceTests
    {
        [Test]
        public void BenchmarkResult_Trace()
        {
            var writer = new StringResultWriter();
            var options = new TraceOptions
            {
                ResultWriter = writer
            };

            var sha256 = SHA256.Create();
            var md5 = MD5.Create();

            var data = new byte[10000];
            new Random(42).NextBytes(data);

            var runner = new BenchmarkRunner();
            runner.SetIterations(10);
            runner.Task("sha256", () => sha256.ComputeHash(data));
            runner.Task("Md5", () => md5.ComputeHash(data));

            var result = runner.RunSessions();
            result.Trace(options);

#if NET6_0_OR_GREATER
            var output = writer.Value.Split("\r\n");
#else
            var output = writer.Value.Split(new[] { "\r\n" }, StringSplitOptions.None);
#endif
            output[0].MatchSnapshot(() => new {id = 0});
            output[1].MatchSnapshot(() => new { id = 1 });
            output[2].MatchSnapshot(() => new { id = 2 });
            output[3].MatchSnapshot(() => new { id = 3 });
            output[4].MatchSnapshot(() => new { id = 4 });
            output[5].Substring(0, 20).MatchSnapshot(() => new { id = 5 });
            output[6].Substring(0, 20).MatchSnapshot(() => new { id = 6 });
        }
    }
}
