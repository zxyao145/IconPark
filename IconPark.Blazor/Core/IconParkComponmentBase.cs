using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace IconPark;

public abstract class IconParkComponmentBase : IComponent
{
    private RenderHandle _renderHandle;
    private RenderFragment _renderFragment;


    #region props

    private IconParkOptions Props { get; set; }

    [Parameter]
    public string Id
    {
        get => Props.Id; 
        set => Props.Id = value;
    }

    [Parameter]
    public string Size
    {
        get => Props.Size.IsT0 ? Props.Size.AsT0.ToString() : Props.Size.AsT1;
        set => Props.Size = value;
    }

    [Parameter]
    public int StrokeWidth
    {
        get => Props.StrokeWidth; 
        set => Props.StrokeWidth = value;
    }

    [Parameter]
    public Theme Theme
    {
        get => Props.Theme;
        set => Props.Theme = value;
    }

    [Parameter]
    public StrokeLinecap StrokeLinecap
    {
        get => Props.StrokeLinecap;
        set => Props.StrokeLinecap = value;
    }

    [Parameter]
    public StrokeLinejoin StrokeLinejoin
    {
        get => Props.StrokeLinejoin;
        set => Props.StrokeLinejoin = value;
    }


    [Parameter]
    public List<string> Fill
    {
        get => Props.Fill;
        set => Props.Fill = value;
    }

    protected List<string> Colors => Props.Colors;

    #endregion

    private bool isFirsrRender = true;

    public IconParkComponmentBase()
    {
        _renderFragment = BuildRenderTree;
        Props = new IconParkOptions();
    }

    public void Attach(RenderHandle renderHandle)
    {
        _renderHandle = renderHandle;
    }

    public Task SetParametersAsync(ParameterView parameters)
    {
        if (ShouldRender(parameters))
        {
            _renderHandle.Render(_renderFragment);
        }

        return Task.CompletedTask;
    }

    protected bool ShouldRender(ParameterView parameters)
    {
        if (isFirsrRender)
        {
            isFirsrRender = false;
            parameters.SetParameterProperties(this);
            return true;
        }
        parameters.TryGetValue<IconParkOptions>(nameof(Props), out var newProps);
       
        if (newProps != null && newProps != Props)
        {
            parameters.SetParameterProperties(this);
            return true;
        }
        return false;
    }

    protected virtual void BuildRenderTree(RenderTreeBuilder builder)
    {
    }
}
