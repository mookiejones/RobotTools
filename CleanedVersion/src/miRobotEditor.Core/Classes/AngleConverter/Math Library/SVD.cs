using System;

namespace miRobotEditor.Core.Classes.AngleConverter
{
    public sealed class SVD
    {
        private readonly Matrix _u;
        private readonly SquareMatrix _v;
        private readonly Vector _w;
        private const double EPSILON = 0.0001;
        public SVD(Matrix mat)
        {
            double num9,num12,num13;
            Matrix matrix;
            int num17,num18,num3 = 0;

            if (mat.Rows < mat.Columns)
            {
                throw new MatrixException("Matrix must have rows >= columns");
            }
            var rows = mat.Rows;
            var columns = mat.Columns;

            var vector = new Vector(columns);
            var num4 = 0.0;
            var b = 0.0;
            var num6 = 0.0;
            var num7 = 0;
            _u = new Matrix(mat);
            _v = new SquareMatrix(columns);
            _w = new Vector(columns);
            for (var i = 0; i < columns; i++)
            {
                num3 = i + 1;
                vector[i] = num4 * b;
                b = num9 = num4 = 0.0;
                if (i < rows)
                {
                    for (var n = i; n < rows; n++)
                    {
                        num4 += Math.Abs(_u[n, i]);
                    }
                    if (Math.Abs(num4 - 0.0) > EPSILON)
                    {
                        for (var num11 = i; num11 < rows; num11++)
                        {
                            _u[num11, i] /= num4;
                            num9 += _u[num11, i] * _u[num11, i];
                        }
                        num12 = _u[i, i];
                        b = (num12 < 0.0) ? Math.Sqrt(num9) : -Math.Sqrt(num9);
                        num13 = (num12 * b) - num9;
                        _u[i, i] = num12 - b;
                        if (i != (columns - 1))
                        {
                            for (var num14 = num3; num14 < columns; num14++)
                            {
                                num9 = 0.0;
                                for (var num15 = i; num15 < rows; num15++)
                                {
                                    num9 += _u[num15, i] * _u[num15, num14];
                                }
                                num12 = num9 / num13;
                                for (var num16 = i; num16 < rows; num16++)
                                {
                                    (matrix = _u)[num17 = num16, num18 = num14] = matrix[num17, num18] + (num12 * _u[num16, i]);
                                }
                            }
                        }
                        for (var num19 = i; num19 < rows; num19++)
                        {
                            _u[num19, i] *= num4;
                        }
                    }
                }
                _w[i] = num4 * b;
                b = num9 = num4 = 0.0;
                if ((i < rows) && (i != (columns - 1)))
                {
                    for (var num20 = num3; num20 < columns; num20++)
                    {
                        num4 += Math.Abs(_u[i, num20]);
                    }
                    if (Math.Abs(num4 - 0.0) > EPSILON)
                    {
                        for (var num21 = num3; num21 < columns; num21++)
                        {
                            _u[i, num21] /= num4;
                            num9 += _u[i, num21] * _u[i, num21];
                        }
                        num12 = _u[i, num3];
                        b = (num12 < 0.0) ? Math.Sqrt(num9) : -Math.Sqrt(num9);
                        num13 = (num12 * b) - num9;
                        _u[i, num3] = num12 - b;
                        for (var num22 = num3; num22 < columns; num22++)
                        {
                            vector[num22] = _u[i, num22] / num13;
                        }
                        if (i != (rows - 1))
                        {
                            for (var num23 = num3; num23 < rows; num23++)
                            {
                                num9 = 0.0;
                                for (var num24 = num3; num24 < columns; num24++)
                                {
                                    num9 += _u[num23, num24] * _u[i, num24];
                                }
                                for (var num25 = num3; num25 < columns; num25++)
                                {
                                    Matrix matrix2;
                                    int num26;
                                    int num27;
                                    (matrix2 = _u)[num26 = num23, num27 = num25] = matrix2[num26, num27] + (num9 * vector[num25]);
                                }
                            }
                        }
                        for (var num28 = num3; num28 < columns; num28++)
                        {
                            _u[i, num28] *= num4;
                        }
                    }
                }
                num6 = Math.Max(num6, Math.Abs(_w[i]) + Math.Abs(vector[i]));
            }
            for (var j = columns - 1; j >= 0; j--)
            {
                if (j < (columns - 1))
                {
                    if (Math.Abs(b - 0.0) > EPSILON)
                    {
                        for (var num30 = num3; num30 < columns; num30++)
                        {
                            _v[num30, j] = (_u[j, num30] / _u[j, num3]) / b;
                        }
                        for (var num31 = num3; num31 < columns; num31++)
                        {
                            num9 = 0.0;
                            for (var num32 = num3; num32 < columns; num32++)
                            {
                                num9 += _u[j, num32] * _v[num32, num31];
                            }
                            for (var num33 = num3; num33 < columns; num33++)
                            {
                                Matrix matrix3;
                                int num34;
                                (matrix3 = _v)[num34 = num33, num17 = num31] = matrix3[num34, num17] + (num9 * _v[num33, j]);
                            }
                        }
                    }
                    for (var num35 = num3; num35 < columns; num35++)
                    {
                        _v[j, num35] = _v[num35, j] = 0.0;
                    }
                }
                _v[j, j] = 1.0;
                b = vector[j];
                num3 = j;
            }
            for (var k = columns - 1; k >= 0; k--)
            {
                num3 = k + 1;
                b = _w[k];
                if (k < (columns - 1))
                {
                    for (var num38 = num3; num38 < columns; num38++)
                    {
                        _u[k, num38] = 0.0;
                    }
                }
                if (Math.Abs(b - 0.0) > EPSILON)
                {
                    b = 1.0 / b;
                    if (k != (columns - 1))
                    {
                        for (var num39 = num3; num39 < columns; num39++)
                        {
                            num9 = 0.0;
                            for (var num40 = num3; num40 < rows; num40++)
                            {
                                num9 += _u[num40, k] * _u[num40, num39];
                            }
                            num12 = (num9 / _u[k, k]) * b;
                            for (var num41 = k; num41 < rows; num41++)
                            {
                                (matrix = _u)[num17 = num41, num18 = num39] = matrix[num17, num18] + (num12 * _u[num41, k]);
                            }
                        }
                    }
                    for (var num42 = k; num42 < rows; num42++)
                    {
                        _u[num42, k] *= b;
                    }
                }
                else
                {
                    for (var num43 = k; num43 < rows; num43++)
                    {
                        _u[num43, k] = 0.0;
                    }
                }
                (matrix = _u)[num17 = k, num18 = k] = matrix[num17, num18] + 1.0;
            }
            for (var m = columns - 1; m >= 0; m--)
            {
                for (var num45 = 0; num45 < 30; num45++)
                {
                    double num47;
                    double num50;
                    double num51;
                    var num46 = 1;
                    num3 = m;
                    while (num3 >= 0)
                    {
                        num7 = num3 - 1;
                        if (Math.Abs((Math.Abs(vector[num3]) + num6) - num6) < EPSILON)
                        {
                            num46 = 0;
                            break;
                        }
                        if (Math.Abs((Math.Abs(_w[num7]) + num6) - num6) < EPSILON)
                        {
                            break;
                        }
                        num3--;
                    }
                    if (num46 != 0)
                    {
                        num9 = 1.0;
                        for (var num48 = num3; num48 <= m; num48++)
                        {
                            num12 = num9 * vector[num48];
                            if (!(Math.Abs((Math.Abs(num12) + num6) - num6) > EPSILON)) continue;
                            b = _w[num48];
                            num13 = Pythag(num12, b);
                            _w[num48] = (float) num13;
                            num13 = 1.0 / num13;
                            num47 = b * num13;
                            num9 = -num12 * num13;
                            for (var num49 = 0; num49 < rows; num49++)
                            {
                                num50 = _u[num49, num7];
                                num51 = _u[num49, num48];
                                _u[num49, num7] = (num50 * num47) + (num51 * num9);
                                _u[num49, num48] = (num51 * num47) - (num50 * num9);
                            }
                        }
                    }
                    num51 = _w[m];
                    if (num3 == m)
                    {
                        if (num51 < 0.0)
                        {
                            _w[m] = -num51;
                            for (var num52 = 0; num52 < columns; num52++)
                            {
                                _v[num52, m] = -_v[num52, m];
                            }
                        }
                        break;
                    }
                    if (num45 >= 30)
                    {
                        throw new MatrixException("No convergence after 30 iterations");
                    }
                    var num53 = _w[num3];
                    num7 = m - 1;
                    num50 = _w[num7];
                    b = vector[num7];
                    num13 = vector[m];
                    num12 = (((num50 - num51) * (num50 + num51)) + ((b - num13) * (b + num13))) / ((2.0 * num13) * num50);
                    b = Pythag(num12, 1.0);
                    var num54 = (num12 >= 0.0) ? b : -b;
                    num12 = (((num53 - num51) * (num53 + num51)) + (num13 * ((num50 / (num12 + num54)) - num13))) / num53;
                    num47 = num9 = 1.0;
                    for (var num55 = num3; num55 <= num7; num55++)
                    {
                        var num56 = num55 + 1;
                        b = vector[num56];
                        num50 = _w[num56];
                        num13 = num9 * b;
                        b = num47 * b;
                        num51 = Pythag(num12, num13);
                        vector[num55] = num51;
                        num47 = num12 / num51;
                        num9 = num13 / num51;
                        num12 = (num53 * num47) + (b * num9);
                        b = (b * num47) - (num53 * num9);
                        num13 = num50 * num9;
                        num50 *= num47;
                        for (var num57 = 0; num57 < columns; num57++)
                        {
                            num53 = _v[num57, num55];
                            num51 = _v[num57, num56];
                            _v[num57, num55] = (num53 * num47) + (num51 * num9);
                            _v[num57, num56] = (num51 * num47) - (num53 * num9);
                        }
                        num51 = Pythag(num12, num13);
                        _w[num55] = num51;
                        if (Math.Abs(num51 - 0.0) > EPSILON)
                        {
                            num51 = 1.0 / num51;
                            num47 = num12 * num51;
                            num9 = num13 * num51;
                        }
                        num12 = (num47 * b) + (num9 * num50);
                        num53 = (num47 * num50) - (num9 * b);
                        for (var num58 = 0; num58 < rows; num58++)
                        {
                            num50 = _u[num58, num55];
                            num51 = _u[num58, num56];
                            _u[num58, num55] = (num50 * num47) + (num51 * num9);
                            _u[num58, num56] = (num51 * num47) - (num50 * num9);
                        }
                    }
                    vector[num3] = 0.0;
                    vector[m] = num12;
                    _w[m] = num53;
                }
            }
        }

