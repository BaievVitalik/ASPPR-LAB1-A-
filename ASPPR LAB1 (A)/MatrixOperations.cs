using System;

namespace MatrixPracticalWork
{
    static class MatrixOperations
    {
        private static double[,] JordanGaussStep(double[,] matrix, int pivotRow, int pivotCol)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[,] newMatrix = new double[rows, cols];
            double pivot = matrix[pivotRow, pivotCol];

            if (Math.Abs(pivot) < 1e-10) return newMatrix;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double val;
                    if (i == pivotRow && j == pivotCol) val = 1.0;
                    else if (i == pivotRow) val = -matrix[i, j];
                    else if (j == pivotCol) val = matrix[i, j];
                    else val = matrix[i, j] * pivot - matrix[i, pivotCol] * matrix[pivotRow, j];

                    newMatrix[i, j] = val / pivot;
                }
            }
            return newMatrix;
        }

        public static double[,] CalculateInverseMatrix(double[,] inputMatrix, bool showSteps = true)
        {
            int n = inputMatrix.GetLength(0);
            double[,] currentMatrix = (double[,])inputMatrix.Clone();

            if (showSteps) Console.WriteLine("Протокол перетворення (ЗЖВ):");

            for (int k = 0; k < n; k++)
            {
                if (showSteps)
                {
                    Console.WriteLine($"---> Крок #{k + 1}");
                    Console.WriteLine($"Розв’язувальний елемент: A[{k + 1}, {k + 1}] = {currentMatrix[k, k]:F2}");
                }

                currentMatrix = JordanGaussStep(currentMatrix, k, k);

                if (showSteps)
                {
                    PrintSimpleMatrix(currentMatrix);
                    Console.WriteLine();
                }
            }
            return currentMatrix;
        }

        public static int CalculateRank(double[,] inputMatrix)
        {
            int rows = inputMatrix.GetLength(0);
            int cols = inputMatrix.GetLength(1);
            double[,] mat = (double[,])inputMatrix.Clone();

            int rank = 0;
            const double EPSILON = 1e-10;
            bool[] rowSelected = new bool[rows];

            for (int j = 0; j < cols && rank < rows; j++)
            {
                int k = -1;
                for (int i = 0; i < rows; i++)
                {
                    if (!rowSelected[i] && Math.Abs(mat[i, j]) > EPSILON)
                    {
                        k = i;
                        break;
                    }
                }

                if (k != -1)
                {
                    rank++;
                    rowSelected[k] = true;
                    double pivot = mat[k, j];
                    for (int l = j; l < cols; l++) mat[k, l] /= pivot;

                    for (int i = 0; i < rows; i++)
                    {
                        if (i != k && Math.Abs(mat[i, j]) > EPSILON)
                        {
                            double factor = mat[i, j];
                            for (int l = j; l < cols; l++)
                                mat[i, l] -= factor * mat[k, l];
                        }
                    }
                }
            }
            return rank;
        }

        public static void SolveMethod1(double[,] originalA, double[,] inverseC, double[] vectorB)
        {
            int n = originalA.GetLength(0);
            Console.WriteLine("Обчислення розв’язків (X = C * B):");

            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                Console.WriteLine($"X[{i + 1}]:");

                for (int j = 0; j < n; j++)
                {
                    double val = inverseC[i, j];
                    double bVal = vectorB[j];
                    sum += val * bVal;

                    Console.Write($"{bVal:F2}*({val:F2})");
                    if (j < n - 1) Console.Write(" + ");
                }

                Console.WriteLine($" = {sum:F2}");
            }
        }

        public static void PrintMatrix(string name, double[,] m)
        {
            if (!string.IsNullOrEmpty(name)) Console.WriteLine(name);
            PrintSimpleMatrix(m);
        }

        public static void PrintSimpleMatrix(double[,] m)
        {
            int rows = m.GetLength(0);
            int cols = m.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{m[i, j],10:F2}");
                }
                Console.WriteLine();
            }
        }
    }
}