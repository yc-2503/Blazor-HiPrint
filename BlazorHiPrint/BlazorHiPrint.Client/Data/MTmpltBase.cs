using BootstrapBlazor.Components;

namespace BlazorHiPrint.Client.Data;

public class MTmpltBase
{
    public  MTmpltBase(double top, double left, Action<string, object?>? filedHasChanged, UnitType unitType)
    {
        _top = top;
        _left = left;
        FiledHasChanged  = filedHasChanged;
        UnitType = unitType;
    }
    public Action<string, object?>? FiledHasChanged;
    private double _top;
    private double _left;

    public double Top
    {
        get { return _top; }
        set
        {
            var hasChanged = _top != value;
            if (hasChanged)
            {
                _top = value;
                FiledHasChanged?.Invoke(nameof(Top), value);
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
                FiledHasChanged?.Invoke(nameof(Left), value);
            }
        }
    }


    [AutoGenerateColumn(Ignore = true)]
    public UnitType UnitType { get; set; }

}
public enum UnitType
{
    Text,
    BarCode,
    QRCode
}
