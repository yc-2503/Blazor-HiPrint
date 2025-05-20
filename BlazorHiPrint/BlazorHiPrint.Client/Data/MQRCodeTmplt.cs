namespace BlazorHiPrint.Client.Data
{
    public class MQRCodeTmplt:  MTmpltBase
    {
        public MQRCodeTmplt(double top,double left, Action<string, object>? filedHasChanged) :base(top,left, filedHasChanged, UnitType.QRCode)
        { }
        private string _text = "QRCode";
        private EcLevel _ecLevel;
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
        public EcLevel EcLevel
        {
            get { return _ecLevel; }
            set
            {
                var hasChanged = _ecLevel != value;
                if (hasChanged)
                {
                    _ecLevel = value;
                    FiledHasChanged?.Invoke(nameof(EcLevel), value);
                }
            }
        }
    }
    public enum EcLevel
    {
        L = 1,
        M = 0,
        Q = 3,
        H = 2
    }
}
