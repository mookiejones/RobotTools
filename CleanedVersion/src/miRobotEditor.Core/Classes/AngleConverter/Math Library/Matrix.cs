using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace miRobotEditor.Core.Classes.AngleConverter
{
    [Localizable(false),Serializable]
    public class Matrix : IFormattable
    {
        private bool Equals(Matrix other)
        {
            return _columns == other._columns && Equals(_elements, other._elements) && _rows == other._rows;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Matrix) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
// ReSharper disable NonReadonlyFieldInGetHashCode
                var hashCode = _columns;
                hashCode = (hashCode*397) ^ (_elements != null ? _elements.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ _rows;
                // ReSharper restore NonReadonlyFieldInGetHashCode

                return hashCode;
            }
        }

        private int _columns;
        private double[,] _elements;
        private int _rows;

        protected Matrix()
        {
            SetSize(0, 0);
        }

        public Matrix(Matrix mat)
        {
            SetSize(mat.Rows, mat.Columns);
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    _elements[i, j] = mat[i,j];
                }
            }
        }

        public Matrix(int rows, int columns)
        {
            SetSize(rows, columns);
        }

        protected Matrix(int rows, int columns, params double[] elements)
        {
            if (elements.Length != (rows * columns))
            {
                throw new MatrixException("Number of elements does not match matrix dimension");
            }
            SetSize(rows, columns);
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    _elements[i,j] = elements[j + (i * columns)];
                }
            }
        }

        protected void AddRowTimesScalar(int row1, int row2, double scalar)
        {
            var row = GetRow(row2);
            for (var i = 0; i < Columns; i++)
            {
                Matrix matrix;
                int num2;
                int num3;
                (matrix = this)[num2 = row1, num3 = i] = matrix[num2, num3] + (scalar * row[i, 0]);
            }
        }

        public Matrix Augment(Matrix mat)
        {
            if (Rows != mat.Rows)
            {
                throw new MatrixException("Cannot augment matrices with different number of rows");
            }
            var matrix = new Matrix(Rows, Columns + mat.Columns);
            for (var i = 0; i < Columns; i++)
            {
                matrix.SetColumn(i, GetColumn(i));
            }
            for (var j = 0; j < mat.Columns; j++)
            {
                matrix.SetColumn(j + Columns, mat.GetColumn(j));
            }
            return matrix;
        }

        public double ConditionNumber()
        {
            var svd = new SVD(this);
            return svd.ConditionNumber;
        }

      

        public Vector GetColumn(int column)
        {
            var vector = new Vector(Rows);
            for (var i = 0; i < Rows; i++)
            {
                vector[i] = this[i, column];
            }
            return vector;
        }

       

        public Vector GetRow(int row)
        {
            var vector = new Vector(Columns);
            for (var i = 0; i < Columns; i++)
            {
                vector[i] = this[row, i];
            }
            return vector;
        }

        private bool IsColumnZeroBelowRow(int column, int row)
        {
            for (var i = row; i < Rows; i++)
            {
                if (Math.Abs(this[i, column]) > 1E-08)
                {
                    return false;
                }
            }
            return true;
        }

        protected bool IsNaN()
        {
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    if (!double.IsNaN(this[i, j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private const double EPSILON = 0.0001;

        protected bool IsRowZero(int row)
        {
            for (var i = 0; i < Columns; i++)
            {
                if (Math.Abs(this[row, i]) > 1E-08)
                {
                    return false;
                }
            }
            return true;
        }

        public double MakeRowEchelon()
        {
            var num = 1.0;
            for (var i = 0; i < Rows; i++)
            {
                var num3 = -1;
                for (var j = 0; j < Columns; j++)
                {
                    if (IsColumnZeroBelowRow(j, i)) continue;
                    num3 = j;
                    break;
                }
                if (num3 == -1)
                {
                    return num;
                }
                var naN = double.NaN;
                for (var k = i; k < Rows; k++)
                {
                    if (!(Math.Abs(this[k, num3]) > 1E-05)) continue;
                    if (i != k)
                    {
                        SwapRows(i, k);
                        num = -num;
                    }
                    naN = this[i, num3];
                    break;
                }
                if (Math.Abs(naN - 1.0) > EPSILON)
                {
                    MultiplyRow(i, 1.0 / naN);
                    num *= naN;
                }
            Label_00CE:
                var num7 = -1;
                var num8 = double.NaN;
                for (var m = i + 1; m < Rows; m++)
                {
                    if (!(Math.Abs(this[m, num3] - 0.0) > EPSILON)) continue;
                    num7 = m;
                    num8 = this[m, num3];
                    break;
                }
                switch (num7)
                {
                    case -1:
                        break;
                    default:
                        AddRowTimesScalar(num7, i, -num8);
                        goto Label_00CE;
                }
            }
            return num;
        }

        protected void MultiplyRow(int row, double scalar)
        {
            for (var i = 0; i < Columns; i++)
            {
                Matrix matrix;
                int num2;
                int num3;
                (matrix = this)[num2 = row, num3 = i] = matrix[num2, num3] * scalar;
            }
        }

        protected static Matrix NaN(int rows, int columns)
        {
            var matrix = new Matrix(rows, columns);
            matrix.SetSize(rows, columns);
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    matrix[i, j] = double.NaN;
                }
            }
            return matrix;
        }

        public static Matrix operator +(Matrix lhs, Matrix rhs)
        {
            if ((lhs.Rows != rhs.Rows) || (lhs.Columns != rhs.Columns))
            {
                throw new MatrixException("Matrices are not the same size");
            }
            var matrix = new Matrix(lhs.Rows, lhs.Columns);
            for (var i = 0; i < lhs.Rows; i++)
            {
                for (var j = 0; j < lhs.Columns; j++)
                {
                    matrix[i, j] = lhs[i, j] + rhs[i, j];
                }
            }
            return matrix;
        }

        public static Matrix operator +(Matrix mat, double scalar)
        {
            var matrix = new Matrix(mat);
            for (var i = 0; i < matrix.Rows; i++)
            {
                for (var j = 0; j < matrix.Columns; j++)
                {
                    Matrix matrix2;
                    int num3;
                    int num4;
                    (matrix2 = matrix)[num3 = i, num4 = j] = matrix2[num3, num4] + scalar;
                }
            }
            return matrix;
        }

// ReSharper disable once UnusedParameter.Global
// ReSharper disable UnusedParameter.Global
        public static Matrix operator +(double scalar, Matrix mat)
// ReSharper restore UnusedParameter.Global
        {
            return new Matrix();
            //return (mat + ((Matrix) scalar));
        }

        public static Matrix operator /(Matrix mat, double scalar)
        {
            if (Math.Abs(scalar - 0.0) < EPSILON)
            {
                throw new DivideByZeroException();
            }
            return mat * (1.0 / scalar);
        }

        public static bool operator ==(Matrix m1, Matrix m2)
        {
            try
            {
                if (m2 != null && (m1 != null && (m1.Rows != m2.Rows || (m1.Columns != m2.Columns))))
                {
                    return false;
                }
                Debug.Assert(m1 != null, "m1 != null");
                for (var i = 0; i < m1.Rows; i++)
                {
                    for (var j = 0; j < m1.Columns; j++)
                    {
                        if (m2 != null && Math.Abs(m1[i, j] - m2[i, j]) > EPSILON)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        public static bool operator !=(Matrix m1, Matrix m2)
        {
            return !(m1 == m2);
        }

        public static Matrix operator *(Matrix lhs, Matrix rhs)
        {
            if (lhs.Columns != rhs.Rows)
            {
                throw new MatrixException("Matrices are not compatible for multiplication");
            }
            var matrix = new Matrix(lhs.Rows, rhs.Columns);
            for (var i = 0; i < matrix.Rows; i++)
            {
                for (var j = 0; j < matrix.Columns; j++)
                {
                    var num3 = 0.0;
                    for (var k = 0; k < lhs.Columns; k++)
                    {
                        num3 += lhs[i, k] * rhs[k, j];
                    }
                    matrix[i, j] = num3;
                }
            }
            return matrix;
        }

        public static Matrix operator *(Matrix mat, double scalar)
        {
            var matrix = new Matrix(mat.Rows, mat.Columns);
            for (var i = 0; i < mat.Rows; i++)
            {
                for (var j = 0; j < mat.Columns; j++)
                {
                    matrix[i, j] = mat[i, j] * scalar;
                }
            }
            return matrix;
        }

        public static Matrix operator *(double scalar, Matrix mat)
        {
            return mat * scalar;
        }

        public static Matrix operator -(Matrix lhs, Matrix rhs)
        {
            if ((lhs.Rows != rhs.Rows) || (lhs.Columns != rhs.Columns))
            {
                throw new MatrixException("Matrices are not the same size");
            }
            var matrix = new Matrix(lhs.Rows, lhs.Columns);
            for (var i = 0; i < lhs.Rows; i++)
            {
                for (var j = 0; j < lhs.Columns; j++)
                {
                    matrix[i, j] = lhs[i, j] - rhs[i, j];
                }
            }
            return matrix;
        }

        public static Matrix operator -(Matrix mat, double scalar)
        {
            var matrix = new Matrix(mat);
            for (var i = 0; i < matrix.Rows; i++)
            {
                for (var j = 0; j < matrix.Columns; j++)
                {
                    Matrix matrix2;
                    int num3;
                    int num4;
                    (matrix2 = matrix)[num3 = i, num4 = j] = matrix2[num3, num4] - scalar;
                }
            }
            return matrix;
        }

        public static Matrix operator -(Matrix mat)
        {
            var matrix = new Matrix(mat);
            for (var i = 0; i < mat.Rows; i++)
            {
                for (var j = 0; j < mat.Columns; j++)
                {
                    matrix[i, j] = -mat[i, j];
                }
            }
            return matrix;
        }

        public Matrix PseudoInverse()
        {
            var svd = new SVD(this);
            Matrix matrix = new SquareMatrix(Columns);
            for (var i = 0; i < svd.W.Rows; i++)
            {
                if (svd.W[i] > 1E-05)
                {
                    matrix[i, i] = 1.0 / svd.W[i];
                }
            }
            return ((svd.V * matrix) * svd.U.Transpose());
        }

        public void SetColumn(int column, Vector vec)
        {
            for (var i = 0; i < Rows; i++)
            {
                this[i, column] = vec[i];
            }
        }

        public void SetRow(int row, Vector vec)
        {
            for (var i = 0; i < Columns; i++)
            {
                this[row, i] = vec[i];
            }
        }

        protected void SetSize(int rows, int columns)
        {            
            _rows = rows;
            _columns = columns;
//            throw new NotImplementedException();
           _elements = new double[rows,columns];
        }

        private void SwapRows(int row1, int row2)
        {
            var row = GetRow(row1);
            var vec = GetRow(row2);
            SetRow(row1, vec);
            SetRow(row2, row);
        }

        public override string ToString()
        {
          //  return ToString();
            return ToString(null, null);
        }

        public virtual string ToString(string format, IFormatProvider formatProvider)
    {
        var sb = new StringBuilder();
        if (format == null)
            format = "F2";
 
        if (format.ToUpper().StartsWith("F"))
        {
            //string[] t= System.Text.RegularExpressions.Regex.Split(ToString(),@"]");
            for (var r = 0; r < Rows; r++)
            {
                sb.Append("[");
                for (var c = 0; c < Columns; c++)
                {
                    sb.Append(this[r, c].ToString(format));
                    if ((c + 1) < Columns)
                        sb.Append(", ");
                }
                sb.Append("]");
                if ((r + 1) < Rows)
                    sb.Append(Environment.NewLine);
 
            }
            return sb.ToString();
        }
 
         if (!format.ToUpper(CultureInfo.InvariantCulture).StartsWith("MATLAB"))
             throw new FormatException("Invalid Format Specifier");
 
         sb = new StringBuilder();
         sb.Append("[");
         for (var i = 0; i < Rows; i++)
         {
             for (var m = 0; m < Columns; m++)
             {
                 sb.Append(this[i, m].ToString("F5"));
                 if ((m + 1) < Columns)
                     sb.Append(", ");
             }
 
             if ((i + 1) < Rows)
                 sb.Append(";" + Environment.NewLine);
         }
         
         return (sb + "]");
     }

    public Matrix Transpose()
    {
        var matrix = new Matrix(Columns, Rows);
        for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    matrix[j, i] = this[i, j];
                }
            }
            return matrix;
        }

        public int Columns
        {
            get
            {
                return _columns;
            }
        }

        public double this[int row, int column] 
            {

        get
            {
                return _elements[row,column];
            }
            set
            {
                _elements[row,column] = value;
            }
        }

        public int Rows
        {
            get
            {
                return _rows;
            }
        }

       
    }
}

