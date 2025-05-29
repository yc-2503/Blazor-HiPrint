namespace BlazorHiPrint.Client.Data
{
    public class MTextTmplt:  MTmpltBase
    {
        public MTextTmplt(double top,double left, Action<string, object?>? filedHasChanged) : base(top, left, filedHasChanged, UnitType.Text)
        {

        }
        private string _text = string.Empty ;

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
