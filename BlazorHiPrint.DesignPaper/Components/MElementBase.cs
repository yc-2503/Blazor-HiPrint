using BlazorHiprint.DesignPaper.Data;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;

namespace BlazorHiprint.DesignPaper.Components;

public class MElementBase<TTmplt>: ComponentBase where TTmplt : MComponentCfgBase
{

    [NotNull]
    [Parameter]
    public TTmplt? Data { get; set; }
    bool _shouldRender = false;
    protected override bool ShouldRender()
    {
        return _shouldRender;
    }
    protected override async Task OnParametersSetAsync()
    {

        Data.FieldHasChanged += (_, _) => { _shouldRender = true; StateHasChanged(); };

        await base.OnParametersSetAsync();
        _shouldRender = false;
    }
}
