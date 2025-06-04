using BlazorHiPrint.DesignPaper.Components.BarCode;
using BlazorHiPrint.DesignPaper.Components.Circle;
using BlazorHiPrint.DesignPaper.Components.Image;
using BlazorHiPrint.DesignPaper.Components.Line;
using BlazorHiPrint.DesignPaper.Components.Rectangle;
using BlazorHiprint.DesignPaper.Components.Table;
using BlazorHiprint.DesignPaper.Components.Text;

namespace BlazorHiprint.DesignPaper.Data;

public abstract class MComponentCfgBase
{
    public MComponentCfgBase(double top, double left, Action<string, object?>? filedHasChanged, UnitType unitType)
    {
        _top = top;
        _left = left;
        FieldHasChanged = filedHasChanged;
        UnitType = unitType;
        ID = Guid.NewGuid().ToString();
    }
    public Action<string, object?>? FieldHasChanged;
    protected double _top;
    protected double _left;
    protected bool _isSelected;
    public string ID { get; init; }
    public Type ComponentType { get { return GetPrintElementType(); } }
    //控件类型
   // [AutoGenerateColumn(Ignore = true)]
    public UnitType UnitType { get; set; }
    //是否被选中
   // [AutoGenerateColumn(Ignore = true)]
    public bool IsSelected
    {
        get { return _isSelected; }
        set
        {
            var hasChanged = _isSelected != value;
            if (hasChanged)
            {
                _isSelected = value;
                FieldHasChanged?.Invoke(nameof(IsSelected), value);
            }
        }
    }
    public double Top
    {
        get { return _top; }
        set
        {
            var hasChanged = _top != value;
            if (hasChanged)
            {
                _top = value;
                FieldHasChanged?.Invoke(nameof(Top), value);
            }
        }
    }
    public double Left
    {
        get { return _left; }
        set
        {
            var hasChanged = _left != value;
            if (hasChanged)
            {
                _left = value;
                FieldHasChanged?.Invoke(nameof(Left), value);
            }
        }
    }
    public Type GetPrintElementType()
    {
        Type _selectedType;
        switch (UnitType)
        {
            case UnitType.BarCode:
                _selectedType = typeof(MBarCode);
                break;
            case UnitType.Text:
                _selectedType = typeof(MText);
                break;
            case UnitType.Rectangle:
                _selectedType = typeof(MRectangle);
                break;
            case UnitType.Line:
                _selectedType = typeof(MLine);
                break;
            case UnitType.Circle:
                _selectedType = typeof(MCircle);
                break;
            case UnitType.Image:
                _selectedType = typeof(MImage);
                break;
            case UnitType.Table:
                _selectedType = typeof(MTable);
                break;
            default:
                throw new InvalidOperationException("未处理类型:"+UnitType);
                
        }
        return _selectedType;
    }
}

public enum UnitType
{
    BarCode,
    Text,
    Rectangle,
    Line,
    Circle,
    Image,
    Table
}
