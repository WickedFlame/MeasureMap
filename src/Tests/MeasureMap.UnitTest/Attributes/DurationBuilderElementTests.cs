using System;
using FluentAssertions;
using MeasureMap.Attributes.Builder;
using NUnit.Framework;

namespace MeasureMap.UnitTest.Attributes;

public class DurationBuilderElementTests
{
    private IBenchmarkBuilderElement _builder;
    private BenchmarkRunner _runner;

    [SetUp]
    public void Setup()
    {
        _builder = new DurationBuilderElement();
        _runner = new BenchmarkRunner();
    }

    [Test]
    public void DurationBuilderElement_WithAttr()
    {
        _builder.Initialize(new DurationWithAttr());
        _builder.Append(_runner);

        _runner.Settings.Duration.Should().Be(TimeSpan.FromSeconds(10));
    }
    
    [Test]
    public void DurationBuilderElement_NoAttr()
    {
        _builder.Initialize(new DurationNoAttr());
        _builder.Append(_runner);

        _runner.Settings.Duration.Should().Be(TimeSpan.Zero);
    }
    
    [Duration(10)]
    public class DurationWithAttr
    {
    }
    
    public class DurationNoAttr
    {
    }
}