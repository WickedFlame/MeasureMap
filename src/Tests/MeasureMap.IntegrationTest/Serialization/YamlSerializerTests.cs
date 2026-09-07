using System.Globalization;
using System.IO;
using MeasureMap.Serialization;

namespace MeasureMap.IntegrationTest.Serialization
{
    [TestFixture]
    [Category("Integration")]
    public class YamlSerializerTests
    {
        private static BenchmarkModel CreateModel()
        {
            return new BenchmarkModel
            {
                Iterations = 10,
                Duration = TimeSpan.FromSeconds(5),
                Benchmarks =
                {
                    new Benchmark
                    {
                        Name = "First",
                        ThreadId = 1,
                        ThreadNumber = 1,
                        AverageTicks = 1234,
                        AverageMilliseconds = 0.1234,
                        AverageTime = TimeSpan.FromMilliseconds(1),
                        TotalTime = TimeSpan.FromMilliseconds(10),
                        Fastest = TimeSpan.FromMilliseconds(1),
                        Slowest = TimeSpan.FromMilliseconds(2),
                        Increase = 100,
                        InitialSize = 1000,
                        EndSize = 1100
                    }
                }
            };
        }

        [Test]
        public void YamlSerializer_Serialize_Model_ContainsHeader()
        {
            var yaml = new YamlSerializer().Serialize(CreateModel());

            yaml.Should().Contain("iterations: 10");
            yaml.Should().Contain("benchmarks:");
        }

        [Test]
        public void YamlSerializer_Serialize_Model_ContainsBenchmarkAsListItem()
        {
            var yaml = new YamlSerializer().Serialize(CreateModel());

            yaml.Should().Contain("- name: First");
            yaml.Should().Contain("    threadId: 1");
            yaml.Should().Contain("    increase: 100");
            yaml.Should().Contain("    initialSize: 1000");
            yaml.Should().Contain("    endSize: 1100");
        }

        [Test]
        public void YamlSerializer_Serialize_Model_UsesInvariantNumberFormat()
        {
            var yaml = new YamlSerializer().Serialize(CreateModel());

            yaml.Should().Contain("averageMilliseconds: 0.1234");
        }

        [Test]
        public void YamlSerializer_Serialize_Model_MultipleBenchmarks()
        {
            var model = CreateModel();
            model.Benchmarks.Add(new Benchmark { Name = "Second" });

            var yaml = new YamlSerializer().Serialize(model);

            yaml.Should().Contain("- name: First");
            yaml.Should().Contain("- name: Second");
        }

        [Test]
        public void YamlSerializer_Serialize_Model_NoBenchmarks()
        {
            var model = new BenchmarkModel
            {
                Iterations = 3,
                Duration = TimeSpan.Zero
            };

            var yaml = new YamlSerializer().Serialize(model);

            yaml.Should().Contain("iterations: 3");
            yaml.Should().Contain("benchmarks:");
            yaml.Should().NotContain("- name:");
        }

        [Test]
        public void YamlSerializer_Serialize_Result_WritesFile()
        {
            var result = new BenchmarkRunner()
                .AddSession("SessionA", ProfilerSession.StartSession().Task(c => { }).SetIterations(5))
                .AddSession("SessionB", ProfilerSession.StartSession().Task(c => { }).SetIterations(5))
                .RunSessions();

            var fileName = Path.Combine(Path.GetTempPath(), $"measuremap_{Guid.NewGuid():N}.yaml");

            try
            {
                new YamlSerializer().Serialize(result, fileName);

                File.Exists(fileName).Should().BeTrue();

                var yaml = File.ReadAllText(fileName);
                yaml.Should().Contain("benchmarks:");
                yaml.Should().Contain("- name: SessionA");
                yaml.Should().Contain("- name: SessionB");
            }
            finally
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
        }

        [Test]
        public void YamlSerializer_DeserializeContent_Header()
        {
            var yaml = new YamlSerializer().Serialize(CreateModel());

            var model = new YamlSerializer().DeserializeContent(yaml);

            model.Iterations.Should().Be(10);
            model.Duration.Should().Be(TimeSpan.FromSeconds(5));
        }

        [Test]
        public void YamlSerializer_DeserializeContent_Benchmark()
        {
            var yaml = new YamlSerializer().Serialize(CreateModel());

            var model = new YamlSerializer().DeserializeContent(yaml);

            model.Benchmarks.Should().HaveCount(1);
            var benchmark = model.Benchmarks.Single();
            benchmark.Name.Should().Be("First");
            benchmark.ThreadId.Should().Be(1);
            benchmark.ThreadNumber.Should().Be(1);
            benchmark.AverageTicks.Should().Be(1234);
            benchmark.AverageMilliseconds.Should().Be(0.1234);
            benchmark.AverageTime.Should().Be(TimeSpan.FromMilliseconds(1));
            benchmark.TotalTime.Should().Be(TimeSpan.FromMilliseconds(10));
            benchmark.Fastest.Should().Be(TimeSpan.FromMilliseconds(1));
            benchmark.Slowest.Should().Be(TimeSpan.FromMilliseconds(2));
            benchmark.Increase.Should().Be(100);
            benchmark.InitialSize.Should().Be(1000);
            benchmark.EndSize.Should().Be(1100);
        }

        [Test]
        public void YamlSerializer_DeserializeContent_MultipleBenchmarks()
        {
            var source = CreateModel();
            source.Benchmarks.Add(new Benchmark { Name = "Second", ThreadId = 2 });

            var yaml = new YamlSerializer().Serialize(source);
            var model = new YamlSerializer().DeserializeContent(yaml);

            model.Benchmarks.Select(b => b.Name).Should().Equal("First", "Second");
            model.Benchmarks.Last().ThreadId.Should().Be(2);
        }

        [Test]
        public void YamlSerializer_DeserializeContent_Empty()
        {
            var model = new YamlSerializer().DeserializeContent(string.Empty);

            model.Should().NotBeNull();
            model.Benchmarks.Should().BeEmpty();
        }

        [Test]
        public void YamlSerializer_Serialize_Deserialize_RoundTrip()
        {
            var source = CreateModel();

            var yaml = new YamlSerializer().Serialize(source);
            var model = new YamlSerializer().DeserializeContent(yaml);

            model.Iterations.Should().Be(source.Iterations);
            model.Duration.Should().Be(source.Duration);
            model.Benchmarks.Should().HaveCount(source.Benchmarks.Count);
            model.Benchmarks[0].Name.Should().Be(source.Benchmarks[0].Name);
            model.Benchmarks[0].EndSize.Should().Be(source.Benchmarks[0].EndSize);
        }

        [Test]
        public void YamlSerializer_Deserialize_FromFile()
        {
            var source = CreateModel();
            var fileName = Path.Combine(Path.GetTempPath(), $"measuremap_{Guid.NewGuid():N}.yaml");

            try
            {
                File.WriteAllText(fileName, new YamlSerializer().Serialize(source));

                var model = new YamlSerializer().Deserialize(fileName);

                model.Iterations.Should().Be(source.Iterations);
                model.Benchmarks.Single().Name.Should().Be("First");
            }
            finally
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
        }
    }
}
