using BlazorHiprint.DesignPaper.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorHiPrint.DesignPaper.Components;

public partial class MComponentConfigBox
{
    /// <summary>
    /// 当前选中的组件
    /// </summary>

    [Parameter]
    public MComponentCfgBase? SelectedItem { get; set; }
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (SelectedItem != null)
        {
            Fragment =  (RenderTreeBuilder __builder) =>
            {

                __builder.OpenComponent(1, SelectedItem.ComponentConfigType);
                __builder.AddComponentParameter(2, "Data", SelectedItem);
                __builder.CloseComponent();

            };
        }else
        {
            Fragment = null;
        }
    }
    RenderFragment? Fragment { get; set; }
}
