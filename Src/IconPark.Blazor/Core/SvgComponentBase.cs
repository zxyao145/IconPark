using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace IconPark;

public abstract class SvgComponentBase : IComponent
{
    private RenderHandle _renderHandle;
    private RenderFragment _renderFragment;


    #region props

    [Parameter]
    public SvgOptions Props { get; set; }

    #endregion

    private bool isFirsrRender = true;

    public SvgComponentBase()
    {
        _renderFragment = BuildRenderTree;
        Props = new SvgOptions();
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

        parameters.TryGetValue<SvgOptions>(nameof(Props), out var newProps);
       
        if(newProps == null)
        {
            newProps = new SvgOptions();
        }

        if (newProps != Props)
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
