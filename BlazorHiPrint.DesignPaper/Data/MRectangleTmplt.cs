namespace BlazorHiprint.DesignPaper.Data;

public class MRectangleTmplt : MTmpltBase
{
    public int Width { get; set; } = 100;
    public int Height { get; set; } = 100;
    public string? FillColor { get; set; } = "transparent";
    public string? StrokeColor { get; set; } = "#000000";
    public int StrokeWidth { get; set; } = 1;

    public MRectangleTmplt(double top, double left, Action<string, object?>? fieldHasChanged) 
        : base(top, left, fieldHasChanged, UnitType.Rectangle)
    {
    }
}
