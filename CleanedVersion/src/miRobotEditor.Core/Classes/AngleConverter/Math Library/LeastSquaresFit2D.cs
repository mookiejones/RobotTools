using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace miRobotEditor.Core.Classes.AngleConverter
{
    public class LeastSquaresFit2D
    {
        private LeastSquaresFit2D() { }

        private static Point2D Centroid(Collection<Point2D> points)
        {
            var count = points.Count;
            var num2 = 0.0;
            var num3 = 0.0;
            foreach (var pointd in points)
            {
                num2 += pointd.X;
                num3 += pointd.Y;
            }
            return new Point2D(num2 / count, num3 / count);
        }

        public static Circle2D FitCircleToPoints(Collection<Point2D> points)
        {
            var centre = Centroid(points);
            var radius = RmsDistanceToPoint(points, centre);
            const double num2 = 1E-06;
            var matrix = new Matrix(points.Count, 3);
            for (var i = 0; i < 100; i++)
            {
                var x = centre.X;
                var y = centre.Y;
                for (var j = 0; j < points.Count; j++)
                {
                    var num7 = points[j].X;
                    var num8 = points[j].Y;
                    var num9 = Math.Sqrt((((((num7 * num7) - ((2.0 * x) * num7)) + (num8 * num8)) - ((2.0 * y) * num8)) + (x * x)) + (y * y));
                    var num10 = (x - num7) / num9;
                    var num11 = (y - num8) / num9;
                   
                    matrix.SetRow(j, new Vector(3, new[] { num10, num11, -1 }));
                }
                var vector = new Vector(points.Count);
                for (var k = 0; k < points.Count; k++)
                {
                    var num14 = points[k].X;
                    var num15 = points[k].Y;
                    var num16 = 0.0 - (Math.Sqrt(((num14 - x) * (num14 - x)) + ((num15 - y) * (num15 - y))) - radius);
                    vector[k] = num16;
                }
                Matrix matrix2 = new SquareMatrix(matrix.Transpose() * matrix);
                matrix2 = (matrix2 as SquareMatrix).Inverse() * matrix.Transpose();
                matrix2 *= vector.Transpose();
                centre.X += matrix2[0, 0];
                centre.Y += matrix2[1, 0];
                radius += matrix2[2, 0];
                if (matrix2.GetColumn(0).Length() < num2)
                {
                    break;
                }
            }
            return new Circle2D(centre, radius);
        }

        public static Line2D FitLineToPoints(Collection<Point2D> points)
        {
            var origin = (Vector2D) Centroid(points);
            var num = 0.0;
            var num2 = 0.0;
            var num3 = 0.0;
            foreach (var pointd in points)
            {
                var num4 = pointd.X - origin[0];
                var num5 = pointd.Y - origin[1];
                num += num4 * num4;
                num2 += num5 * num5;
                num3 += num4 * num5;
            }
            var mat = new SquareMatrix(2, new[] { num, num3, num2, num3 });
            var svd = new SVD(mat);
            var positiveInfinity = double.PositiveInfinity;
            var column = -1;
            for (var i = 0; i < svd.W.Size; i++)
            {
                if (!(svd.W[i] < positiveInfinity)) continue;
                positiveInfinity = svd.W[i];
                column = i;
            }
            return new Line2D(origin, new Vector2D(svd.V.GetColumn(column)));
        }

        public static double RmsDistanceToPoint(Collection<Point2D> points, Point2D centre)
        {
            var num = points.Sum(pointd => Math.Pow((pointd - centre).Length(), 2.0));
            return (num / points.Count);
        }
    }
}

