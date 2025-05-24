namespace BlazorHiPrint.Client.Data
{
    public class MQTextTmplt:  MTmpltBase
    {
        public MQTextTmplt(double top,double left, Action<string, object>? filedHasChanged) : base(top, left, filedHasChanged, UnitType.Text)
        { }
        private string _text = "Text";

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

    }

}
