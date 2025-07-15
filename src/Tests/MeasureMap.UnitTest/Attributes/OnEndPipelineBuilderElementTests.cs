using System;
using FluentAssertions;
using MeasureMap.Attributes.Builder;
using NUnit.Framework;

namespace MeasureMap.UnitTest.Attributes;

public class OnEndPipelineBuilderElementTests
{
    private IBenchmarkBuilderElement _builder;
    private ProfilerSession _runner;

    [SetUp]
    public void Setup()
    {
        _builder = new OnEndPipelineBuilderElement();
        _runner = ProfilerSession.StartSession();

        OnEndPipelineWithAttr.Called = false;
        OnEndPipelineNoAttr.Called = false;
    }

    [TearDown]
    public void TearDown()
    {
        _runner.Dispose();
    }

    [Test]
    public void OnEndPipelineBuilderElement_WithAttr()
    {
        _builder.Initialize(new OnEndPipelineWithAttr());
        _builder.Append(_runner);

        _runner.Settings.OnEndPipelineEvent.Should().NotBeNull();
        
        _runner.Settings.OnEndPipelineEvent(new ExecutionContext());
        OnEndPipelineWithAttr.Called.Should().BeTrue();
    }
    
    [Test]
    public void OnEndPipelineBuilderElement_NoAttr()
    {
        _builder.Initialize(new OnEndPipelineNoAttr());
        _builder.Append(_runner);

        _runner.Settings.OnEndPipelineEvent.Should().NotBeNull();

        _runner.Settings.OnEndPipelineEvent(new ExecutionContext());
        OnEndPipelineNoAttr.Called.Should().BeFalse();
    }
    
    public class OnEndPipelineWithAttr
    {
        public static bool Called { get; set; } = false;

        [OnEndPipeline]
        public void OnEnd()
        {
            Called = true;
        }
    }
    
    public class OnEndPipelineNoAttr
    {
        public static bool Called { get; set; } = false;

        public void OnEnd()
        {
            Called = true;
        }
    }
}