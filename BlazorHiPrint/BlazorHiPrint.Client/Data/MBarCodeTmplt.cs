namespace BlazorHiPrint.Client.Data
{
    public class MBarCodeTmplt:  MTmpltBase
    {
        public MBarCodeTmplt(double top, double left,Action<string, object>? filedHasChanged) : base(top, left,filedHasChanged, UnitType.BarCode)
        { }

        private string _text = "BarCode";

        public string Text
        {
            get { return _text; }
            set
            {
                var hasChanged = _text != value;
                if (hasChanged)
                {
                    _text = value;
                    FiledHasChanged?.Invoke(nameof(Text), value);
                }
            }
        }

    }

}
