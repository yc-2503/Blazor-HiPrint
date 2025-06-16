using ZXing;

namespace BlazorHiPrint.DesignPaper.Components.BarCode;

public class MBarCodeTmplt : MComponentTmpltBase
{
    public MBarCodeTmplt(double top, double left, Action<string, object?>? fieldHasChanged)
    : base(top, left, fieldHasChanged, UnitType.BarCode)
    {
        Console.WriteLine("1");
    }
    private double _width = 100;
    public double Width
    {
        get => _width;
        set
        {
            if (_width != value)
            {
                _width = value;
                FieldHasChanged?.Invoke(nameof(Width), value);
            }
        }
    }

    private double _height = 100;
    public double Height
    {
        get => _height;
        set
        {
            if (_height != value)
            {
                _height = value;
                FieldHasChanged?.Invoke(nameof(Height), value);
            }
        }
    }
    private BarcodeFormat _format = BarcodeFormat.CODE_128;
    string? _text  = "BARCODE";
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
