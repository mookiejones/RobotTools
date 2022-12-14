using System;

namespace miRobotEditor.Core.Classes.AngleConverter
{
    public class Vector : Matrix
    {
        protected Vector() : base(0, 0)
        {
        }
// ReSharper disable RedundantOverridenMember
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
// ReSharper restore RedundantOverridenMember
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public Vector(Matrix mat)
        {
            if ((mat.Rows != 1) && (mat.Columns != 1))
            {
                throw new MatrixException("Cannot convert matrix to Vector. It has more than one row or column");
            }
            if (mat.Columns == 1)
            {
                mat = mat.Transpose();
            }
            if (mat.Rows != 1) return;
            Size = mat.Columns;
            for (var i = 0; i < mat.Columns; i++)
            {
                this[i] = mat[0, i];
            }
        }

        public Vector(int size) : base(size, 1)
        {
        }

        public Vector(int size, params double[] elements) : base(size, 1, elements)
        {
            if (elements.Length != size)
            {
                throw new MatrixException("Number of elements does not match vector dimension");
            }
        }

        public static double Dot(Vector vec1, Vector vec2)
        {
            if (vec1.Size != vec2.Size)
            {
                throw new MatrixException("Vectors are not of equal size");
            }
            Matrix matrix = vec1.Transpose() * vec2;
            return matrix[0, 0];
        }

        public double Length()
        {
            var d = 0.0;
            for (var i = 0; i < Size; i++)
            {
                d += this[i] * this[i];
            }
            return Math.Sqrt(d);
        }

        public void Normalise()
        {
            var num = Length();
            for (var i = 0; i < Size; i++)
            {
                Vector vector;
                int num3;
                (vector = this)[num3 = i] = vector[num3] / num;
            }
        }

        public Vector Normalised()
        {
            return new Vector(this / Length());
        }

// ReSharper disable FunctionRecursiveOnAllPaths
        public static Vector operator +(Vector v1, Vector v2)
// ReSharper restore FunctionRecursiveOnAllPaths
        {
            return new Vector(v1 + v2);
        }

// ReSharper disable FunctionRecursiveOnAllPaths
        public static Vector operator +(Vector vec, double scalar)
// ReSharper restore FunctionRecursiveOnAllPaths
        {
            return new Vector(vec + scalar);
        }
        public static Vector Add(Vector vec, double scalar)
        {
            return new Vector(vec + scalar);
        }
        public static Vector operator +(double scalar, Vector vec)
        {
            return new Vector(vec + scalar);
        }

// ReSharper disable FunctionRecursiveOnAllPaths
        public static Vector operator /(Vector vec, double scalar)
// ReSharper restore FunctionRecursiveOnAllPaths
        {
            return new Vector(vec / scalar);
        }
        public static Vector Divide(Vector vec, double scalar)
        {
            return new Vector(vec / scalar);
        }
// ReSharper disable FunctionRecursiveOnAllPaths
        public static Vector operator *(Matrix mat, Vector vec)
// ReSharper restore FunctionRecursiveOnAllPaths
        {
            return new Vector(mat * vec);
        }

// ReSharper disable FunctionRecursiveOnAllPaths
        public static Vector operator *(Vector vec, double scalar)
// ReSharper restore FunctionRecursiveOnAllPaths
        {
            return new Vector(vec * scalar);
        }
        public static Vector Multiply(Vector vec, double scalar)
        {
            return new Vector(vec * scalar);
        }
// ReSharper disable FunctionRecursiveOnAllPaths
        public static Vector operator *(double scalar, Vector vec)
// ReSharper restore FunctionRecursiveOnAllPaths
        {
            return new Vector(scalar * vec);
        }

// ReSharper disable FunctionRecursiveOnAllPaths
        public static Vector operator -(Vector v1, Vector v2)
// ReSharper restore FunctionRecursiveOnAllPaths
        {
            return new Vector(v1 - v2);
        }

        public static Vector Subtract(Vector v1, Vector v2)
        {
            return new Vector(v1 - v2);
        }
        public static bool operator ==(Vector v1, Vector v2)
        {
            return (v1 == v2);
        }
        public static bool Equals(Vector v1, Vector v2)
        {
            return (v1 == v2);
        }
        public static bool operator !=(Vector v1, Vector v2)
        {
            return !(v1 == v2);
        }
// ReSharper disable FunctionRecursiveOnAllPaths
        public static Vector operator -(Vector vec, double scalar)
// ReSharper restore FunctionRecursiveOnAllPaths
        {
            return new Vector(vec - scalar);
        }

// ReSharper disable FunctionRecursiveOnAllPaths
        public static Vector operator -(Vector v)
// ReSharper restore FunctionRecursiveOnAllPaths
        {
            return new Vector(-v);
        }
        public static Vector Negate(Vector v)
        {
            return new Vector(-v);
        }
        public double this[int index]
        {
            get
            {
                return base[index, 0];
            }
            set
            {
                base[index, 0] = value;
            }
        }

        public int Size
        {
            get
            {
                return Rows;
            }
            set
            {
                SetSize(value, 1);
            }
        }
    }
}

