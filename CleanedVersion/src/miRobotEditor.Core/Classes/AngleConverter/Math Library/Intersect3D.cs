using System;
using System.ComponentModel;

namespace miRobotEditor.Core.Classes.AngleConverter
{
    [Localizable(false)]
    public static class Intersect3D
    {
        public static IGeometricElement3D ElementToElement(IGeometricElement3D e1, IGeometricElement3D e2)
        {
            if ((e1 is Point3D) || (e2 is Point3D))
            {
                throw new NotImplementedException("Intersection not applicable to Point3D entities");
            }
            if (e1 is Line3D)
            {
                if (e2 is Line3D)
                {
                    return LineWithLine(e1 as Line3D, e2 as Line3D);
                }
                if (e2 is Plane3D)
                {
                    return PlaneWithLine(e2 as Plane3D, e1 as Line3D);
                }
            }
            else if (e1 is Plane3D)
            {
                if (e2 is Line3D)
                {
                    return PlaneWithLine(e1 as Plane3D, e2 as Line3D);
                }
                if (e2 is Plane3D)
                {
                    return PlaneWithPlane(e1 as Plane3D, e2 as Plane3D);
                }
            }
            throw new NotImplementedException();
        }

        public static IGeometricElement3D LineWithLine(Line3D line1, Line3D line2)
        {
            throw new NotImplementedException();
        }

        public static IGeometricElement3D PlaneWithLine(Plane3D plane, Line3D line)
        {
            var origin = line.Origin;
            var num = Vector.Dot(plane.Normal, line.Direction);
            if (Math.Abs(num) >= 1E-08)
            {
                num = -((((plane.A * origin.X) + (plane.B * origin.Y)) + (plane.C * origin.Z)) + plane.D) / num;
                return line.GetPoint(num);
            }
            return null;
        }

        public static IGeometricElement3D PlaneWithPlane(Plane3D plane1, Plane3D plane2)
        {
            throw new NotImplementedException();
        }
    }
}

