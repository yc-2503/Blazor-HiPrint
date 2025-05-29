using BlazorHiprint.DesignPaper.Data;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace BlazorHiprint.DesignPaper.Components;

public class MElementBase<TTmplt>: ComponentBase where TTmplt : class
{
    [NotNull]
    [Parameter]
    public object? Value { get; set; }
    [NotNull]
    protected TTmplt? Data { get; set; }
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        Data = Value as TTmplt;
    }
}
