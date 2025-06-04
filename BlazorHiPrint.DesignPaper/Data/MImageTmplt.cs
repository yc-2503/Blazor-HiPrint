using BlazorHiprint.DesignPaper.Data;

namespace BlazorHiprint.DesignPaper.Data;

public class MImageTmplt : MComponentCfgBase
{

    public MImageTmplt(double top, double left, Action<string, object?>? fieldHasChanged) 
        : base(top, left, fieldHasChanged, UnitType.Image)
    {
    }
    string _src = "";
    public string Src
    {
        get { return _src; }
        set
        {
            if (_src != value)
            {
                _src = value;
                FieldHasChanged?.Invoke(nameof(Src), value);
            }
        }
    }

    double _width = 100;
    public double Width
    {
        get { return _width; }
        set
        {
            if (_width != value)
            {
                _width = value;
                FieldHasChanged?.Invoke(nameof(Width), value);
            }
        }
    }

    double _height = 100;
    public double Height
    {
        get { return _height; }
        set
        {
            if (_height != value)
            {
                _height = value;
                FieldHasChanged?.Invoke(nameof(Height), value);
            }
        }
    }
}
