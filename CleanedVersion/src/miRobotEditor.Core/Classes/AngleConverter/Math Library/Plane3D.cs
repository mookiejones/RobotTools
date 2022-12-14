using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace miRobotEditor.Core.Classes.AngleConverter
{
    [Localizable(false)]
    public sealed class Plane3D : IGeometricElement3D
    {
        public Plane3D(Point3D point, Vector3D normal)
        {
            Point = point;
            Normal = normal;
        }

        public static Plane3D FitToPoints(Collection<Point3D> points)
        {
            var fitd = new LeastSquaresFit3D();
            return fitd.FitPlaneToPoints(points);
        }

        public override string ToString()
        {
            return string.Format("Plane: Origin={0}, Normal={1}", Point, Normal);
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("Plane: Origin={0}, Normal={1}", Point.ToString(format, formatProvider), Normal.ToString(format, formatProvider));
        }

        public double A
        {
            get
            {
                return Normal.X;
            }
        }

        public double B
        {
            get
            {
                return Normal.Y;
            }
        }

        public double C
        {
            get
            {
                return Normal.Z;
            }
        }

        public double D
        {
            get
            {
                return -Vector.Dot(Normal, (Vector3D) Point);
            }
        }

        TransformationMatrix3D IGeometricElement3D.Position
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Vector3D Normal { get; private set; }

        public Point3D Point { get; private set; }
    }
}

