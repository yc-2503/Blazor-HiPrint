using System;
using BootstrapBlazor.Components;

namespace BlazorHiPrint.Client.Data
{
    public class MCircleTmplt : MTmpltBase
    {
        public MCircleTmplt(double top, double left, Action<string, object?>? fieldHasChanged) 
            : base(top, left, fieldHasChanged, UnitType.Circle)
        {
        }

        private double _radius = 50;
        public double Radius
        {
            get => _radius;
            set
            {
                if (_radius != value)
                {
                    _radius = value;
                    FieldHasChanged?.Invoke(nameof(Radius), value);
                }
            }
        }

        public double Diameter => Radius * 2;
    }
}
