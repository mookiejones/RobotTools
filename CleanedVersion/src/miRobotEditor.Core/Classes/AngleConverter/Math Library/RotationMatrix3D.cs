using System;

namespace miRobotEditor.Core.Classes.AngleConverter
{
    public sealed class RotationMatrix3D : SquareMatrix
    {
        
        public RotationMatrix3D() : base(3)
        {
            for (var i = 0; i < 3; i++)
            {
                base[i, i] = 1.000;
            }
        }

        public RotationMatrix3D(Matrix mat) : base(3)
        {
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    base[i, j] = mat[i, j];
                }
            }
        }

        public static RotationMatrix3D FromABC(double a, double b, double c)
        {
            return FromRPY(c, b, a);
        }

        public static RotationMatrix3D FromEulerZYZ(double x, double y, double z)
        {
            var matrixd = new RotationMatrix3D();
            var xval = Math.Cos((x * Math.PI) / 180.0);
            var yval = Math.Cos((y * Math.PI) / 180.0);
            var zval = Math.Cos((z * Math.PI) / 180.0);
            var aval = Math.Sin((x * Math.PI) / 180.0);
            var bval = Math.Sin((y * Math.PI) / 180.0);
            var cval = Math.Sin((z * Math.PI) / 180.0);
            matrixd[0, 0] = ((xval * yval) * zval) - (aval * cval);
            matrixd[0, 1] = ((-xval * yval) * cval) - (aval * zval);
            matrixd[0, 2] = xval * bval;
            matrixd[1, 0] = ((aval * yval) * zval) + (xval * cval);
            matrixd[1, 1] = ((-aval * yval) * cval) + (xval * zval);
            matrixd[1, 2] = aval * bval;
            matrixd[2, 0] = -bval * zval;
            matrixd[2, 1] = bval * cval;
            matrixd[2, 2] = yval;
            return matrixd;
        }

