using System.Security.Cryptography.X509Certificates;

namespace BlazorHiPrint.Client.Data
{
    public class MLineTmplt : MTmpltBase
    {
        public MLineTmplt(double top, double left, Action<string, object?>? fieldHasChanged) : base(top, left, fieldHasChanged, UnitType.Line)
        {

        }

        //线条长度属性，属性变化时，通知页面更新
        double _length = 100;
        public double Length
        {
            get { return _length; }
            set
            {
                if (_length != value)
                {
                    _length = value;
                    FieldHasChanged?.Invoke(nameof(Length), value);
                }
            }
        }

        //线条角度属性（以度为单位，0度为水平向右）
        double _angle = 0;
        public double Angle
        {
            get { return _angle; }
            set
            {
                if (_angle != value)
                {
                    _angle = value;
                    FieldHasChanged?.Invoke(nameof(Angle), value);
                }
            }
        }

        //线条粗细属性
        double _strokeWidth = 1;
        public double StrokeWidth
        {
            get { return _strokeWidth; }
            set
            {
                if (_strokeWidth != value)
                {
                    _strokeWidth = value;
                    FieldHasChanged?.Invoke(nameof(StrokeWidth), value);
                }
            }
        }

        //线条颜色属性
        string _strokeColor = "black";
        public string StrokeColor
        {
            get { return _strokeColor; }
            set
            {
                if (_strokeColor != value)
                {
                    _strokeColor = value;
                    FieldHasChanged?.Invoke(nameof(StrokeColor), value);
                }
            }
        }

        // 计算线条终点的X坐标
        public double EndX => Length * Math.Cos(Angle * Math.PI / 180);
        
        // 计算线条终点的Y坐标
        public double EndY => Length * Math.Sin(Angle * Math.PI / 180);

        // 计算SVG容器的宽度（需要包含线条的完整范围）
        public double SvgWidth => Math.Max(Math.Abs(EndX), 20) + 20;
        
        // 计算SVG容器的高度（需要包含线条的完整范围）
        public double SvgHeight => Math.Max(Math.Abs(EndY), 20) + 20;
    }
}
