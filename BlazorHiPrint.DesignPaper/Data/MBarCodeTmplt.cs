using BlazorHiprint.DesignPaper.Data;
using ZXing;

namespace BlazorHiprint.DesignPaper.Data;

public class MBarCodeTmplt : MComponentTmpltBase
{
    public MBarCodeTmplt(double top, double left, Action<string, object?>? fieldHasChanged)
    : base(top, left, fieldHasChanged, UnitType.BarCode)
    {
    }
    string? _text  = "BARCODE";
    public int Width { get; set; } = 100;
    public int Height { get; set; } = 50;
    private BarcodeFormat _format = BarcodeFormat.CODE_128;
    public string Text
    {
        get { return _text; }
        set
        {
            var hasChanged = _text != value;
            if (hasChanged)
            {
                _text = value;
                FieldHasChanged?.Invoke(nameof(Text), value);
            }
        }
    }
    public BarcodeFormat Format
    {
        get { return _format; }
        set
        {
            var hasChanged = _format != value;
            if (hasChanged)
            {
                _format = value;
                FieldHasChanged?.Invoke(nameof(BarcodeFormat), value);
            }
        }
    }


}
