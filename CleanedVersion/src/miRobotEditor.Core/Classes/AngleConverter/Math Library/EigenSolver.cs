using System;

namespace miRobotEditor.Core.Classes.AngleConverter
{
// ReSharper disable once UnusedMember.Global
    public class EigenSolver
    {
        private readonly Vector _diag;
        private bool _isRotation;
        private SquareMatrix _mat;
        private readonly Vector _subd;
        private const double EPSILON = 0.0001;

        public EigenSolver(SquareMatrix m)
        {
            if (m.Size != 3)
            {
                throw new MatrixException("Unable to solve a matrix of size != 3");
            }
            _mat = new SquareMatrix(m);
            _diag = new Vector(3);
            _subd = new Vector(3);
        }

        public static double[] ComputeRoots(Matrix m)
        {
            var num = m[0, 0];
            var num2 = m[0, 1];
            var num3 = m[0, 2];
            var num4 = m[1, 1];
            var num5 = m[1, 2];
            var num6 = m[2, 2];
            var num7 = (((((num * num4) * num6) + (((2.0 * num2) * num3) * num5)) - ((num * num5) * num5)) - ((num4 * num3) * num3)) - ((num6 * num2) * num2);
            var num8 = (((((num * num4) - (num2 * num2)) + (num * num6)) - (num3 * num3)) + (num4 * num6)) - (num5 * num5);
            var num9 = (num + num4) + num6;
            var num10 = num9 / 3.0;
            var num11 = (num8 - (num9 / num10)) / 3.0;
            if (num11 > 0.0)
            {
                num11 = 0.0;
            }
            var x = 0.5 * (num7 + (num10 * (((2.0 * num10) * num10) - num8)));
            var num13 = (x * x) + ((num11 * num11) * num11);
            if (num13 > 0.0)
            {
                num13 = 0.0;
            }
            var num14 = Math.Sqrt(-num11);
            var d = Math.Atan2(Math.Sqrt(-num13), x) / 3.0;
            var num16 = Math.Cos(d);
            var num17 = Math.Sin(d);
            return new[] { (num10 + ((2.0 * num14) * num16)), (num10 - (num14 * (num16 + (Math.Sqrt(3.0) * num17)))), (num10 - (num14 * (num16 - (Math.Sqrt(3.0) * num17)))) };
        }

        private void DecreasingSort()
        {
            for (var i = 0; i <= 1; i++)
            {
                var num2 = i;
                var num3 = _diag[num2];
                var num4 = i + 1;
                while (num4 < 3)
                {
                    if (_diag[num4] > num3)
                    {
                        num2 = num4;
                        num3 = _diag[num2];
                    }
                    num4++;
                }
                if (num2 == i) continue;
                _diag[num2] = _diag[i];
                _diag[i] = num3;
                for (num4 = 0; num4 < 3; num4++)
                {
                    var num5 = _mat[num4, i];
                    _mat[num4, i] = _mat[num4, num2];
                    _mat[num4, num2] = num5;
                    _isRotation = !_isRotation;
                }
            }
        }

        public void DecrSortEigenStuff()
        {
            Tridiagonal();
            QLAlgorithm();
            DecreasingSort();
            GuaranteeRotation();
        }

