namespace BlazorHiPrint.DesignPaper.Components.Text;

public class MTextTmplt : MComponentTmpltBase
{
    private string? _text = "Text";
    public string? Text {
        get => _text;
        set {
            if (_text != value) {
                _text = value;
                FieldHasChanged?.Invoke(nameof(Text), value);
            }
        }
    }

    private int _fontSize = 12;
    public int FontSize {
        get => _fontSize;
        set {
            if (_fontSize != value) {
                _fontSize = value;
                FieldHasChanged?.Invoke(nameof(FontSize), value);
            }
        }
    }

    private string? _color = "#000000";
    public string? Color {
        get => _color;
        set {
            if (_color != value) {
                _color = value;
                FieldHasChanged?.Invoke(nameof(Color), value);
            }
        }
    }

    private string _fontFamily = "Arial";
    public string FontFamily {
        get => _fontFamily;
        set {
            if (_fontFamily != value) {
                _fontFamily = value;
                FieldHasChanged?.Invoke(nameof(FontFamily), value);
            }
        }
    }

    private string _fontWeight = "normal";
    public string FontWeight {
        get => _fontWeight;
        set {
            if (_fontWeight != value) {
                _fontWeight = value;
                FieldHasChanged?.Invoke(nameof(FontWeight), value);
            }
        }
    }

    private bool _isItalic;
    public bool IsItalic {
        get => _isItalic;
        set {
            if (_isItalic != value) {
                _isItalic = value;
                FieldHasChanged?.Invoke(nameof(IsItalic), value);
            }
        }
    }

    private bool _isUnderline;
    public bool IsUnderline {
        get => _isUnderline;
        set {
            if (_isUnderline != value) {
                _isUnderline = value;
                FieldHasChanged?.Invoke(nameof(IsUnderline), value);
            }
        }
    }

    private string? _textAlign = "left";
    public string? TextAlign {
        get => _textAlign;
        set {
            if (_textAlign != value) {
                _textAlign = value;
                FieldHasChanged?.Invoke(nameof(TextAlign), value);
            }
        }
    }

    [Obsolete("Use Color property instead")]
    public string? FontColor { get => Color; set => Color = value; }

    [Obsolete("Use FontWeight property instead")]
    public bool IsBold { get => FontWeight == "bold"; set => FontWeight = value ? "bold" : "normal"; }

    public MTextTmplt(double top, double left, Action<string, object?>? fieldHasChanged) 
        : base(top, left, fieldHasChanged, UnitType.Text)
    {
    }
}
