using BlazorHiprint.DesignPaper.Data;

namespace BlazorHiprint.DesignPaper.Data;

public class MTextTmplt : MTmpltBase
{
    public string? Text { get; set; }
    public int FontSize { get; set; } = 12;
    public string? Color { get; set; } = "#000000";
    public string FontFamily { get; set; } = "Arial";
    public string FontWeight { get; set; } = "normal";
    public bool IsItalic { get; set; }
    public bool IsUnderline { get; set; }
    public string? TextAlign { get; set; } = "left";

    [Obsolete("Use Color property instead")]
    public string? FontColor { get => Color; set => Color = value; }

    [Obsolete("Use FontWeight property instead")]
    public bool IsBold { get => FontWeight == "bold"; set => FontWeight = value ? "bold" : "normal"; }

    public MTextTmplt(double top, double left, Action<string, object?>? fieldHasChanged) 
        : base(top, left, fieldHasChanged, UnitType.Text)
    {
    }
}
