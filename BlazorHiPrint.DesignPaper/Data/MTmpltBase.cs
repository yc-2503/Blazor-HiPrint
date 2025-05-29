using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHiprint.DesignPaper.Data;

public abstract class MTmpltBase
{
    public double Top { get; set; }
    public double Left { get; set; }
    public bool IsSelected { get; set; }
    public UnitType UnitType { get; }
    public Action<string, object?>? FieldHasChanged { get; set; }

    public MTmpltBase(double top, double left, Action<string, object?>? fieldHasChanged, UnitType unitType)
    {
        Top = top;
        Left = left;
        FieldHasChanged = fieldHasChanged;
        UnitType = unitType;
    }
}

public enum UnitType
{
    BarCode,
    Text,
    Rectangle,
    Line,
    Circle,
    Image,
    Table
}
