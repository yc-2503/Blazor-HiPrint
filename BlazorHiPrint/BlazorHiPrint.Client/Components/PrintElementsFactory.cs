using BlazorHiPrint.Client.Components.BarCode;
using BlazorHiPrint.Client.Components.Circle;
using BlazorHiPrint.Client.Components.Image;
using BlazorHiPrint.Client.Components.Line;
using BlazorHiPrint.Client.Components.Rectangle;
using BlazorHiPrint.Client.Components.Table;
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

            case UnitType.BarCode:
                _selectedType = typeof(MBarCodeConfig);
                break;
            case UnitType.Text:
                _selectedType = typeof(MTextConfig);
                break;
            case UnitType.Rectangle:
                _selectedType = typeof(MRectangleConfig);
                break;
            case UnitType.Line:
                _selectedType = typeof(MLineConfig);
                break;
            case UnitType.Circle:
                _selectedType = typeof(MCircleConfig);
                break;
            case UnitType.Image:
                _selectedType = typeof(MImageConfig);
                break;
            case UnitType.Table:
                _selectedType = typeof(MTableConfig);
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
            case UnitType.Text:
                return new MTextTmplt(options.Top, options.Left, options.FieldHasChanged);
            case UnitType.Rectangle:
                return new MRectangleTmplt(options.Top, options.Left, options.FieldHasChanged);
            case UnitType.Line:
                return new MLineTmplt(options.Top, options.Left, options.FieldHasChanged);
            case UnitType.Circle:
                return new MCircleTmplt(options.Top, options.Left, options.FieldHasChanged);
            case UnitType.Image:
                return new MImageTmplt(options.Top, options.Left, options.FieldHasChanged);
            case UnitType.Table:
                return new MTableTmplt(options.Top, options.Left, options.FieldHasChanged);
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
    public object? Value { get; set; }

}
