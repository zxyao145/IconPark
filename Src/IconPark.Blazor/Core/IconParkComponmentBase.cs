using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace IconPark;

public abstract class IconParkComponmentBase : IComponent
{
    private RenderHandle _renderHandle;
    private RenderFragment _renderFragment;

    public IconParkComponmentBase()
    {
        _renderFragment = BuildRenderTree;
    }

    #region props


    [Parameter]
    public Theme Theme { get; set; } = Theme.Outline;

    [Parameter]
    public string Size { get; set; } = DefaultConfig.Size;

    [Parameter]
    public int StrokeWidth { get; set; } = DefaultConfig.StrokeWidth;

    [Parameter]
    public List<string> Fill { get; set; } = new List<string>();

    [Parameter]
    public StrokeLinecap StrokeLinecap { get; set; } = DefaultConfig.StrokeLinecap;

    [Parameter]
    public StrokeLinejoin StrokeLinejoin { get; set; } = DefaultConfig.StrokeLinejoin;


    [Parameter]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    #endregion


    public SvgOptions GetSvgOptions()
    {
        return new SvgOptions()
        {
            Theme = this.Theme,
            Size = this.Size,
            Fill = this.Fill,
            StrokeWidth = this.StrokeWidth,
            StrokeLinecap = this.StrokeLinecap,
            StrokeLinejoin = this.StrokeLinejoin,
            Id = this.Id
        };
    }


    public void Attach(RenderHandle renderHandle)
    {
        _renderHandle = renderHandle;
    }

    public Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);
        _renderHandle.Render(_renderFragment);

        return Task.CompletedTask;
    }

    protected virtual void BuildRenderTree(RenderTreeBuilder builder)
    {
    }
}
