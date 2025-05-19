namespace PMSM.Client.WebApp.Client.Data;

public class MTemplateUnit
{
    public Action<string, object>? FiledHasChanged;
    private double _top;
    private double _left;
    private string _value = "QRCode";
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
    public string Text
    {
        get { return _value; }
        set
        {
            var hasChanged = _value != value;
            if (hasChanged)
            {
                _value = value;
                FiledHasChanged?.Invoke(nameof(Text), value);
            }
        }
    }
    public bool IsSelected { get; set; }
    public UnitType UnitType { get; set; }
    public object? Value { get; set; }
}
public enum UnitType
{
    Text,
    BarCode,
    QRCode
}
