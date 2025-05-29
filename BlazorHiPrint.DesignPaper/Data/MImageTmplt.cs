using BlazorHiprint.DesignPaper.Data;

namespace BlazorHiprint.DesignPaper.Data;

public class MImageTmplt : MTmpltBase
{
    public string? Src { get; set; }
    public int Width { get; set; } = 100;
    public int Height { get; set; } = 100;

    public MImageTmplt(double top, double left, Action<string, object?>? fieldHasChanged) 
        : base(top, left, fieldHasChanged, UnitType.Image)
    {
    }
}
