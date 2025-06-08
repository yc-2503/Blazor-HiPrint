using BlazorHiprint.DesignPaper.Data;

namespace BlazorHiprint.DesignPaper.Data;

public class MLineTmplt : MComponentTmpltBase
{

    public MLineTmplt(double top, double left, Action<string, object?>? fieldHasChanged) : base(top, left, fieldHasChanged, UnitType.Line)
    {
        //_centerY = top + Length / 2 * Math.Sin(Angle * Math.PI / 180);
        //_centerX = left + Length / 2 * Math.Cos(Angle * Math.PI / 180);
    }
    //double _centerX = 0;
    //double _centerY = 0;
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
                double x1 = SvgWidth;
                double y1 = SvgHeight;
                _angle = value;
                double dx = SvgWidth - x1;
                double dy = SvgHeight - y1;

                _top = _top - dy / 2;
                _left = _left - dx / 2;
                if (_top < 0)
                {
                    _top = 0;
                }
                if (_left < 0)
                {
                    _left = 0;
                }
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
   // [AutoGenerateColumn(Ignore = true)]
    // 计算线条起点的X坐标
    public double StartX => (SvgWidth / 2) - Length / 2 * Math.Cos(Angle * Math.PI / 180);
   // [AutoGenerateColumn(Ignore = true)]
    // 计算线条起点的Y坐标
    public double StartY => (SvgHeight / 2) - Length / 2 * Math.Sin(Angle * Math.PI / 180);

  // [AutoGenerateColumn(Ignore = true)]
    // 计算线条终点的X坐标
    public double EndX => (SvgWidth / 2) + Length / 2 * Math.Cos(Angle * Math.PI / 180);
  //  [AutoGenerateColumn(Ignore = true)]
    // 计算线条终点的Y坐标
    public double EndY => (SvgHeight / 2) + Length / 2 * Math.Sin(Angle * Math.PI / 180);
 //   [AutoGenerateColumn(Ignore = true)]
    // 计算SVG容器的宽度（需要包含线条的完整范围）
    public double SvgWidth => Math.Abs(Length * Math.Cos(Angle * Math.PI / 180));
  //  [AutoGenerateColumn(Ignore = true)]
    // 计算SVG容器的高度（需要包含线条的完整范围）
    public double SvgHeight => Math.Abs(Length * Math.Sin(Angle * Math.PI / 180)) + _strokeWidth;
}
