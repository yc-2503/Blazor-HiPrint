using BlazorHiprint.DesignPaper.Attributes;

namespace BlazorHiprint.DesignPaper.Data;

public abstract class MComponentTmpltBase
{
    public MComponentTmpltBase(double top, double left, Action<string, object?>? filedHasChanged, UnitType unitType)
    {
        _top = top;
        _left = left;
        FieldHasChanged = filedHasChanged;
        UnitType = unitType;
        ID = Guid.NewGuid().ToString();
    }
    public Action<string, object?>? FieldHasChanged { get; set; }
    protected double _top;
    protected double _left;
    protected bool _isSelected;
    [AutoGenerateColumn(Ignore = true)]
    public string ID { get; init; }


    //控件类型
    [AutoGenerateColumn(Ignore = true)]
    public UnitType UnitType { get; set; }
    //是否被选中
    [AutoGenerateColumn(Ignore = true)]
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
