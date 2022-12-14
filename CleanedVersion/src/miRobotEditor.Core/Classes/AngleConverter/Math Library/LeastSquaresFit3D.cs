using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace miRobotEditor.Core.Classes.AngleConverter
{
    [Localizable(false)]
    public sealed class LeastSquaresFit3D
    {
        private Collection<Point3D> _measuredPoints;
        private NRSolver _solver;

        private void CalculateErrors(IList<Point3D> points, IGeometricElement3D element)
        {
            Errors = new Collection<double>();
            var num = 0.0;
            var num2 = 0.0;
            MaxError = 0.0;
            PointWithLargestError = -1;
            for (var i = 0; i < points.Count; i++)
            {
                var pointd = points[i];
                var item = Distance3D.Between(element, pointd);
                Errors.Add(item);
                num += item;
                num2 += item * item;
                if (!(item > MaxError)) continue;
                MaxError = item;
                PointWithLargestError = i;
            }
            var count = points.Count;
            AverageError = num / count;
            StandardDeviationError = Math.Sqrt((count * num2) - (AverageError * AverageError)) / count;
        }

        public Point3D Centroid(Collection<Point3D> points)
        {
            var count = points.Count;
            var num2 = 0.0;
            var num3 = 0.0;
            var num4 = 0.0;
            foreach (var pointd in points)
            {
                num2 += pointd.X;
                num3 += pointd.Y;
                num4 += pointd.Z;
            }
            var element = new Point3D(num2 / count, num3 / count, num4 / count);
            Transform = new TransformationMatrix3D(new Vector3D(element.X, element.Y, element.Z), new RotationMatrix3D());
            CalculateErrors(points, element);
            return element;
        }

        private Vector Circle3DErrorFunction(Vector vec)
        {
            var vector = new Vector(_solver.NumEquations);
            var num = 0;
            var point = new Point3D(vec[0], vec[1], vec[2]);
            var plane = new Plane3D(point, new Vector3D(vec[3], vec[4], vec[5]));
            foreach (var pointd2 in _measuredPoints)
            {
                var pointd3 = Project3D.PointOntoPlane(plane, pointd2);
                var vectord = point - pointd3;
                vectord.Normalise();
                var pointd4 = new Point3D();
//                Point3D pointd4 = point + ((Point3D) (vectord * num2));
                vector[num++] = pointd2.X - pointd4.X;
                vector[num++] = pointd2.Y - pointd4.Y;
                vector[num++] = pointd2.Z - pointd4.Z;
            }
            vector[num] = new Vector3D(vec[3], vec[4], vec[5]).Length() - 1.0;
            return vector;
        }

        public Circle3D FitCircleToPoints(Collection<Point3D> points)
        {
            if (points == null) throw new MatrixNullReference();
            if (points.Count < 3)
            {
                throw new ArgumentException("Need at least 3 points to fit circle");
            }
            _solver = new NRSolver((points.Count * 3) + 1, 7);
            _measuredPoints = points;
            var fitd = new LeastSquaresFit3D();
            var planed = fitd.FitPlaneToPoints(points);
            var initialGuess = new Circle3D {
                Origin = fitd.Centroid(points),
                Normal = planed.Normal,
                Radius = fitd.AverageError,
//                Radius = fitd.RMSError
            };
            var vector = _solver.Solve(Circle3DErrorFunction, VectorFromCircle3D(initialGuess));
            var element = new Circle3D {
                Origin = new Point3D(vector[0], vector[1], vector[2]),
                Normal = new Vector3D(vector[3], vector[4], vector[5]),
                Radius = vector[6]
            };
            CalculateErrors(points, element);
            return element;
        }

        public Circle3D FitCircleToPoints2(Collection<Point3D> points)
        {
            if (points == null) throw new MatrixNullReference();

            if (points.Count < 3)
            {
                throw new ArgumentException("Need at least 3 points to fit circle");
            }
            var circle = new Circle3D();
            var fitd = new LeastSquaresFit3D();
            var matrix = new Matrix(points.Count, 7);
            var vector = new Vector(points.Count);
            for (var i = 0; i < 50; i++)
            {
                circle.Origin = fitd.Centroid(points);
                circle.Radius = fitd.RmsError;
                var plane = fitd.FitPlaneToPoints(points);
                circle.Normal = plane.Normal;
                var num2 = 0;
                foreach (var pointd in points)
                {
                    var pointd2 = Project3D.PointOntoPlane(plane, pointd);
                    var vectord = circle.Origin - pointd2;
                    vectord.Normalise();
                    matrix[num2, 0] = vectord[0];
                    matrix[num2, 1] = vectord[1];
                    matrix[num2, 2] = vectord[2];
                    matrix[num2, 3] = plane.Normal.X;
                    matrix[num2, 4] = plane.Normal.Y;
                    matrix[num2, 5] = plane.Normal.Z;
                    matrix[num2, 6] = -1.0;
                    var num3 = Distance3D.PointToCircle(circle, pointd);
                    vector[num2] = num3;
                    num2++;
                }
                var vector2 = matrix.PseudoInverse() * vector;
                if (vector2.Length() < 1E-06)
                {
                    break;
                }
                var origin = circle.Origin;
                origin.X += vector2[0];
                var pointd3 = circle.Origin;
                pointd3.Y += vector2[1];
                var pointd4 = circle.Origin;
                pointd4.Z += vector2[2];
                var normal = circle.Normal;
                normal.X += vector2[3];
                var vectord2 = circle.Normal;
                vectord2.Y += vector2[4];
                var vectord3 = circle.Normal;
                vectord3.Z += vector2[5];
                circle.Radius -= vector2[6];
            }
            CalculateErrors(points, circle);
            return circle;
        }

        public Line3D FitLineToPoints(Collection<Point3D> points)
        {
            if (points == null) throw new MatrixNullReference();

            var origin = Centroid(points);
            var num = 0.0;
            var num2 = 0.0;
            var num3 = 0.0;
            var num4 = 0.0;
            var num5 = 0.0;
            var num6 = 0.0;
            foreach (var pointd2 in points)
            {
                var num7 = pointd2.X - origin.X;
                var num8 = pointd2.Y - origin.Y;
                var num9 = pointd2.Z - origin.Z;
                num += num7 * num7;
                num2 += num8 * num8;
                num3 += num9 * num9;
                num4 += num7 * num8;
                num5 += num8 * num9;
                num6 += num7 * num9;
            }
            var mat = new SquareMatrix(3, new[] { num2 + num3, -num4, -num6, -num4, num3 + num, -num5, -num6, -num5, num + num2 });
            var svd = new SVD(mat);
            var direction = new Vector3D(svd.U.GetColumn(svd.SmallestSingularIndex));
            var element = new Line3D(origin, direction);
            CalculateErrors(points, element);
            return element;
        }

        public Plane3D FitPlaneToPoints(Collection<Point3D> points)
        {
            if (points == null) throw new NullReferenceException();

            if (points.Count < 3)
            {
                throw new MatrixException("Not enough points to fit a plane");
            }
            var point = Centroid(points);
            var num = 0.0;
            var num2 = 0.0;
            var num3 = 0.0;
            var num4 = 0.0;
            var num5 = 0.0;
            var num6 = 0.0;
            foreach (var pointd2 in points)
            {
                var num7 = pointd2.X - point.X;
                var num8 = pointd2.Y - point.Y;
                var num9 = pointd2.Z - point.Z;
                num += num7 * num7;
                num2 += num8 * num8;
                num3 += num9 * num9;
                num4 += num7 * num8;
                num5 += num8 * num9;
                num6 += num7 * num9;
            }
            var mat = new SquareMatrix(3, new[] { num, num4, num6, num4, num2, num5, num6, num5, num3 });
            var svd = new SVD(mat);
            var normal = new Vector3D(svd.U.GetColumn(svd.SmallestSingularIndex));
            var element = new Plane3D(point, normal);
            CalculateErrors(points, element);
            return element;
        }

        public Sphere3D FitSphereToPoints(Collection<Point3D> points)
        {
            if (points == null) throw new NullReferenceException();

            if (points.Count < 4)
            {
                throw new MatrixException("Need at least 4 points to fit sphere");
            }
            var sphere = new Sphere3D();
            var fitd = new LeastSquaresFit3D();
            sphere.Origin = fitd.Centroid(points);
            sphere.Radius = fitd.RmsError;
            var matrix = new Matrix(points.Count, 4);
            var vector = new Vector(points.Count);
            for (var i = 0; i < 50; i++)
            {
                var num2 = 0;
                foreach (var pointd in points)
                {
                    var vectord = Project3D.PointOntoSphere(sphere, pointd) - sphere.Origin;
                    vectord.Normalise();
                    matrix[num2, 0] = vectord.X;
                    matrix[num2, 1] = vectord.Y;
                    matrix[num2, 2] = vectord.Z;
                    matrix[num2, 3] = -1.0;
                    var num3 = Distance3D.PointToSphere(sphere, pointd);
                    vector[num2] = num3;
                    num2++;
                }
                var vector2 = matrix.PseudoInverse() * vector;
                if (vector2.Length() < 1E-06)
                {
                    break;
                }
                var origin = sphere.Origin;
                origin.X += vector2[0];
                var pointd3 = sphere.Origin;
                pointd3.Y += vector2[1];
                var pointd4 = sphere.Origin;
                pointd4.Z += vector2[2];
                sphere.Radius -= vector2[3];
            }
            CalculateErrors(points, sphere);
            return sphere;
        }

        private static Vector VectorFromCircle3D(Circle3D initialGuess)
        {
            var vector = new Vector(7);
            vector[0] = initialGuess.Origin.X;
            vector[1] = initialGuess.Origin.Y;
            vector[2] = initialGuess.Origin.Z;
            vector[3] = initialGuess.Normal.X;
            vector[4] = initialGuess.Normal.Y;
            vector[5] = initialGuess.Normal.Z;
            vector[6] = initialGuess.Radius;
            return vector;
        }

        private double AverageError { get; set; }

        private Collection<double> Errors { get; set; }

        private double MaxError { get; set; }

        private int PointWithLargestError {  get; set; }

        private double RmsError
        {
            get
            {
                var d = Errors.Sum(num2 => num2*num2);
                d /= Errors.Count;
                return Math.Sqrt(d);
            }
        }

        private double StandardDeviationError { get; set; }

        private TransformationMatrix3D Transform {  get; set; }
    }
}

