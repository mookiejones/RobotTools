using System;
using System.ComponentModel;

namespace miRobotEditor.Core.Structs
{
    /// <summary>
    /// A line/column position.
    /// NRefactory lines/columns are counting from one.
    /// </summary>
    public struct Location : IComparable<Location>, IEquatable<Location>
    {
        public static readonly Location Empty = new Location(-1, -1);

        public Location(int column, int line)
        {
            _x = column;
            _y = line;
        }

        private int _x;
        private int _y;
        public int X { get { return _x; } set { _x = value; } }
        public int Y { get { return _y; } set { _y = value; } }

        public int Line
        {
            get { return Y; }
            set { Y = value; }
        }

        public int Column
        {
            get { return X; }
            set { X = value; }
        }

        public bool IsEmpty
        {
            get
            {
                return X <= 0 && Y <= 0;
            }
        }

        [Localizable(false)]
        public override string ToString()
        {
            return string.Format("(Line {1}, Col {0})", X, Y);
        }

        public override int GetHashCode()
        {
            return unchecked(87 * X.GetHashCode() ^ Y.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Location)) return false;
            return (Location)obj == this;
        }

        public bool Equals(Location other)
        {
            return this == other;
        }

        public static bool operator ==(Location a, Location b)
        {
            return a.X == b._x && a.Y == b._y;
        }

        public static bool operator !=(Location a, Location b)
        {
            return a.X != b._x || a.Y != b._y;
        }

        public static bool operator <(Location a, Location b)
        {
            if (a.Y < b._y)
                return true;
            if (a.Y == b._y)
                return a.X < b._x;
            return false;
        }

        public static bool operator >(Location a, Location b)
        {
            if (a.Y > b._y)
                return true;
            if (a.Y == b._y)
                return a.X > b._x;
            return false;
        }

        public static bool operator <=(Location a, Location b)
        {
            return !(a > b);
        }

        public static bool operator >=(Location a, Location b)
        {
            return !(a < b);
        }

        public int CompareTo(Location other)
        {
            if (this == other)
                return 0;
            if (this < other)
                return -1;
            return 1;
        }
    }

}
