using BlazorHiprint.DesignPaper.Data;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

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
}
