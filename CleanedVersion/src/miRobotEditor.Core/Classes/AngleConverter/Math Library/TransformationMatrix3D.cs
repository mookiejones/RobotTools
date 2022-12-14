using System;
using System.Collections.ObjectModel;

namespace miRobotEditor.Core.Classes.AngleConverter
{
    [Serializable]
    public class TransformationMatrix3D : SquareMatrix
    {
        public TransformationMatrix3D() : base(4)
        {
            for (var i = 0; i < 4; i++)
            {
                base[i, i] = 1.0;
            }
        }

        public TransformationMatrix3D(Matrix mat) : base(4)
        {
            if ((mat.Rows != 4) || (mat.Columns != 4))
            {
                throw new MatrixException("Matrix is not the correct size to convert to a TransformationMatrix3D");
            }
            var matrix = new SquareMatrix(3);
            for (var i = 0; i < 3; i++)
            {
                for (var k = 0; k < 3; k++)
                {
                    matrix[i, k] = mat[i, k];
                }
            }
            if (!matrix.IsRotationMatrix())
            {
                throw new MatrixException("Matrix does not contain a valid rotation");
            }
            for (var j = 0; j < 4; j++)
            {
                for (var m = 0; m < 4; m++)
                {
                    base[j, m] = mat[j, m];
                }
            }
        }

        public TransformationMatrix3D(RotationMatrix3D rot) : base(4)
        {
            base[3, 3] = 1.0;
            Rotation = rot;
        }

        public TransformationMatrix3D(Vector3D trans) : base(4)
        {
            for (var i = 0; i < 4; i++)
            {
                base[i, i] = 1.0;
            }
            Translation = trans;
        }

        public TransformationMatrix3D(Vector3D trans, RotationMatrix3D rot) : base(4)
        {
            for (var i = 0; i < 4; i++)
            {
                base[i, i] = 1.0;
            }
            Rotation = rot;
            Translation = trans;
        }

// ReSharper disable once UnusedMember.Global
        public static TransformationMatrix3D FromXYZABC(double x, double y, double z, double a, double b, double c)
        {
            return new TransformationMatrix3D(new Vector3D(x, y, z), RotationMatrix3D.FromABC(a, b, c));
        }

// ReSharper disable once UnusedMember.Global
        public static TransformationMatrix3D FromXYZEulerZYZ(double x, double y, double z, double z1, double y1, double z2)
        {
            return new TransformationMatrix3D(new Vector3D(x, y, z), RotationMatrix3D.FromEulerZYZ(z1, y1, z2));
        }

// ReSharper disable once UnusedMember.Global
        public static TransformationMatrix3D FromXYZRPY(double x, double y, double z, double r, double p, double w)
        {
            return new TransformationMatrix3D(new Vector3D(x, y, z), RotationMatrix3D.FromRPY(r, p, w));
        }

        public static TransformationMatrix3D Identity()
        {
            var matrixd = new TransformationMatrix3D();
            for (var i = 0; i < 4; i++)
            {
                matrixd[i, i] = 1.0;
            }
            return matrixd;
        }

        public new TransformationMatrix3D Inverse()
        {
            return new TransformationMatrix3D(base.Inverse());
        }

        public static TransformationMatrix3D NaN()
        {
            return new TransformationMatrix3D(NaN(4));
        }

        public static Collection<Point3D> operator *(TransformationMatrix3D transform, Collection<Point3D> points)
        {
            var list = new Collection<Point3D>();
            foreach (var pointd in points)
            {
                list.Add(transform * pointd);
            }
            return list;
        }
        public static Collection<Point3D> Multiply(TransformationMatrix3D transform, Collection<Point3D> points)
        {
            var list = new Collection<Point3D>();
            foreach (var pointd in points)
            {
                list.Add(transform * pointd);
            }
            return list;
        }
        public static Collection<Vector3D> operator *(TransformationMatrix3D transform, Collection<Vector3D> vectors)
        {
            var list = new Collection<Vector3D>();
            foreach (var vectord in vectors)
            {
                list.Add(transform * vectord);
            }
            return list;
        }

