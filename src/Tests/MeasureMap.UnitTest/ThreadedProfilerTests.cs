using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasureMap.UnitTest
{
    [TestFixture]
    public class ThreadedProfilerTests
    {
        [Test]
        public void ThreadedProfiler_NoThread()
        {
            var result = ProfilerSession.StartSession()
                .Task(c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    Trace.WriteLine($"Iteration {(int)i}");
                })
                .SetIterations(10)
                .RunSession();

            Assert.IsInstanceOf<ProfilerResult>(result);

            Assert.That(((ProfilerResult)result).Count() == 1);
            Assert.That(result.Iterations.Count() == 10);
        }

        [Test]
        public void ThreadedProfiler_OneThread()
        {
            var result = ProfilerSession.StartSession()
                .Task(c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    Trace.WriteLine($"Iteration {(int)i}");
                })
                .SetIterations(10)
                .SetThreads(1)
                .RunSession();

            Assert.That(((ProfilerResult)result).Count() == 1);
            Assert.That(result.Iterations.Count() == 10);
        }

        [Test]
        public void ThreadedProfiler_MultipleThreads()
        {
            var result = ProfilerSession.StartSession()
                .Task(c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    Trace.WriteLine($"Iteration {(int)i}");
                })
                .SetIterations(10)
                .SetThreads(10)
                .RunSession();

            Assert.That(((ProfilerResult)result).Count() == 10);
            Assert.That(result.Iterations.Count() == 100);
        }

        [Test]
        public void ThreadedProfiler_MultipleThreads_ReturnValues()
        {
            var result = ProfilerSession.StartSession()
                .Task(c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    Trace.WriteLine($"Iteration {(int)i}");
                })
                .SetIterations(10)
                .SetThreads(10)
                .RunSession();

            Assert.That(((ProfilerResult)result).All(r => r.AverageTime.Ticks > 0), () => "AverageTime");
            Assert.That(((ProfilerResult)result).All(r => r.EndSize > 0), () => "EndSize");
            Assert.That(((ProfilerResult)result).All(r => r.AverageTicks > 0), () => "AverageTicks");
            // milliseconds can be 0 when ticks are too low
            //Assert.That(((ProfilerResultCollection)result).Any(r => r.AverageMilliseconds != 0), () => "AverageMilliseconds");
            Assert.That(((ProfilerResult)result).All(r => r.Fastest != null), () => "Fastest");
            Assert.That(((ProfilerResult)result).All(r => r.Increase != 0), () => "Increase");
            Assert.That(((ProfilerResult)result).All(r => r.InitialSize > 0), () => "InitialSize");
            Assert.That(((ProfilerResult)result).All(r => r.TotalTime.Ticks > 0), () => "TotalTime.Ticks");
        }


        [Test]
        public void ThreadedProfiler_MultipleThreads_Interval_Duration()
        {
            var sw = new Stopwatch();
            sw.Start();

            var result = ProfilerSession.StartSession()
                .Task(c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    Trace.WriteLine($"Iteration {i}");
                })
                .SetDuration(TimeSpan.FromSeconds(20))
                .SetInterval(TimeSpan.FromMilliseconds(50))
                .SetThreads(10)
                .RunSession();


            sw.Stop();

            result.Trace();

            result.Elapsed().Should().BeGreaterThan(TimeSpan.FromSeconds(20)).And.BeLessThan(sw.Elapsed);

            /*
            ### MeasureMap - Profiler result for Profilesession
            #### Summary
	            Warmup ========================================
		            Duration Warmup:			00:00:00.0183942
	            Setup ========================================
		            Threads:			10
		            Iterations:			1129
	            Duration ========================================
		            Duration:			00:00:25.1914746
		            Total Time:			00:01:01.4940694
		            Average Time:			00:00:00.0544677
		            Average Milliseconds:		53
		            Average Ticks:			544677
		            Fastest:			00:00:00.0022682
		            Slowest:			00:00:05.2171468
	            Memory ==========================================
		            Memory Initial size:		1585784
		            Memory End size:		1684312
		            Memory Increase:		98528

            #### Details per Thread
            | ThreadId | Iterations | Average time | Slowest | Fastest |
            | --- | --- | ---: | ---: | ---: |
            | 10 | 194 | 00:00:00.0138437 | 00:00:00.0604912 | 00:00:00.0029758 |
            | 11 | 173 | 00:00:00.0191987 | 00:00:00.0870298 | 00:00:00.0032143 |
            | 17 | 138 | 00:00:00.0300923 | 00:00:00.2621003 | 00:00:00.0058133 |
            | 18 | 119 | 00:00:00.0597062 | 00:00:00.3090188 | 00:00:00.0032996 |
            | 16 | 127 | 00:00:00.0422762 | 00:00:00.3011997 | 00:00:00.0045766 |
            | 20 | 85 | 00:00:00.1109262 | 00:00:00.3315440 | 00:00:00.0038052 |
            | 19 | 99 | 00:00:00.0923007 | 00:00:00.3204989 | 00:00:00.0028416 |
            | 21 | 68 | 00:00:00.1224101 | 00:00:00.3372373 | 00:00:00.0029042 |
            | 22 | 58 | 00:00:00.1047994 | 00:00:00.4461727 | 00:00:00.0026986 |
            | 23 | 68 | 00:00:00.0866374 | 00:00:05.2171468 | 00:00:00.0022682 |
            */
        }

        [Test]
        public void ThreadedProfiler_MultipleThreads_Interval_Iterations()
        {
            var sw = new Stopwatch();
            sw.Start();

            var result = ProfilerSession.StartSession()
                .Task(c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    Trace.WriteLine($"Iteration {i}");
                })
                .SetIterations(50)
                .SetInterval(TimeSpan.FromMilliseconds(50))
                .SetThreads(10)
                .RunSession();

            sw.Stop();

            result.Trace();

            result.Elapsed().Should().BeLessThan(sw.Elapsed);

            /*
            ### MeasureMap - Profiler result for Profilesession
            #### Summary
	            Warmup ========================================
		            Duration Warmup:			00:00:00.0170398
	            Setup ========================================
		            Threads:			10
		            Iterations:			500
	            Duration ========================================
		            Duration:			00:00:07.6054371
		            Total Time:			00:00:12.1014659
		            Average Time:			00:00:00.0242029
		            Average Milliseconds:		23
		            Average Ticks:			242029
		            Fastest:			00:00:00.0022626
		            Slowest:			00:00:00.2318987
	            Memory ==========================================
		            Memory Initial size:		1592056
		            Memory End size:		1629092
		            Memory Increase:		37036

            #### Details per Thread
            | ThreadId | Iterations | Average time | Slowest | Fastest |
            | --- | --- | ---: | ---: | ---: |
            | 10 | 50 | 00:00:00.0107312 | 00:00:00.0408709 | 00:00:00.0029143 |
            | 11 | 50 | 00:00:00.0127949 | 00:00:00.0277547 | 00:00:00.0033066 |
            | 16 | 50 | 00:00:00.0177724 | 00:00:00.0464626 | 00:00:00.0025592 |
            | 20 | 50 | 00:00:00.0505545 | 00:00:00.1462360 | 00:00:00.0035389 |
            | 17 | 50 | 00:00:00.0364123 | 00:00:00.1176959 | 00:00:00.0029823 |
            | 19 | 50 | 00:00:00.0452512 | 00:00:00.1173062 | 00:00:00.0040458 |
            | 18 | 50 | 00:00:00.0280446 | 00:00:00.0750011 | 00:00:00.0037804 |
            | 10 | 50 | 00:00:00.0139704 | 00:00:00.2229528 | 00:00:00.0026401 |
            | 16 | 50 | 00:00:00.0162519 | 00:00:00.2318987 | 00:00:00.0022626 |
            | 11 | 50 | 00:00:00.0102454 | 00:00:00.0660816 | 00:00:00.0025892 |
            */
        }






        [Test]
        public void ThreadedProfiler_MultipleThreads_Simple_Duration()
        {
            var sw = new Stopwatch();
            sw.Start();

            var result = ProfilerSession.StartSession()
                .Task(c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    Trace.WriteLine($"Iteration {i}");
                })
                .SetDuration(TimeSpan.FromSeconds(20))
                .SetThreads(10)
                .RunSession();


            sw.Stop();

            result.Trace();

            result.Elapsed().Should().BeGreaterThan(TimeSpan.FromSeconds(20)).And.BeLessThan(sw.Elapsed);

            /*
            ### MeasureMap - Profiler result for Profilesession
            #### Summary
	            Warmup ========================================
		            Duration Warmup:			00:00:00.0189054
	            Setup ========================================
		            Threads:			10
		            Iterations:			2268
	            Duration ========================================
		            Duration:			00:00:41.3471002
		            Total Time:			00:00:55.2762125
		            Average Time:			00:00:00.0243722
		            Average Milliseconds:		23
		            Average Ticks:			243722
		            Fastest:			00:00:00.0024162
		            Slowest:			00:00:06.3143534
	            Memory ==========================================
		            Memory Initial size:		1596528
		            Memory End size:		1785172
		            Memory Increase:		188644

            #### Details per Thread
            | ThreadId | Iterations | Average time | Slowest | Fastest |
            | --- | --- | ---: | ---: | ---: |
            | 11 | 350 | 00:00:00.0130281 | 00:00:00.0821559 | 00:00:00.0024162 |
            | 9 | 412 | 00:00:00.0105151 | 00:00:00.0238863 | 00:00:00.0034361 |
            | 17 | 50 | 00:00:00.1314544 | 00:00:01.4934925 | 00:00:00.0052489 |
            | 19 | 1 | 00:00:06.3143534 | 00:00:06.3143534 | 00:00:06.3143534 |
            | 16 | 262 | 00:00:00.0178478 | 00:00:00.0435406 | 00:00:00.0034318 |
            | 20 | 405 | 00:00:00.0115145 | 00:00:00.2712846 | 00:00:00.0028629 |
            | 18 | 93 | 00:00:00.0721457 | 00:00:00.4774589 | 00:00:00.0037850 |
            | 21 | 345 | 00:00:00.0137611 | 00:00:00.0518304 | 00:00:00.0039876 |
            | 22 | 239 | 00:00:00.0252535 | 00:00:00.2743825 | 00:00:00.0026940 |
            | 23 | 111 | 00:00:00.0600429 | 00:00:01.1319821 | 00:00:00.0024369 |
            */
        }

        [Test]
        public void ThreadedProfiler_MultipleThreads_Simple_Iterations()
        {
            var sw = new Stopwatch();
            sw.Start();

            var result = ProfilerSession.StartSession()
                .Task(c =>
                {
                    var i = c.Get<int>(ContextKeys.Iteration);
                    Trace.WriteLine($"Iteration {i}");
                })
                .SetIterations(50)
                .SetThreads(10)
                .RunSession();

            sw.Stop();

            result.Trace();

            result.Elapsed().Should().BeLessThan(sw.Elapsed);

            /*
            ### MeasureMap - Profiler result for Profilesession
            #### Summary
	            Warmup ========================================
		            Duration Warmup:			00:00:00.0191410
	            Setup ========================================
		            Threads:			10
		            Iterations:			500
	            Duration ========================================
		            Duration:			00:00:08.4436926
		            Total Time:			00:00:09.2145654
		            Average Time:			00:00:00.0184291
		            Average Milliseconds:		17
		            Average Ticks:			184291
		            Fastest:			00:00:00.0021651
		            Slowest:			00:00:00.6672119
	            Memory ==========================================
		            Memory Initial size:		1596900
		            Memory End size:		1639636
		            Memory Increase:		42736

            #### Details per Thread
            | ThreadId | Iterations | Average time | Slowest | Fastest |
            | --- | --- | ---: | ---: | ---: |
            | 10 | 50 | 00:00:00.0106889 | 00:00:00.1027457 | 00:00:00.0026854 |
            | 11 | 50 | 00:00:00.0122346 | 00:00:00.0341723 | 00:00:00.0054592 |
            | 16 | 50 | 00:00:00.0171771 | 00:00:00.0393153 | 00:00:00.0067055 |
            | 19 | 50 | 00:00:00.0153481 | 00:00:00.1648672 | 00:00:00.0024235 |
            | 20 | 50 | 00:00:00.0187932 | 00:00:00.3237942 | 00:00:00.0035926 |
            | 17 | 50 | 00:00:00.0410966 | 00:00:00.6672119 | 00:00:00.0061538 |
            | 18 | 50 | 00:00:00.0309728 | 00:00:00.3929003 | 00:00:00.0031255 |
            | 21 | 50 | 00:00:00.0171989 | 00:00:00.1665635 | 00:00:00.0021651 |
            | 10 | 50 | 00:00:00.0101438 | 00:00:00.0211847 | 00:00:00.0046102 |
            | 11 | 50 | 00:00:00.0106368 | 00:00:00.0214394 | 00:00:00.0028004 |
            */
        }
    }
}
