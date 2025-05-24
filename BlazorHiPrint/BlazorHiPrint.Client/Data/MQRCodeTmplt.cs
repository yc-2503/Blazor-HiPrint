namespace BlazorHiPrint.Client.Data
{
    public class MQRCodeTmplt : MTmpltBase
    {
        public MQRCodeTmplt(double top, double left, Action<string, object>? filedHasChanged) : base(top, left, filedHasChanged, UnitType.QRCode)
        { }
        private string _text = "QRCode";
        private EcLevel _ecLevel;
        /// <summary>
        /// 获取或设置文本值。
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                // 检查文本值是否发生变化
                var hasChanged = _text != value;
                if (hasChanged)
                {
                    // 如果文本值发生变化，则更新文本值
                    _text = value;
                    // 调用事件处理程序，通知文本值已更改
                    FieldHasChanged?.Invoke(nameof(Text), value);
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
                    FieldHasChanged?.Invoke(nameof(EcLevel), value);
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
