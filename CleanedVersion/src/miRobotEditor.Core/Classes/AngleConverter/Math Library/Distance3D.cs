using System;

namespace miRobotEditor.Core.Classes.AngleConverter
{
    public static class Distance3D
    {
        enum TYP3D { Point3D, Line3D, Plane3D, Circle3D, Sphere3D,None }

// ReSharper disable InconsistentNaming
        private static TYP3D getType(IGeometricElement3D geo)
// ReSharper restore InconsistentNaming
        {
            if (geo is Point3D)
                return TYP3D.Point3D;
            if(geo is Line3D)
                return TYP3D.Line3D;
            if(geo is Plane3D)
                return TYP3D.Plane3D;
            if (geo is Circle3D)
                return TYP3D.Circle3D;
            if (geo is Sphere3D)
                return TYP3D.Sphere3D;


            return TYP3D.None;
        }
        public static double Between(IGeometricElement3D e1, IGeometricElement3D e2)
        {
            if (e1 is Point3D)
            {
                var point3D = e1 as Point3D;
                switch (getType(e2))
                {
                    case TYP3D.Point3D:
                        return PointToPoint(point3D, e2 as Point3D);
                    case TYP3D.Line3D:
                        return PointToLine(e2 as Line3D, point3D);
                    case TYP3D.Plane3D:
                        return PointToPlane(e2 as Plane3D, point3D);
                    case TYP3D.Circle3D:
                        return PointToCircle(e2 as Circle3D, point3D);
                    case TYP3D.Sphere3D:
                        return PointToSphere(e2 as Sphere3D, point3D);
                }

            }
            else if (e1 is Line3D)
            {
                var line3D = e1 as Line3D;
                switch (getType(e2))
                {
                    case TYP3D.Point3D:
                        return PointToLine(line3D, e2 as Point3D);
                    case TYP3D.Line3D:
                        return LineToLine(line3D, e2 as Line3D);
                    case TYP3D.Plane3D:
                    case TYP3D.Sphere3D:
                        throw new NotImplementedException();
                    case TYP3D.Circle3D:
                        return LineToCircle(e2 as Circle3D, line3D);
                }

            }
            else if (e1 is Plane3D)
            {
                var plane3D = e1 as Plane3D;
                switch (getType(e2))
                {
                    case TYP3D.Point3D:
                        return PointToPlane(plane3D, e2 as Point3D);
                    case TYP3D.Line3D:
                    case TYP3D.Plane3D:
                    case TYP3D.Circle3D:
                    case TYP3D.Sphere3D:
                        throw new NotImplementedException();
                }

            }
            else if (e1 is Circle3D)
            {
                var circle3D = e1 as Circle3D;
                switch (getType(e2))
                {
                    case TYP3D.Point3D:
                        return PointToCircle(circle3D, e2 as Point3D);
                    case TYP3D.Line3D:
                    case TYP3D.Plane3D:
                    case TYP3D.Circle3D:
                    case TYP3D.Sphere3D:
                        throw new NotImplementedException();
                }
            }
            else if (e1 is Sphere3D)
            {
                var sphere3D = e1 as Sphere3D;
                switch (getType(e2))
                {
                    case TYP3D.Point3D:
                        return PointToSphere(sphere3D, e2 as Point3D);
                    case TYP3D.Line3D:
                    case TYP3D.Plane3D:
                    case TYP3D.Circle3D:
                    case TYP3D.Sphere3D:
                        throw new NotImplementedException();
                }
                throw new NotImplementedException();
                
            }
            return -1;
        }

// ReSharper disable UnusedParameter.Local
        private static double LineToCircle(Circle3D circle, Line3D point)
// ReSharper restore UnusedParameter.Local
        {
            throw new NotImplementedException();
        }

        private static double LineToLine(Line3D line1, Line3D line2)
        {
            Point3D pointd;
            Point3D pointd2;
            return LineToLine(line1, line2, out pointd, out pointd2);
        }

        private static double LineToLine(Line3D line1, Line3D line2, out Point3D closestPoint1, out Point3D closestPoint2)
        {
            double num7;
            double num8;
            double num9;
            var origin = line1.Origin;
            var pointd2 = line2.Origin;
            var direction = line1.Direction;
            var vectord2 = line2.Direction;
            var vectord3 = origin - pointd2;
            var num = Vector.Dot(-direction, vectord2);
            var num2 = Vector.Dot(vectord3, direction);
            var num3 = vectord3.Length() * vectord3.Length();
            var num4 = Math.Abs(1.0 - (num * num));
            if (num4 > 1E-05)
            {
                var num5 = Vector.Dot(-vectord3, vectord2);
                var num6 = 1.0 / num4;
                num7 = ((num * num5) - num2) * num6;
                num8 = ((num * num2) - num5) * num6;
                num9 = ((num7 * ((num7 + (num * num8)) + (2.0 * num2))) + (num8 * (((num * num7) + num8) + (2.0 * num5)))) + num3;
            }
            else
            {
                num7 = -num2;
                num8 = 0.0;
                num9 = (num2 * num7) + num3;
            }
            closestPoint1 = origin + new Vector3D(num7 * direction);
            closestPoint2 = pointd2 + new Vector3D(num8 * vectord2);
            return Math.Sqrt(num9);
        }

        public static double PointToCircle(Circle3D circle, Point3D point)
        {
            var plane = new Plane3D(circle.Origin, circle.Normal);
            var pointd = Project3D.PointOntoPlane(plane, point);
            Between(point, pointd);
            var vectord = circle.Origin - pointd;
            vectord.Normalise();
            //Point3D pointd2 = circle.Origin + ((Point3D) (vectord * circle.Radius));
            var pointd2 = new Point3D();
            return PointToPoint(point, pointd2);
        }

        private static double PointToLine(Line3D line, Point3D point)
        {
            return Vector3D.Cross(line.Direction, line.Origin - point).Length();
        }

        private static double PointToPlane(Plane3D plane, Point3D point)
        {
            return Vector.Dot(plane.Normal, point - plane.Point);
        }

        private static double PointToPoint(Point3D p1, Point3D p2)
        {
            return ((p1 - p2)).Length();
        }

        public static double PointToSphere(Sphere3D sphere, Point3D point)
        {
            return (((point - sphere.Origin)).Length() - sphere.Radius);
        }
    }
}

