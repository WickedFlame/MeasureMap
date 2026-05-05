using MeasureMap.Attributes.Builder;
using MeasureMap.ContextStack;
using static MeasureMap.UnitTest.Attributes.OnEndPipelineBuilderElementTests;

namespace MeasureMap.UnitTest.Attributes;

public class OnStartPipelineBuilderElementTests
{
    private IBenchmarkBuilderElement _builder;
    private ProfilerSession _runner;

    [SetUp]
    public void Setup()
    {
        _builder = new OnStartPipelineBuilderElement();
        _runner = ProfilerSession.StartSession();
        _runner.SetContextStackBuilder(new InstanceBasedStackBuilder<OnStartPipelineWithAttr>(o => new Task(() => o.OnStart())));

        OnStartPipelineWithAttr.Called = false;
        OnStartPipelineNoAttr.Called = false;
    }
    
    [TearDown]
    public void TearDown()
    {
        _runner.Dispose();
    }

    [Test]
    public void OnStartPipelineBuilderElement_WithAttr()
    {
        _builder.Initialize<OnStartPipelineWithAttr>();
        _builder.Append(_runner);

        var runner = _runner.ContextStack.Create(0, new ProfilerSettings());
        runner.Run(Mock.Of<ITask>(), new ExecutionContext());

        OnStartPipelineWithAttr.Called.Should().BeTrue();
    }
    
    [Test]
    public void OnStartPipelineBuilderElement_NoAttr()
    {
        _builder.Initialize<OnStartPipelineNoAttr>();
        _builder.Append(_runner);

        _runner.Settings.OnStartPipelineEvent.Should().NotBeNull();

        _runner.Settings.OnStartPipelineEvent(new ProfilerSettings());
        OnStartPipelineNoAttr.Called.Should().BeFalse();
    }
    
    public class OnStartPipelineWithAttr
    {
        public static bool Called { get; set; } = false;

        [OnStartPipeline]
        public void OnStart()
        {
            Called = true;
        }
    }
    
    public class OnStartPipelineNoAttr
    {
        public static bool Called { get; set; } = false;

        public void OnStart()
        {
            Called = true;
        }
    }
}