using BootstrapBlazor.Components;

namespace BlazorHiPrint.Client.Data;

public class MTmpltBase
{
    public  MTmpltBase(double top, double left, Action<string, object?>? filedHasChanged, UnitType unitType)
    {
        _top = top;
        _left = left;
        FieldHasChanged  = filedHasChanged;
        UnitType = unitType;
    }
    public Action<string, object?>? FieldHasChanged;
    private double _top;
    private double _left;
    //控件类型
    [AutoGenerateColumn(Ignore = true)]
    public UnitType UnitType { get; set; }
    //是否被选中
    [AutoGenerateColumn(Ignore = true)]
    public bool IsSelected { get; set; }
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
    Text,
    BarCode,
    QRCode,
    Rectangle,
    Line,
    Circle,
    Image

}
