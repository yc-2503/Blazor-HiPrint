using System;

namespace BlazorHiPrint.Client.Data
{
    public class MImageTmplt : MTmpltBase
    {
        public MImageTmplt(double top, double left, Action<string, object?>? fieldHasChanged) 
            : base(top, left, fieldHasChanged, UnitType.Image)
        {
        }

        string _imagePath = "";
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                if (_imagePath != value)
                {
                    _imagePath = value;
                    FieldHasChanged?.Invoke(nameof(ImagePath), value);
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
}
