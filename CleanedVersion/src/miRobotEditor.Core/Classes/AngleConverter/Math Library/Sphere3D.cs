using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace miRobotEditor.Core.Classes.AngleConverter
{
    [Localizable(false)]
    public class Sphere3D : IGeometricElement3D
    {
      

        public Sphere3D()
        {
            Origin = new Point3D();
            Radius = 0.0;
        }

        public Sphere3D(Sphere3D sphere)
        {
            Origin = sphere.Origin;
            Radius = sphere.Radius;
        }

        public Sphere3D(Point3D origin, double radius)
        {
            Origin = origin;
            Radius = radius;
        }

        public static Sphere3D FitToPoints(Collection<Point3D> points)
        {
            var fitd = new LeastSquaresFit3D();
            return fitd.FitSphereToPoints(points);
        }

        public override string ToString()
        {
            return ToString("", null);
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("Sphere3D: Centre {0:F2} Radius {1:F2}", Origin, Radius);
        }

        public Point3D Origin { get; set; }
        public TransformationMatrix3D Position
        {
            get
            {
                return new TransformationMatrix3D((Vector3D) Origin, RotationMatrix3D.Identity());
            }
        }

        public double Radius { get; set; }
    }
}

