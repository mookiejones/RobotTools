using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace miRobotEditor.Core.Classes.AngleConverter.Robot
{
    [Localizable(false)]
    public class RobotBaseAndToolCalibration
    {
        public void CalibrateRobotBaseAndTool(Collection<TransformationMatrix3D> robotPoses, Collection<TransformationMatrix3D> measuredPoses)
        {
            if (robotPoses.Count != measuredPoses.Count)
            {
                throw new ArgumentException("Number of measured poses does not equal the number of robot poses");
            }
            if (robotPoses.Count < 3)
            {
                throw new MatrixException("Number of robot poses must be 3 or more");
            }
            var list = new Collection<Matrix>();
            var list2 = new Collection<Matrix>();
            for (var i = 0; i < measuredPoses.Count; i++)
            {
                var matrixd = measuredPoses[i];
                var matrixd2 = robotPoses[i];
                var rotation = (Quaternion) matrixd.Rotation;
                var quaternion2 = (Quaternion) matrixd2.Rotation;
                var vectord = rotation.Vector;
                var elements = new double[9];
                elements[1] = -vectord.Z;
                elements[2] = vectord.Y;
                elements[3] = vectord.Z;
                elements[5] = -vectord.X;
                elements[6] = -vectord.Y;
                elements[7] = vectord.X;
                var matrix = new SquareMatrix(3, elements);
                var vectord2 = quaternion2.Vector;
                var numArray2 = new double[9];
                numArray2[1] = -vectord2.Z;
                numArray2[2] = vectord2.Y;
                numArray2[3] = vectord2.Z;
                numArray2[5] = -vectord2.X;
                numArray2[6] = -vectord2.Y;
                numArray2[7] = vectord2.X;
                var matrix2 = new SquareMatrix(3, numArray2);
                var matrix3 = (vectord * vectord.Transpose()) / rotation.Scalar;
                var matrix4 = (vectord * vectord2.Transpose()) / rotation.Scalar;
                var matrix5 = new SquareMatrix((rotation.Scalar * SquareMatrix.Identity(3)) + matrix + matrix3);
                var mat = new SquareMatrix((-quaternion2.Scalar * SquareMatrix.Identity(3)) + matrix2 - matrix4);
                list.Add(matrix5.Augment(mat));
                list2.Add(vectord2 - ((quaternion2.Scalar / rotation.Scalar) * vectord));
            }
            var matrix7 = new Matrix(list[0].Transpose());
            var matrix8 = new Matrix(list2[0].Transpose());
            for (var j = 1; j < list.Count; j++)
            {
                matrix7 = matrix7.Augment(list[j].Transpose());
                matrix8 = matrix8.Augment(list2[j].Transpose());
            }
            matrix7 = matrix7.Transpose();
            matrix8 = matrix8.Transpose();
            ConditionNumber = matrix7.ConditionNumber();
            var vector = new Vector(matrix7.PseudoInverse() * matrix8);
            var scalar = Math.Pow(((1.0 + (vector[3] * vector[3])) + (vector[4] * vector[4])) + (vector[5] * vector[5]), -0.5);
            var vectord3 = new Vector3D(vector[3], vector[4], vector[5]) * scalar;
            var vectord4 = new Vector3D(vector[0], vector[1], vector[2]) * scalar;
            var num4 = Math.Pow(((1.0 - (vectord4[0] * vectord4[0])) - (vectord4[1] * vectord4[1])) - (vectord4[2] * vectord4[2]), 0.5);
            var quaternion3 = new Quaternion(vectord3, scalar);
            var rot = ((RotationMatrix3D) quaternion3).Inverse();
            var quaternion4 = new Quaternion(vectord4, num4);
            var matrixd4 = (RotationMatrix3D) quaternion4;
            var num5 = (robotPoses.Select(t => (Quaternion) measuredPoses[0].Rotation).Select(
                quaternion5 => new {quaternion5, quaternion6 = (Quaternion) robotPoses[0].Rotation}).Select(
                    @t1 =>
                    (Vector.Dot(@t1.quaternion5.Vector/@t1.quaternion5.Scalar, quaternion4.Vector) +
                     ((@t1.quaternion6.Scalar/@t1.quaternion5.Scalar)*quaternion3.Scalar)) -
                    Vector.Dot(@t1.quaternion6.Vector/@t1.quaternion5.Scalar, quaternion3.Vector))).Sum();
            num5 /= robotPoses.Count;
            if (Math.Sign(num5) != Math.Sign(num4))
            {
            }
            var list3 = new Collection<Matrix>();
            var list4 = new Collection<Vector>();
            for (var m = 0; m < robotPoses.Count; m++)
            {
                Matrix matrix9 = new RotationMatrix3D(measuredPoses[m].Rotation);
                list3.Add(matrix9.Augment(-SquareMatrix.Identity(3)));
                list4.Add(new Vector3D((rot * robotPoses[m].Translation) - measuredPoses[m].Translation));
            }
            var matrix10 = new Matrix(list3[0].Transpose());
            var matrix11 = new Matrix(list4[0].Transpose());
            for (var n = 1; n < list3.Count; n++)
            {
                matrix10 = matrix10.Augment(list3[n].Transpose());
                matrix11 = matrix11.Augment(list4[n].Transpose());
            }
            matrix10 = matrix10.Transpose();
            matrix11 = matrix11.Transpose();
            ConditionNumber = Math.Max(ConditionNumber, matrix10.ConditionNumber());
            var vector2 = new Vector(matrix10.PseudoInverse() * matrix11);
            CalculatedRobotBase = new TransformationMatrix3D(new Vector3D(vector2[3], vector2[4], vector2[5]), rot);
            CalculatedRobotTool = new TransformationMatrix3D(new Vector3D(vector2[0], vector2[1], vector2[2]), matrixd4.Inverse());
        }

// ReSharper disable once MemberCanBePrivate.Global
        public TransformationMatrix3D CalculatedRobotBase { get; private set; }

// ReSharper disable once MemberCanBePrivate.Global
        public TransformationMatrix3D CalculatedRobotTool { get; private set; }

// ReSharper disable once MemberCanBePrivate.Global
        public double ConditionNumber { get; private set; }
    }
}

