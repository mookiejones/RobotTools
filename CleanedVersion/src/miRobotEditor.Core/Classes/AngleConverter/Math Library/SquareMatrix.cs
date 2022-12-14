using System;

namespace miRobotEditor.Core.Classes.AngleConverter
{
    [Serializable]
    public class SquareMatrix : Matrix
    {
        public SquareMatrix() : base(0, 0)
        {
        }

        public SquareMatrix(Matrix mat)
        {
            if (mat.Rows != mat.Columns)
            {
                throw new MatrixException("Matrix is not square. Cannot cast to a SquareMatrix");
            }
            Size = mat.Rows;
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    base[i, j] = mat[i, j];
                }
            }
        }

        public SquareMatrix(SquareMatrix mat) : base(mat)
        {
        }

        public SquareMatrix(int size) : base(size, size)
        {
        }

        public SquareMatrix(int size, params double[] elements) : base(size, size, elements)
        {
        }

        public double Determinant()
        {
            var matrix = new SquareMatrix(this);
            var num = matrix.MakeRowEchelon();
            for (var i = 0; i < Size; i++)
            {
                if (matrix.IsRowZero(i))
                {
                    return 0.0;
                }
            }
            return num;
        }

        public static SquareMatrix Identity(int size)
        {
            var matrix = new SquareMatrix(size);
            for (var i = 0; i < size; i++)
            {
                matrix[i, i] = 1.0;
            }
            return matrix;
        }

        public SquareMatrix Inverse()
        {
            var matrix = Augment(Identity(Size));
            matrix.MakeRowEchelon();
            var matrix2 = new SquareMatrix(Size);
            var matrix3 = new SquareMatrix(Size);
            for (var i = 0; i < Size; i++)
            {
                matrix2.SetColumn(i, matrix.GetColumn(i));
                matrix3.SetColumn(i, matrix.GetColumn(i + Size));
            }
            for (var j = 0; j < matrix2.Rows; j++)
            {
                if (matrix2.IsRowZero(j))
                {
                    throw new MatrixException("Matrix is singular");
                }
            }
            for (var k = Size - 1; k > 0; k--)
            {
                for (var m = k - 1; m >= 0; m--)
                {
                    var scalar = -matrix2[m, k];
                    matrix2.AddRowTimesScalar(m, k, scalar);
                    matrix3.AddRowTimesScalar(m, k, scalar);
                }
            }
            return matrix3;
        }

        public bool IsRotationMatrix()
        {
            if (!IsNaN())
            {
                if (Math.Abs(Determinant() - 1.0) > 0.001)
                {
                    return false;
                }
                Matrix matrix = Inverse();
                Matrix matrix2 = Transpose();
                for (var i = 0; i < Size; i++)
                {
                    for (var j = 0; j < Size; j++)
                    {
                        if (Math.Abs(matrix[i, j] - matrix2[i, j]) > 0.001)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private const double EPSILON = 0.0001;
// ReSharper disable once UnusedMember.Global
// ReSharper disable once InconsistentNaming
        public void LUDecomposition(out SquareMatrix l, out SquareMatrix u)
        {
            l = new SquareMatrix(Size);
            u = new SquareMatrix(Size);
            if (Math.Abs(base[0, 0] - 0.0) < EPSILON)
            {
                throw new MatrixException("Unable to decompose matrix");
            }
            l.SetColumn(0, GetColumn(0));
            u.SetRow(0, GetRow(0));
            u.MultiplyRow(0, 1.0 / base[0, 0]);
            for (var i = 1; i < Size; i++)
            {
                var vectorArray = new Vector[Size];
                var vectorArray2 = new Vector[Size];
                for (var j = 1; j < Size; j++)
                {
                    vectorArray[j] = new Vector(i);
                    vectorArray2[j] = new Vector(i);
                    var row = l.GetRow(j);
                    var column = u.GetColumn(j);
                    for (var m = 0; m < i; m++)
                    {
                        vectorArray[j][m] = row[m];
                        vectorArray2[j][m] = column[m];
                    }
                }
                for (var k = i; k < Size; k++)
                {
                    l[k, i] = base[k, i] - Vector.Dot(vectorArray[k], vectorArray2[i]);
                    if (k == i)
                    {
                        u[i, k] = 1.0;
                    }
                    else
                    {
                        if (Math.Abs(l[i, i] - 0.0) < EPSILON)
                        {
                            throw new MatrixException("Unable to decompose matrix");
                        }
                        u[i, k] = (base[i, k] - Vector.Dot(vectorArray[i], vectorArray2[k])) / l[i, i];
                    }
                }
            }
        }

        public SquareMatrix Minor(int i, int j)
        {
            var matrix = new SquareMatrix(Size - 1);
            var num = 0;
            for (var k = 0; k < Rows; k++)
            {
                if (k == i) continue;
                var num3 = 0;
                for (var m = 0; m < Columns; m++)
                {
                    if (m == j) continue;
                    matrix[num, num3] = base[k, m];
                    num3++;
                }
                num++;
            }
            return matrix;
        }

        public static SquareMatrix NaN(int size)
        {
            return new SquareMatrix( NaN(size, size));
        }

       

        public SquareMatrix Power(int power)
        {
            Matrix mat = new SquareMatrix(this);
            for (var i = 1; i < power; i++)
            {
                mat = this * mat;
            }
            return new SquareMatrix(mat);
        }

        public double Trace()
        {
            var num = 0.0;
            for (var i = 0; i < Size; i++)
            {
                num += base[i, i];
            }
            return num;
        }

        public new SquareMatrix Transpose()
        {
            return new SquareMatrix(base.Transpose());
        }

        public int Size
        {
            get
            {
                return Rows;
            }
            set
            {
                SetSize(value, value);
            }
        }
    }
}

