
using System.Security.Cryptography.X509Certificates;

namespace BlazorHiPrint.Client.Data
{
    public class MRectangleTmplt : MTmpltBase
    {
        public MRectangleTmplt(double top, double left, Action<string, object?>? fieldHasChanged) : base(top, left, fieldHasChanged, UnitType.Rectangle)
        {

        }

        //宽度属性，属性变化时，通知页面更新
        int _width = 0;
        public int Width
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
        //高度属性
        /// <summary>
        /// 存储高度的变量
        /// </summary>
        public int _height = 0;
        
        /// <summary>
        /// 获取或设置高度
        /// 当高度的值改变时，会触发FieldHasChanged事件
        /// </summary>
        public int Height
        {
            get { return _height; }
            set
            {
                // 只有当新值与当前值不同时，才更新值并触发事件
                if (_height != value)
                {
                    _height = value;
                    // 触发事件，通知订阅者高度已改变
                    FieldHasChanged?.Invoke(nameof(Height), value);
                }
            }
        }









    }
}
