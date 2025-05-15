namespace PMSM.Client.WebApp.Client.Data;

public class MTemplateUnit
{
    public double Top { get; set; }
    public double Left { get; set; }
    public string Text { get; set; }
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