        public static Point3D operator *(TransformationMatrix3D mat, Point3D pt)
        {
            var vectord = new Vector3D(pt.X, pt.Y, pt.Z);
            var vectord2 = mat * vectord;
            return new Point3D(vectord2.X, vectord2.Y, vectord2.Z);
        }

// ReSharper disable FunctionRecursiveOnAllPaths
        public static TransformationMatrix3D operator *(TransformationMatrix3D m1, TransformationMatrix3D m2)
// ReSharper restore FunctionRecursiveOnAllPaths
        {
            var result = new TransformationMatrix3D(m1*m2);
            return result;
        }

        public static Vector3D operator *(TransformationMatrix3D mat, Vector3D vec)
        {
            var vector = new Vector(4, new[] { vec[0], vec[1], vec[2], 1.0 });
            Matrix matrix = mat * vector;
            return new Vector3D(matrix[0, 0], matrix[1, 0], matrix[2, 0]);
        }

        public virtual string ToString(string format)
        {
            return ToString(format, null);
        }

        public override string ToString(string format, IFormatProvider formatProvider)
        {
          
            if (format.ToUpperInvariant().StartsWith("RPY"))
            {
                var translation = Translation;
                var rpy = Rotation.RPY;
                return string.Format("{0:F2}, {1:F2}, {2:F2}, {3:F2}, {4:F2}, {5:F2}", new object[] { translation.X, translation.Y, translation.Z, rpy.X, rpy.Y, rpy.Z });
            }
            if (format.ToUpperInvariant().StartsWith("ABC"))
            {
                var vectord3 = Translation;
                var abc = Rotation.ABC;
                return string.Format("{0:F2}, {1:F2}, {2:F2}, {3:F2}, {4:F2}, {5:F2}", new object[] { vectord3.X, vectord3.Y, vectord3.Z, abc.X, abc.Y, abc.Z });
            }
            if (format.ToUpperInvariant().StartsWith("QUATERNION"))
            {
                var vectord5 = Translation;
                var rotation = (Quaternion) Rotation;
                return string.Format("{0:F2}, {1:F2}, {2:F2}, {3:F3}, {4:F3}, {5:F3}, {6:F3}", new object[] { vectord5.X, vectord5.Y, vectord5.Z, rotation.X, rotation.Y, rotation.Z, rotation.W });
            }
            if (format.ToUpperInvariant().StartsWith("ABBQUATERNION"))
            {
                var vectord6 = Translation;
                var quaternion2 = (Quaternion) Rotation;
                return string.Format("{0:F2}, {1:F2}, {2:F2}, {3:F3}, {4:F3}, {5:F3}, {6:F3}", new object[] { vectord6.X, vectord6.Y, vectord6.Z, quaternion2.W, quaternion2.X, quaternion2.Y, quaternion2.Z });
            }
            if (format.ToUpperInvariant().StartsWith("ABG"))
            {
                var vectord7 = Translation;
                var abg = Rotation.ABG;
                return string.Format("{0:F2}, {1:F2}, {2:F2}, {3:F2}, {4:F2}, {5:F2}", new object[] { vectord7.X, vectord7.Y, vectord7.Z, abg.X, abg.Y, abg.Z });
            }
            if (format.ToUpperInvariant().StartsWith("EULERZYZ"))
            {
                var vectord9 = Translation;
                var eulerZYZ = Rotation.EulerZYZ;
                return string.Format("{0:F2}, {1:F2}, {2:F2}, {3:F2}, {4:F2}, {5:F2}", new object[] { vectord9.X, vectord9.Y, vectord9.Z, eulerZYZ.X, eulerZYZ.Y, eulerZYZ.Z });
            }
            return base.ToString(format, formatProvider);
        }

        public RotationMatrix3D Rotation
        {
            get
            {
                var matrixd = new RotationMatrix3D();
                for (var i = 0; i < 3; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        matrixd[i, j] = base[i, j];
                    }
                }
                return matrixd;
            }
            set
            {
                for (var i = 0; i < 3; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        base[i, j] = value[i, j];
                    }
                }
            }
        }

        public Vector3D Translation
        {
            get
            {
                return new Vector3D(base[0, 3], base[1, 3], base[2, 3]);
            }
            set
            {
                base[0, 3] = value[0];
                base[1, 3] = value[1];
                base[2, 3] = value[2];
            }
        }
    }
}

