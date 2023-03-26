using IconPark.Props;
using IconPark.Svg;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using OneOf;
using System.Reflection.Metadata;

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
    /// <summary>
    /// 主题
    /// </summary>
    [Parameter]
    public ThemeType Theme { get; set; } = ThemeType.Outline;

    /// <summary>
    /// 大小（宽度和高度）
    /// </summary>
    [Parameter]
    public OneOf<int, string> Size { get; set; } = DefaultConfig.Size;

    /// <summary>
    /// 线宽
    /// </summary>
    [Parameter]
    public int StrokeWidth { get; set; } = DefaultConfig.StrokeWidth;

    /// <summary>
    /// 填充色
    /// </summary>
    [Parameter]
    public List<string> Fill { get; set; } = new List<string>();

    /// <summary>
    /// svg 路径两端的形状
    /// </summary>
    [Parameter]
    public StrokeLinecapType StrokeLinecap { get; set; } = DefaultConfig.StrokeLinecap;

    /// <summary>
    /// svg 路径的转角处使用的形状或者绘制的基础形状
    /// </summary>
    [Parameter]
    public StrokeLinejoinType StrokeLinejoin { get; set; } = DefaultConfig.StrokeLinejoin;

    /// <summary>
    /// 唯一Id，无需指定，默认为guid
    /// </summary>
    public string Id { get; private set; } = Guid.NewGuid().ToString();

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
        builder.OpenElement(0, "span");
        builder.AddAttribute(1, "class", "i-icon");
    }
}
