using MeasureMap.Attributes.Builder;

namespace MeasureMap.UnitTest.Attributes;

public class IterationsBuilderElementTests
{
    private IBenchmarkBuilderElement _builder;
    private BenchmarkRunner _runner;

    [SetUp]
    public void Setup()
    {
        _builder = new IterationsBuilderElement();
        _runner = new BenchmarkRunner();
    }

    [Test]
    public void DurationBuilderElement_WithAttr()
    {
        _builder.Initialize(new IterationsWithAttr());
        _builder.Append(_runner);

        _runner.Settings.Iterations.Should().Be(10);
    }
    
    [Test]
    public void IterationsBuilderElement_NoAttr()
    {
        _builder.Initialize(new IterationsNoAttr());
        _builder.Append(_runner);

        _runner.Settings.Iterations.Should().Be(1);
    }
    
    [Iterations(10)]
    public class IterationsWithAttr
    {
    }
    
    public class IterationsNoAttr
    {
    }
}