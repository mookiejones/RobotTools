using System;

namespace miRobotEditor.Core.Classes.AngleConverter
{
    public static class Project3D
    {
        private const double EPSILON = 0.001;
        public static Point3D PointOntoCircle(Circle3D circle, Point3D point)
        {
            var plane = new Plane3D(circle.Origin, circle.Normal);
            var pointd = PointOntoPlane(plane, point);
            Distance3D.Between(point, pointd);
            var vectord = circle.Origin - pointd;
            vectord.Normalise();
            return new Point3D();
//            return (circle.Origin + ((Point3D) (vectord * circle.Radius)));
        }

        public static Point3D PointOntoLine(Line3D line, Point3D point)
        {
            var vectord = line.Origin - point;
            if (Math.Abs(vectord.Length() - 0.0) < EPSILON)
            {
                return new Point3D(point);
            }
            var num = Vector.Dot(vectord, line.Direction);
          
            //return new Point3D();
                            return (line.Origin + (Vector3D) ((Point3D) (line.Direction * num)));
        }

        public static Point3D PointOntoPlane(Plane3D plane, Point3D point)
        {
            var vectord = plane.Point - point;
            var vectord3 = Vector3D.Cross(Vector3D.Cross(vectord, plane.Normal), plane.Normal).Normalised();
          //  return new Point3D();
            return (plane.Point + (Vector3D) ((Point3D) (vectord3 * Vector.Dot(vectord, vectord3))));
        }

        public static Point3D PointOntoSphere(Sphere3D sphere, Point3D point)
        {
            var vectord = point - sphere.Origin;
            
            vectord.Normalise();
            return new Point3D();
            //                return (sphere.Origin + ((Point3D) (vectord * sphere.Radius)));
        }
    }
}

