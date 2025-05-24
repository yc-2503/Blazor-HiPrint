using BlazorHiPrint.Client.Components.BarCode;
using BlazorHiPrint.Client.Components.QRCode;
using BlazorHiPrint.Client.Components.Rectangle;
using BlazorHiPrint.Client.Components.Text;
using BlazorHiPrint.Client.Data;

namespace BlazorHiPrint.Client.Components;

public static class PrintElementsFactory
{
    public static Type? GetPrintElementType(MTmpltBase templateUnit)
    {
        Type? _selectedType;
        switch (templateUnit.UnitType)
        {
            case UnitType.QRCode:
                _selectedType = typeof(MQRCode);
                break;
            case UnitType.BarCode:
                _selectedType = typeof(MBarCode);
                break;
            case UnitType.Text:
                _selectedType = typeof(MText);
                break;
            case UnitType.Rectangle:
                _selectedType = typeof(MRectangle);
                break;
            default:
                _selectedType = null;
                break;
        }
        return _selectedType;
    }

    public static Type? GetPrintElementConfigureType(MTmpltBase templateUnit)
    {
        Type? _selectedType = null;
        switch (templateUnit.UnitType)
        {
            case UnitType.QRCode:
                _selectedType = typeof(MQRCodeConfig);
                break;
            case UnitType.BarCode:
                _selectedType = typeof(MBarCodeConfig);
                break;
            case UnitType.Text:
                _selectedType = typeof(MTextConfig);
                break;
            case UnitType.Rectangle:
                _selectedType = typeof(MRectangleConfig);
                break;
            default:
                _selectedType = null;
                break;
        }

        return _selectedType;
    }
    public static MTmpltBase CreateMTmplt(CreateMTmpltOptions options)
    {
        switch (options.UnitType)
        {
            case UnitType.BarCode:
                return new MBarCodeTmplt(options.Top, options.Left, options.FieldHasChanged);
            case UnitType.QRCode:
                return new MQRCodeTmplt(options.Top, options.Left, options.FieldHasChanged);
            case UnitType.Text:
                return new MQTextTmplt(options.Top, options.Left, options.FieldHasChanged);
            case UnitType.Rectangle:
                return new MRectangleTmplt(options.Top, options.Left, options.FieldHasChanged);
            default:
                throw new ApplicationException("控件类型未实现");
        }
    }

}
public class CreateMTmpltOptions
{
    public Action<string, object?>? FieldHasChanged { get; set; }
    public double Top { get; set; }
    public double Left { get; set; }
    public UnitType UnitType { get; set; }

}