        private static double Pythag(double a, double b)
        {
            double num3;
            var num = Math.Abs(a);
            var num2 = Math.Abs(b);
            if (num > num2)
            {
                num3 = num2 / num;
                return (num * Math.Sqrt(1.0 + (num3 * num3)));
            }
            if (num2 > 0.0)
            {
                num3 = num / num2;
                return (num2 * Math.Sqrt(1.0 + (num3 * num3)));
            }
            return 0.0;
        }

        public double ConditionNumber
        {
            get
            {
                var positiveInfinity = double.PositiveInfinity;
                var num2 = 0.0;
                for (var i = 0; i < W.Rows; i++)
                {
                    positiveInfinity = Math.Min(positiveInfinity, _w[i]);
                    num2 = Math.Max(num2, _w[i]);
                }
                return (num2 / positiveInfinity);
            }
        }

        public int SmallestSingularIndex
        {
            get
            {
                var num = W[0];
                var num2 = 0;
                for (var i = 1; i < W.Size; i++)
                {
                    if (!(W[i] < num)) continue;
                    num = W[i];
                    num2 = i;
                }
                return num2;
            }
        }

        public Matrix U
        {
            get
            {
                return _u;
            }
        }

        public SquareMatrix V
        {
            get
            {
                return _v;
            }
        }

        public Vector W
        {
            get
            {
                return _w;
            }
        }
    }
}