        private void GuaranteeRotation()
        {
            if (_isRotation) return;
            for (var i = 0; i < 3; i++)
            {
                _mat[i, 0] = -_mat[i, 0];
            }
        }

// ReSharper disable once InconsistentNaming
        private void QLAlgorithm()
        {
            for (var i = 0; i < 3; i++)
            {
                var num2 = 0;
                while (num2 < 0x20)
                {
                    Vector vector;
                    int num14;
                    var num3 = i;
                    while (num3 <= 1)
                    {
                        var num4 = Math.Abs(_diag[num3]) + Math.Abs(_diag[num3 + 1]);
                        if (Math.Abs(Math.Abs(_subd[num3]) + num4 - num4) < EPSILON)
                        {
                            break;
                        }
                        num3++;
                    }
                    if (num3 == i)
                    {
                        break;
                    }
                    var num5 = (_diag[i + 1] - _diag[i]) / (2.0 * _subd[i]);
                    var num6 = Math.Sqrt((num5 * num5) + 1.0);
                    if (num5 < 0.0)
                    {
                        num5 = (_diag[num3] - _diag[i]) + (_subd[i] / (num5 - num6));
                    }
                    else
                    {
                        num5 = (_diag[num3] - _diag[i]) + (_subd[i] / (num5 + num6));
                    }
                    var num7 = 1.0;
                    var num8 = 1.0;
                    var num9 = 0.0;
                    for (var j = num3 - 1; j >= i; j--)
                    {
                        var num11 = num7 * _subd[j];
                        var num12 = num8 * _subd[j];
                        if (Math.Abs(num11) >= Math.Abs(num5))
                        {
                            num8 = num5 / num11;
                            num6 = Math.Sqrt((num8 * num8) + 1.0);
                            _subd[j + 1] = num11 * num6;
                            num7 = 1.0 / num6;
                            num8 *= num7;
                        }
                        else
                        {
                            num7 = num11 / num5;
                            num6 = Math.Sqrt((num7 * num7) + 1.0);
                            _subd[j + 1] = num5 * num6;
                            num8 = 1.0 / num6;
                            num7 *= num8;
                        }
                        num5 = _diag[j + 1] - num9;
                        num6 = ((_diag[j] - num5) * num7) + ((2.0 * num12) * num8);
                        num9 = num7 * num6;
                        _diag[j + 1] = num5 + num9;
                        num5 = (num8 * num6) - num12;
                        for (var k = 0; k < 3; k++)
                        {
                            num11 = _mat[k, j + 1];
                            _mat[k, j + 1] = (num7 * _mat[k, j]) + (num8 * num11);
                            _mat[k, j] = (num8 * _mat[k, j]) - (num7 * num11);
                        }
                    }
                    (vector = _diag)[num14 = i] = vector[num14] - num9;
                    _subd[i] = num5;
                    _subd[num3] = 0.0;
                    num2++;
                }
                if (num2 == 0x20)
                {
                    throw new MatrixException("No Convergence after 10 iterations");
                }
            }
        }



        public static bool DoubleEquality(double a, double b)
        {
            if (double.IsNaN(a)) return double.IsNaN(b);
            if (double.IsInfinity(a)) return double.IsInfinity(b);
            if (Math.Abs(a - 0) < EPSILON) return Math.Abs(b - 0) < EPSILON;
            return Math.Abs(a - b) <= Math.Abs(a*1e-15);
        }

        private void Tridiagonal()
        {
            Matrix matrix = new SquareMatrix(3);
            matrix.SetRow(0, _mat.GetRow(0));
            matrix.SetRow(1, _mat.GetRow(1));
            _diag[0] = matrix[0, 0];
            _subd[2] = 0.0;
            if (!DoubleEquality(matrix[0,2],0.0))
            {
                Matrix matrix2;
                Matrix matrix3;
                var num = Math.Sqrt((matrix[0, 1] * matrix[0, 1]) + (matrix[0, 2] * matrix[0, 2]));
                var num2 = 1.0 / num;
                (matrix2 = matrix)[0, 1] = matrix2[0, 1] * num2;
                (matrix3 = matrix)[0, 2] = matrix3[0, 2] * num2;
                var num3 = ((2.0 * matrix[0, 1]) * matrix[1, 2]) + (matrix[0, 2] * (matrix[2, 2] - matrix[1, 1]));
                _diag[1] = matrix[1, 1] + (matrix[0, 2] * num3);
                _diag[2] = matrix[2, 2] - (matrix[0, 2] * num3);
                _subd[0] = num;
                _subd[1] = matrix[1, 2] - (matrix[0, 1] * num3);
                _mat = SquareMatrix.Identity(3);
                _mat[1, 1] = matrix[0, 1];
                _mat[1, 2] = matrix[0, 2];
                _mat[2, 1] = matrix[0, 2];
                _mat[2, 2] = -matrix[0, 1];
                _isRotation = false;
            }
            else
            {
                _diag[1] = matrix[1, 1];
                _diag[2] = matrix[2, 2];
                _subd[0] = matrix[0, 1];
                _subd[1] = matrix[1, 2];
                _mat = SquareMatrix.Identity(3);
                _isRotation = true;
            }
        }
    }
}

