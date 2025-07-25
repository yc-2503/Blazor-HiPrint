using BlazorHiPrint.DesignPaper.Components;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;

namespace BlazorHiprint.DesignPaper.Components;

public class MElementBase<TTmplt>: ComponentBase where TTmplt : MComponentTmpltBase
{

    [NotNull]
    [Parameter]
    public TTmplt? Data { get; set; }
    bool _shouldRender = false;
    protected override bool ShouldRender()
    {
        return _shouldRender;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        if (firstRender) {
            Data.FieldHasChanged += (name, _) => {
                _shouldRender = true;
                StateHasChanged();
            };
        }
        _shouldRender = false;
    }
}
