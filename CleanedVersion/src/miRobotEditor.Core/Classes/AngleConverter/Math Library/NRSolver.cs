using System;

namespace miRobotEditor.Core.Classes.AngleConverter
{
    public sealed class NRSolver
    {
        private const int MaxIterations = 20;
        private const double StopCondition = 1E-07;

        public NRSolver(int numEquations, int numVariables)
        {
            NumEquations = numEquations;
            NumVariables = numVariables;
        }

        private Matrix CalculateJacobian(ErrorFunction errorFunction, Vector guess)
        {
            var matrix = new Matrix(NumEquations, NumVariables);
            for (var i = 0; i < matrix.Columns; i++)
            {
                Vector vector2;
                int num4;
                Vector vector4;
                int num5;
                const double num2 = 1E-07;
                var num3 = (Math.Abs(guess[i]) >= 1.0) ? (Math.Abs(guess[i]) * num2) : num2;
                var vec = new Vector(guess);
                (vector2 = vec)[num4 = i] = vector2[num4] + num3;
                var vector3 = errorFunction(vec);
                (vector4 = vec)[num5 = i] = vector4[num5] - (2.0 * num3);
                var vector5 = errorFunction(vec);
                var vector6 = vector3 - vector5;
                matrix.SetColumn(i, vector6 / (2.0 * num3));
            }
            return matrix;
        }

        private static bool IsDone(Vector delta)
        {
            for (var i = 0; i < delta.Rows; i++)
            {
                if (Math.Abs(delta[i]) > StopCondition)
                {
                    return false;
                }
            }
            return true;
        }

        public Vector Solve(ErrorFunction errorFunction, Vector initialGuess)
        {
            if (initialGuess.Size != NumVariables)
            {
                throw new MatrixException("Size of the initial guess vector is not correct");
            }
            var guess = new Vector(initialGuess);
            NumStepsToConverge = 0;
            for (var i = 0; i < MaxIterations; i++)
            {
                var matrix = CalculateJacobian(errorFunction, guess);
                var vector3 = errorFunction(guess);
                var matrix2 = matrix.Transpose();
                var matrix3 = new SquareMatrix(matrix2 * matrix);
                var vector4 = matrix2 * vector3;
                var delta = matrix3.PseudoInverse() * vector4;
                guess -= delta;
                if (!IsDone(delta)) continue;
                NumStepsToConverge = i + 1;
                return guess;
            }
            return guess;
        }

        public int NumEquations { get; private set; }


        private int NumVariables { get; set; }

// ReSharper disable UnusedAutoPropertyAccessor.Local
        private int NumStepsToConverge { get; set; }
// ReSharper restore UnusedAutoPropertyAccessor.Local
    }
}

