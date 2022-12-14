using System;
using System.Collections.ObjectModel;

namespace miRobotEditor.Core.Classes.AngleConverter
{
#pragma warning disable 660,661
    public sealed class Vector3D : Vector, IGeometricElement3D
#pragma warning restore 660,661
    {

// ReSharper disable RedundantOverridenMember
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
// ReSharper restore RedundantOverridenMember
        public Vector3D() : base(3)
        {
        }

// ReSharper disable RedundantOverridenMember
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
// ReSharper restore RedundantOverridenMember
  
        public Vector3D(Matrix mat) : base(3)
        {
            if ((mat.Rows != 1) && (mat.Columns != 1))
            {
                throw new MatrixException("Cannot convert Matrix to Vector3D. It has more than one row or column");
            }
            if ((mat.Columns == 3) && (mat.Rows == 1))
            {
                mat = mat.Transpose();
            }
            for (var i = 0; i < 3; i++)
            {
                base[i] = mat[i, 0];
            }
        }

        public Vector3D(Vector vec) : base(3)
        {
            for (var i = 0; i < 3; i++)
            {
                base[i] = vec[i];
            }
        }

        public Vector3D(double x, double y, double z) : base(3, new[] { x, y, z })
        {
        }

        public static double Angle(Vector vec1, Vector vec2)
        {
            var num = Dot(vec1, vec2);
            var num2 = vec1.Length() * vec2.Length();
            return ((Math.Acos(num / num2) * 180.0) / Math.PI);
        }

        public static Vector3D Cross(Vector vec1, Vector vec2)
        {
            var vectord = new Vector3D();
            vectord[0] = (vec1[1] * vec2[2]) - (vec1[2] * vec2[1]);
            vectord[1] = (vec1[2] * vec2[0]) - (vec1[0] * vec2[2]);
            vectord[2] = (vec1[0] * vec2[1]) - (vec1[1] * vec2[0]);
            return vectord;
        }

        public static Vector3D NaN()
        {
            return new Vector3D(NaN(3, 1));
        }

        public new Vector3D Normalised()
        {
            return new Vector3D((Matrix) (this / Length()));
        }
        
// ReSharper disable FunctionRecursiveOnAllPaths
        public static Vector3D operator +(Vector3D v1, Vector3D v2)
// ReSharper restore FunctionRecursiveOnAllPaths
        {
            return new Vector3D(v1 + v2);
        }
       
        public static Vector3D Add(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1 + v2);
        }
// ReSharper disable FunctionRecursiveOnAllPaths
        public static Vector3D operator /(Vector3D vec, double scalar)
// ReSharper restore FunctionRecursiveOnAllPaths
        {
            return new Vector3D((Matrix) (vec / scalar));
        }
        public static Vector3D Divide(Vector3D vec, double scalar)
        {
            return new Vector3D((Matrix)(vec / scalar));
        }
        public static explicit operator Point3D(Vector3D vec)
        {
            return new Point3D(vec);
        }
        public static Point3D ToPoint3D(Vector3D vec)
        {
            return new Point3D(vec);
        }
       
// ReSharper disable FunctionRecursiveOnAllPaths
        public static Vector3D operator *(Vector3D vec, double scalar)
// ReSharper restore FunctionRecursiveOnAllPaths
        {
            return new Vector3D((Matrix) (vec * scalar));
        }
        public static Vector3D Multiply(Vector3D vec, double scalar)
        {
            return new Vector3D((Matrix)(vec * scalar));
        }
        public static Collection<Vector3D> operator *(Collection<TransformationMatrix3D> transforms, Vector3D vector)
        {
            var list = new Collection<Vector3D>();
            foreach (var matrixd in transforms)
            {
                list.Add(matrixd * vector);
            }
            return list;
        }

// ReSharper disable FunctionRecursiveOnAllPaths
        public static Vector3D operator -(Vector3D v1, Vector3D v2)
// ReSharper restore FunctionRecursiveOnAllPaths
        {
            return new Vector3D(v1 - v2);
        }
        public static Boolean operator ==(Vector3D v1, Vector3D v2)
        {
            return v1 == v2;
        }
        public static Boolean operator !=(Vector3D v1, Vector3D v2)
        {
            return !(v1 == v2);
        }
        public static Vector3D Subtract(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1 - v2);
        }
        public static Vector3D operator -(Vector3D vec)
        {
            return Negate(vec);
        }
        public static Vector3D Negate(Vector3D vec)
        {
            return new Vector3D(-vec.X, -vec.Y, -vec.Z);
        }
        TransformationMatrix3D IGeometricElement3D.Position
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public double X
        {
            get
            {
                return base[0];
            }
            set
            {
                base[0] = value;
            }
        }

        public double Y
        {
            get
            {
                return base[1];
            }
            set
            {
                base[1] = value;
            }
        }

        public double Z
        {
            get
            {
                return base[2];
            }
            set
            {
                base[2] = value;
            }
        }
    }
}

