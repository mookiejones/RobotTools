using System;
using System.ComponentModel;

namespace miRobotEditor.Core.Classes.AngleConverter
{
    [Localizable(false)]
    public sealed class Line3D : IGeometricElement3D
    {
        public Line3D(Point3D origin, Vector3D direction)
        {
            Origin = origin;
            Direction = direction;
            Direction.Normalise();
        }

        public Point3D GetPoint(double u)
        {
            var vectord = new Vector3D(u * Direction);
            return (Origin + vectord);
        }

        public override string ToString()
        {
            return string.Format("Line: Origin={0}, Direction={1}", Origin, Direction);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("Line: Origin={0}, Direction={1}", Origin.ToString(format, formatProvider), Direction.ToString(format, formatProvider));
        }

        public Vector3D Direction { get; private set; }

        TransformationMatrix3D IGeometricElement3D.Position
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Point3D Origin { get; private set; }
       
    }
}

