using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;

namespace MeasureMap.UnitTest
{
    [TestFixture]
    public class TraceTests
    {
        [Test]
        public void ProfilerResultCollection_Trace()
        {
            var sha256 = SHA256.Create();
            var md5 = MD5.Create();

            var data = new byte[10000];
            new Random(42).NextBytes(data);

            var runner = new BenchmarkRunner();
            runner.SetIterations(10);
            runner.Task("sha256", () => sha256.ComputeHash(data));
            runner.Task("Md5", () => md5.ComputeHash(data));

            var result = runner.RunSessions();

            result.Trace();
        }
    }
}
