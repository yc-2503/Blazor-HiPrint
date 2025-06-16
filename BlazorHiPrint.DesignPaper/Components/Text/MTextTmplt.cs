using BlazorHiprint.DesignPaper.Attributes;
using BlazorHiPrint.DesignPaper.Data;
namespace BlazorHiPrint.DesignPaper.Components.Text;

/// <summary>
/// 文本组件模板类，用于定义文本元素的属性和行为
/// </summary>
public class MTextTmplt : MComponentTmpltBase
{
    /// <summary>
    /// 初始化文本组件模板
    /// </summary>
    /// <param name="top">顶部位置</param>
    /// <param name="left">左侧位置</param>
    /// <param name="fieldHasChanged">属性变更回调</param>
    public MTextTmplt(double top, double left, Action<string, object?>? fieldHasChanged)
        : base(top, left, fieldHasChanged, UnitType.Text)
    {
    }
    private string? _text = "Text";
    /// <summary>
    /// 获取或设置文本内容
    /// </summary>
    public string? Text
    {
        get => _text;
        set
        {
            if (_text != value)
            {
                _text = value;
                FieldHasChanged?.Invoke(nameof(Text), value);
            }
        }
    }

    private int _fontSize = 12;
    /// <summary>
    /// 获取或设置字体大小(单位:px)
    /// </summary>
    public int FontSize
    {
        get => _fontSize;
        set
        {
            if (_fontSize != value)
            {
                _fontSize = value;
                FieldHasChanged?.Invoke(nameof(FontSize), value);
            }
        }
    }

    private string? _textColor = "#000000";
    /// <summary>
    /// 获取或设置文本颜色(十六进制格式，如"#FF0000")
    /// </summary>
    public string? TextColor
    {
        get => _textColor;
        set
        {
            if (_textColor != value)
            {
                _textColor = value;
                FieldHasChanged?.Invoke(nameof(TextColor), value);
            }
        }
    }

    private string? _backgroundColor = "transparent";
    /// <summary>
    /// 获取或设置背景颜色(十六进制格式或"transparent")
    /// </summary>
    public string? BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            if (_backgroundColor != value)
            {
                _backgroundColor = value;
                FieldHasChanged?.Invoke(nameof(BackgroundColor), value);
            }
        }
    }

    private FontFamilyType _fontFamily = FontFamilyType.Arial;
    /// <summary>
    /// 获取或设置字体类型
    /// </summary>
    public FontFamilyType FontFamily
    {
        get => _fontFamily;
        set
        {
            if (_fontFamily != value)
            {
                _fontFamily = value;
                FieldHasChanged?.Invoke(nameof(FontFamily), value);
            }
        }
    }

    /// <summary>
    /// 获取字体名称字符串(兼容旧版本)
    /// </summary>
    [AutoGenerateColumn(Ignore = true)]
    public string FontFamilyName => _fontFamily.ToString();

    private FontWeightType _fontWeight = FontWeightType.Normal;
    /// <summary>
    /// 获取或设置字体粗细
    /// </summary>
    public FontWeightType FontWeight
    {
        get => _fontWeight;
        set
        {
            if (_fontWeight != value)
            {
                _fontWeight = value;
                FieldHasChanged?.Invoke(nameof(FontWeight), value);
            }
        }
    }

    private bool _isItalic;
    /// <summary>
    /// 获取或设置是否斜体
    /// </summary>
    public bool IsItalic
    {
        get => _isItalic;
        set
        {
            if (_isItalic != value)
            {
                _isItalic = value;
                FieldHasChanged?.Invoke(nameof(IsItalic), value);
            }
        }
    }

    private bool _isUnderline;
    /// <summary>
    /// 获取或设置是否下划线
    /// </summary>
    public bool IsUnderline
    {
        get => _isUnderline;
        set
        {
            if (_isUnderline != value)
            {
                _isUnderline = value;
                FieldHasChanged?.Invoke(nameof(IsUnderline), value);
            }
        }
    }

    private string? _textAlign = "left";
    /// <summary>
    /// 获取或设置文本对齐方式("left", "center"或"right")
    /// </summary>
    public string? TextAlign
    {
        get => _textAlign;
        set
        {
            if (_textAlign != value)
            {
                _textAlign = value;
                FieldHasChanged?.Invoke(nameof(TextAlign), value);
            }
        }
    }


}
