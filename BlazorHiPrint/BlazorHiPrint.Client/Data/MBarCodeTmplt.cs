using BootstrapBlazor.Components;
using ZXing;

namespace BlazorHiPrint.Client.Data
{
    public class MBarCodeTmplt:  MTmpltBase
    {
        public MBarCodeTmplt(double top, double left,Action<string, object>? filedHasChanged) : base(top, left,filedHasChanged, UnitType.BarCode)
        { }

        private string _text = "0123456789";
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

}
