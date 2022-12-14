using System;
using System.ComponentModel;

namespace miRobotEditor.Core.Classes.AngleConverter
{
    [Localizable(false)]
    public class Quaternion : IFormattable
    {
// ReSharper disable FunctionRecursiveOnAllPaths
        protected bool Equals(Quaternion other)
// ReSharper restore FunctionRecursiveOnAllPaths
        {
            return Equals(other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Quaternion) obj);
        }

        public override int GetHashCode()
        {
// ReSharper disable BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
// ReSharper restore BaseObjectGetHashCodeCallInGetHashCode
        }


        public Quaternion()
        {
            X = 0.0;
            Y = 0.0;
            Z = 0.0;
            W = 0.0;
        }


        public Quaternion(Quaternion q)
        {
            X = q.X;
            Y = q.Y;
            Z = q.Z;
            W = q.W;
        }

        public Quaternion(Vector3D vector, double scalar)
        {
            Vector = vector;
            Scalar = scalar;
        }

        public Quaternion(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public double Angle()
        {
            var num = Math.Acos(W) * 2.0;
            return ((num * 180.0) / Math.PI);
        }

        public Vector3D Axis()
        {
            new Quaternion(this).Normalise();
            var w = W;
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
            Math.Acos(w);
// ReSharper restore ReturnValueOfPureMethodIsNotUsed
            var num2 = Math.Sqrt(1.0 - (w * w));
            if (Math.Abs(num2) < 0.0005)
            {
                num2 = 1.0;
            }
            return new Vector3D(X / num2, Y / num2, Z / num2);
        }

        public Quaternion Conjugate()
        {
            return new Quaternion(-X, -Y, -Z, W);
        }

        public static Quaternion FromAxisAngle(Vector axis, double angle)
        {
            angle *= 0.017453292519943295;
            axis.Normalise();
            var num = Math.Sin(angle / 2.0);
            var w = Math.Cos(angle / 2.0);
            var quaternion = new Quaternion(axis[0] * num, axis[1] * num, axis[2] * num, w);
            quaternion.Normalise();
            return quaternion;
        }

        public Quaternion Inverse()
        {
            var quaternion = Conjugate();
            quaternion.Scalar = 1.0 / Scalar;
            return quaternion;
        }

        public double Magnitude()
        {
            return Math.Sqrt((((X * X) + (Y * Y)) + (Z * Z)) + (W * W));
        }

        public void Normalise()
        {
            var num = Magnitude();
            X /= num;
            Y /= num;
            Z /= num;
            W /= num;
        }

        public static Quaternion operator +(Quaternion q1, Quaternion q2)
        {
            return Add(q1,q2);
        }
        public static Quaternion Add(Quaternion q1, Quaternion q2)
        {
            return new Quaternion { Scalar = q1.Scalar + q2.Scalar, Vector = q1.Vector + q2.Vector };
        }
        public static explicit operator RotationMatrix3D(Quaternion q)
        {
            var matrixd = new RotationMatrix3D();
            var num = q.X * q.X;
            var num2 = q.X * q.Y;
            var num3 = q.X * q.Z;
            var num4 = q.X * q.W;
            var num5 = q.Y * q.Y;
            var num6 = q.Y * q.Z;
            var num7 = q.Y * q.W;
            var num8 = q.Z * q.Z;
            var num9 = q.Z * q.W;
            matrixd[0, 0] = 1.0 - (2.0 * (num5 + num8));
            matrixd[1, 0] = 2.0 * (num2 + num9);
            matrixd[2, 0] = 2.0 * (num3 - num7);
            matrixd[0, 1] = 2.0 * (num2 - num9);
            matrixd[1, 1] = 1.0 - (2.0 * (num + num8));
            matrixd[2, 1] = 2.0 * (num6 + num4);
            matrixd[0, 2] = 2.0 * (num3 + num7);
            matrixd[1, 2] = 2.0 * (num6 - num4);
            matrixd[2, 2] = 1.0 - (2.0 * (num + num5));
            return matrixd;
        }

        public static Quaternion operator *(Quaternion q1, Quaternion q2)
        {
            var quaternion = new Quaternion();
            var vectord = new Vector3D(q1.X, q1.Y, q1.Z);
            var vectord2 = new Vector3D(q2.X, q2.Y, q2.Z);
            var w = q1.W;
            var num2 = q2.W;
            var vectord3 = ((Vector3D) ((w * vectord2) + (num2 * vectord))) + Vector3D.Cross(vectord, vectord2);
            quaternion.X = vectord3[0];
            quaternion.Y = vectord3[1];
            quaternion.Z = vectord3[2];
            quaternion.W = (w * num2) - AngleConverter.Vector.Dot(vectord, vectord2);
            return quaternion;
        }
        public static Quaternion Multiply(Quaternion q1, Quaternion q2)
        {
            var quaternion = new Quaternion();
            var vectord = new Vector3D(q1.X, q1.Y, q1.Z);
            var vectord2 = new Vector3D(q2.X, q2.Y, q2.Z);
            var w = q1.W;
            var num2 = q2.W;
            var vectord3 = ((Vector3D)((w * vectord2) + (num2 * vectord))) + Vector3D.Cross(vectord, vectord2);
            quaternion.X = vectord3[0];
            quaternion.Y = vectord3[1];
            quaternion.Z = vectord3[2];
            quaternion.W = (w * num2) - AngleConverter.Vector.Dot(vectord, vectord2);
            return quaternion;
        }
        public static Quaternion operator -(Quaternion q1, Quaternion q2)
        {
            return Subtract(q1,q2);
        }
        public static Quaternion Subtract(Quaternion q1, Quaternion q2)
        {
            return new Quaternion { Scalar = q1.Scalar - q2.Scalar, Vector = q1.Vector - q2.Vector };
        }
        public static bool operator ==(Quaternion q1, Quaternion q2)
        {
            return q1==q2;
        }
        public static bool operator !=(Quaternion q1, Quaternion q2)
        {
            return !(q1 == q2);
        }
        public static bool Equals(Quaternion q1, Quaternion q2)
        {
            return q1 == q2;
        }
        public override string ToString()
        {
            return string.Format("{0:F2}, {1:F2}, {2:F2}, {3:F2}", new object[] { X, Y, Z, W });
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString();
        }

        public double Scalar
        {
            get
            {
                return W;
            }
            set
            {
                W = value;
            }
        }

        public Vector3D Vector
        {
            get
            {
                return new Vector3D(X, Y, Z);
            }
            set
            {
                X = value.X;
                Y = value.Y;
                Z = value.Z;
            }
        }

        public double X { get; set; }
        public double Y{ get; set; }
        public double Z{ get; set; }
        public double W{ get; set; }
      
    }
}

