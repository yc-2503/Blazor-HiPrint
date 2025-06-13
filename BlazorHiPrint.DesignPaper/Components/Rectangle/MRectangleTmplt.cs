namespace BlazorHiPrint.DesignPaper.Components.Rectangle;

public class MRectangleTmplt : MComponentTmpltBase
{
    public MRectangleTmplt(double top, double left, Action<string, object?>? fieldHasChanged)
    : base(top, left, fieldHasChanged, UnitType.Rectangle)
    {
    }
    private double _width = 100;
    public double Width {
        get => _width;
        set {
            if (_width != value) {
                _width = value;
                FieldHasChanged?.Invoke(nameof(Width), value);
            }
        }
    }

    private double _height = 100;
    public double Height {
        get => _height;
        set {
            if (_height != value) {
                _height = value;
                FieldHasChanged?.Invoke(nameof(Height), value);
            }
        }
    }

    private string? _fillColor = "transparent";
    public string? FillColor {
        get => _fillColor;
        set {
            if (_fillColor != value) {
                _fillColor = value;
                FieldHasChanged?.Invoke(nameof(FillColor), value);
            }
        }
    }

    private string? _strokeColor = "#000000";
    public string? StrokeColor {
        get => _strokeColor;
        set {
            if (_strokeColor != value) {
                _strokeColor = value;
                FieldHasChanged?.Invoke(nameof(StrokeColor), value);
            }
        }
    }

    private int _strokeWidth = 1;
    public int StrokeWidth {
        get => _strokeWidth;
        set {
            if (_strokeWidth != value) {
                _strokeWidth = value;
                FieldHasChanged?.Invoke(nameof(StrokeWidth), value);
            }
        }
    }


}
