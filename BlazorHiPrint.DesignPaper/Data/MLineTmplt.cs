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
    //�����������ԣ����Ա仯ʱ��֪ͨҳ�����
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

    //�����Ƕ����ԣ��Զ�Ϊ��λ��0��Ϊˮƽ���ң�
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

    //������ϸ����
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

    //������ɫ����
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
    // ������������X����
    public double StartX => (SvgWidth / 2) - Length / 2 * Math.Cos(Angle * Math.PI / 180);
   // [AutoGenerateColumn(Ignore = true)]
    // ������������Y����
    public double StartY => (SvgHeight / 2) - Length / 2 * Math.Sin(Angle * Math.PI / 180);

  // [AutoGenerateColumn(Ignore = true)]
    // ���������յ��X����
    public double EndX => (SvgWidth / 2) + Length / 2 * Math.Cos(Angle * Math.PI / 180);
  //  [AutoGenerateColumn(Ignore = true)]
    // ���������յ��Y����
    public double EndY => (SvgHeight / 2) + Length / 2 * Math.Sin(Angle * Math.PI / 180);
 //   [AutoGenerateColumn(Ignore = true)]
    // ����SVG�����Ŀ�ȣ���Ҫ����������������Χ��
    public double SvgWidth => Math.Abs(Length * Math.Cos(Angle * Math.PI / 180));
  //  [AutoGenerateColumn(Ignore = true)]
    // ����SVG�����ĸ߶ȣ���Ҫ����������������Χ��
    public double SvgHeight => Math.Abs(Length * Math.Sin(Angle * Math.PI / 180)) + _strokeWidth;
}
