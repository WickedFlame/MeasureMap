using Abstract.FileSystem;

namespace MeasureMap.IntegrationTest.AttributeBasedTests
{
    public class VerifyAttributeTests
    {
        [TearDown]
        [SetUp]
        public void TearDown()
        {
            if(System.IO.File.Exists("..\\..\\..\\AttributeBasedTests\\VerifyAttributeTests.benchmark.yml".ToSystemPath()))
            {
                System.IO.File.Delete("..\\..\\..\\AttributeBasedTests\\VerifyAttributeTests.benchmark.yml".ToSystemPath());
            }
        }

        [Test]
        public void VerifyAttribute_Benchmark()
        {
            var runner = new BenchmarkRunner();
            runner.RunSession<VerifyAttributeClass>();

            System.IO.File.Exists("..\\..\\..\\AttributeBasedTests\\VerifyAttributeTests.benchmark.yml".ToSystemPath()).Should().BeTrue();
        }
    }

    [Verify]
    public class VerifyAttributeClass
    {
        [Benchmark]
        public void Test_1()
        {
            // Simulate some work
            System.Threading.Thread.Sleep(10);
        }

        [Benchmark]
        public void Test_2()
        {
            // Simulate some work
            System.Threading.Thread.Sleep(10);
        }
    }
}
