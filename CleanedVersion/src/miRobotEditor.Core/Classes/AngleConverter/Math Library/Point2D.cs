using System;
using System.ComponentModel;
using System.Globalization;

namespace miRobotEditor.Core.Classes.AngleConverter
{
    [Localizable(false)]
    public sealed class Point2D : IFormattable
    {
        private bool Equals(Point2D other)
        {
            return Equals(Position, other.Position);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Point2D) obj);
        }

        public Point2D()
        {
            Position = new Vector2D();
        }
        public override int GetHashCode()
        {
// ReSharper disable BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
// ReSharper restore BaseObjectGetHashCodeCallInGetHashCode
        }

        public Point2D(Vector2D position)
        {
            Position = position;
        }

        public Point2D(double x, double y)
        {
            Position = new Vector2D(x, y);
        }
       
        public static bool operator ==(Point2D p1, Point2D p2)
        {
            return p1 == p2;
        }
        public static bool Equals(Point2D p1, Point2D p2)
        {
            return p1 == p2;
        }
        public static bool operator !=(Point2D p1, Point2D p2)
        {
            return (!(p1 == p2));
        }
        public static implicit operator Vector2D(Point2D point)
        {
            return new Vector2D(point.Position);
        }

        public static Vector2D operator -(Point2D p1, Point2D p2)
        {
            return new Vector2D(p2.X - p1.X, p2.Y - p1.Y);
        }
        public static Vector2D Subtract(Point2D p1, Point2D p2)
        {
            return new Vector2D(p2.X - p1.X, p2.Y - p1.Y);
        }

        [Localizable(false)]
        public override string ToString()
        {
            return string.Format("{0:F2}, {1:F2}", X, Y);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("{0}, {1}", X.ToString(format, CultureInfo.InvariantCulture), Y.ToString(format, CultureInfo.InvariantCulture));
        }

        public Vector2D Position { get; set; }

        public double X
        {
            get
            {
                return Position[0];
            }
            set
            {
                Position[0] = value;
            }
        }

        public double Y
        {
            get
            {
                return Position[1];
            }
            set
            {
                Position[1] = value;
            }
        }
    }
}