// ReSharper disable once InconsistentNaming
        public static RotationMatrix3D FromRPY(double roll, double pitch, double yaw)
        {
            return new RotationMatrix3D((RotateZ(yaw) * RotateY(pitch)) * RotateX(roll));
        }

        public static RotationMatrix3D Identity()
        {
            return new RotationMatrix3D(Identity(3));
        }

        public new RotationMatrix3D Inverse()
        {
            return new RotationMatrix3D(base.Inverse());
        }

        public static explicit operator Quaternion(RotationMatrix3D mat)
        {
            var quaternion = new Quaternion();
            var d = mat.Trace() + 1.0;
            if (d > 1E-05)
            {
                var num2 = Math.Sqrt(d) * 2.0;
                quaternion.X = (mat[2, 1] - mat[1, 2]) / num2;
                quaternion.Y = (mat[0, 2] - mat[2, 0]) / num2;
                quaternion.Z = (mat[1, 0] - mat[0, 1]) / num2;
                quaternion.W = 0.25 * num2;
            }
            else if ((mat[0, 0] > mat[1, 1]) && (mat[0, 0] > mat[2, 2]))
            {
                var num3 = Math.Sqrt(((1.0 + mat[0, 0]) - mat[1, 1]) - mat[2, 2]) * 2.0;
                quaternion.X = 0.25 * num3;
                quaternion.Y = (mat[0, 1] + mat[1, 0]) / num3;
                quaternion.Z = (mat[2, 0] + mat[0, 2]) / num3;
                quaternion.W = (mat[1, 2] - mat[2, 1]) / num3;
            }
            else if (mat[1, 1] > mat[2, 2])
            {
                var num4 = Math.Sqrt(((1.0 + mat[1, 1]) - mat[0, 0]) - mat[2, 2]) * 2.0;
                quaternion.X = (mat[0, 1] + mat[1, 0]) / num4;
                quaternion.Y = 0.25 * num4;
                quaternion.Z = (mat[1, 2] + mat[2, 1]) / num4;
                quaternion.W = (mat[2, 0] - mat[0, 2]) / num4;
            }
            else
            {
                var num5 = Math.Sqrt(((1.0 + mat[2, 2]) - mat[0, 0]) - mat[1, 1]) * 2.0;
                quaternion.X = (mat[2, 0] + mat[0, 2]) / num5;
                quaternion.Y = (mat[1, 2] + mat[2, 1]) / num5;
                quaternion.Z = 0.25 * num5;
                quaternion.W = (mat[0, 1] - mat[1, 0]) / num5;
            }
            quaternion.Normalise();
            return quaternion;
        }

        public static RotationMatrix3D RotateAroundVector(Vector3D vector, double angle)
        {
            return (RotationMatrix3D) Quaternion.FromAxisAngle(vector, angle);
        }

        private static RotationMatrix3D RotateX(double angle)
        {
            angle *= 0.017453292519943295;
            var matrixd = new RotationMatrix3D();
            matrixd[0, 0] = 1.0;
            matrixd[1, 1] = Math.Cos(angle);
            matrixd[1, 2] = -Math.Sin(angle);
            matrixd[2, 1] = Math.Sin(angle);
            matrixd[2, 2] = Math.Cos(angle);
            return matrixd;
        }

        private static RotationMatrix3D RotateY(double angle)
        {
            angle *= 0.017453292519943295;
            var matrixd = new RotationMatrix3D();
            matrixd[0, 0] = Math.Cos(angle);
            matrixd[0, 2] = Math.Sin(angle);
            matrixd[1, 1] = 1.0;
            matrixd[2, 0] = -Math.Sin(angle);
            matrixd[2, 2] = Math.Cos(angle);
            return matrixd;
        }

        private static RotationMatrix3D RotateZ(double angle)
        {
            angle *= 0.017453292519943295;
            var matrixd = new RotationMatrix3D();
            matrixd[0, 0] = Math.Cos(angle);
            matrixd[0, 1] = -Math.Sin(angle);
            matrixd[1, 0] = Math.Sin(angle);
            matrixd[1, 1] = Math.Cos(angle);
            matrixd[2, 2] = 1.0;
            return matrixd;
        }

        public double RotationAngle()
        {
            return ((Math.Acos((Trace() - 1.0) / 2.0) * 180.0) / Math.PI);
        }

        public Vector3D RotationAxis()
        {
            return ((Quaternion) this).Axis();
        }

        public Vector3D ABC
        {
            get
            {
                return new Vector3D(RPY[2], RPY[1], RPY[0]);
            }
        }

        public Vector3D ABG
        {
            get
            {
                double num4;
                double num5;
                double num6;
                double num7;
                var d = Math.Asin(base[0, 2]);
                var num2 = Math.Cos(d);
                var y = (d * 180.0) / Math.PI;
                if (Math.Abs(num2) > 0.005)
                {
                    num4 = base[2, 2] / num2;
                    num5 = -base[1, 2] / num2;
                    num6 = (Math.Atan2(num5, num4) * 180.0) / Math.PI;
                    num4 = base[0, 0] / num2;
                    num5 = -base[0, 1] / num2;
                    num7 = (Math.Atan2(num5, num4) * 180.0) / Math.PI;
                }
                else
                {
                    num6 = 0.0;
                    num4 = base[1, 1];
                    num5 = base[1, 0];
                    num7 = (Math.Atan2(num5, num4) * 180.0) / Math.PI;
                }
                return new Vector3D(num6, y, num7);
            }
        }

        public Vector3D EulerZYZ
        {
            get
            {
                double num2;
                double num3;
                var num = Math.Atan2(Math.Sqrt(Math.Pow(base[2, 0], 2.0) + Math.Pow(base[2, 1], 2.0)), base[2, 2]);
                if (Math.Abs(num) < 1E-06)
                {
                    num2 = 0.0;
                    num3 = Math.Atan2(-base[0, 1], base[0, 0]);
                }
                else if (Math.Abs(num - Math.PI) < 1E-06)
                {
                    num2 = 0.0;
                    num3 = Math.Atan2(base[0, 1], -base[0, 0]);
                }
                else
                {
                    num2 = Math.Atan2(base[1, 2], base[0, 2]);
                    num3 = Math.Atan2(base[2, 1], -base[2, 0]);
                }
                num2 *= 57.295779513082323;
                num *= 57.295779513082323;
                return new Vector3D(num2, num, num3 * 57.295779513082323);
            }
        }

        public Vector3D RPY
        {
            get
            {
                var z = Math.Atan2(base[1, 0], base[0, 0]);
                var y = Math.Atan2(-base[2, 0], Math.Sqrt((base[2, 1] * base[2, 1]) + (base[2, 2] * base[2, 2])));
                var num3 = Math.Atan2(base[2, 1], base[2, 2]);
                z *= 57.295779513082323;
                y *= 57.295779513082323;
                return new Vector3D(num3 * 57.295779513082323, y, z);
            }
        }
    }
}

