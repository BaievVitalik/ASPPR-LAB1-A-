using System;
using System.Text;

namespace MatrixPracticalWork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            //// Вхідні дані(Варіант 4)

            //matrixA = {
            //    { 3, 2, 2 },

            //    { 1, -2, 5 },

            //    { -2, -3, 4 }
            //};

            //vectorB = { 1, 2, 3 };

            try
            {
                Console.Write("Введіть розмірність матриці А (m x n): ");
                string[] sizeInput = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (sizeInput.Length < 2) throw new Exception("Потрібно ввести два числа (рядки та стовпці).");

                int m = int.Parse(sizeInput[0]);
                int n = int.Parse(sizeInput[1]);

                double[,] matrixA = new double[m, n];
                for (int i = 0; i < m; i++)
                {
                    Console.Write($"{i + 1} ряд: ");
                    string[] rowValues = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (rowValues.Length < n) throw new Exception($"Недостатньо елементів у рядку. Очікувалося: {n}.");

                    for (int j = 0; j < n; j++)
                    {
                        matrixA[i, j] = double.Parse(rowValues[j]);
                    }
                }

                Console.WriteLine();

                double[] vectorB = null;
                Console.Write($"Чи потрібно ввести матрицю B ({m} x 1)? (Y / N): ");
                string choice = Console.ReadLine().Trim().ToUpper();

                if (choice == "Y" || choice == "Т")
                {
                    vectorB = new double[m];
                    for (int i = 0; i < m; i++)
                    {
                        Console.Write($"{i + 1}р: ");
                        vectorB[i] = double.Parse(Console.ReadLine());
                    }
                }
                else
                {
                    Console.WriteLine("Введення матриці B пропущено. Завдання 3 (СЛАР) не буде виконано.");
                }

                Console.WriteLine("\nВведена матриця А:");
                MatrixOperations.PrintSimpleMatrix(matrixA);

                if (vectorB != null)
                {
                    Console.WriteLine("\nВведена матриця B:");
                    foreach (var v in vectorB) Console.WriteLine($"{v,10:F2}");
                }
                Console.WriteLine(new string('-', 40));

                bool isSquare = (m == n);

                Console.WriteLine("Завдання 1. Знайти обернену матрицю C = A^-1:");
                if (isSquare)
                {
                    double[,] inverseMatrix = MatrixOperations.CalculateInverseMatrix(matrixA);
                    Console.WriteLine("Остаточна обернена матриця C = A^-1 =");
                    MatrixOperations.PrintMatrix("", inverseMatrix);
                }
                else
                {
                    Console.WriteLine("Помилка: Обернена матриця існує тільки для квадратних матриць (m = n).\n");
                }
                Console.WriteLine(new string('-', 40));

                Console.WriteLine("Завдання 2. Пошук рангу матриці A:");
                int rank = MatrixOperations.CalculateRank(matrixA);
                Console.WriteLine($"R = {rank}");
                Console.WriteLine(new string('-', 40));

                Console.WriteLine("Завдання 3. Розв'язати систему лінійних алгебраїчних рівнянь");
                if (isSquare && vectorB != null)
                {
                    Console.WriteLine();
                    double[,] inverseC = MatrixOperations.CalculateInverseMatrix(matrixA, false);
                    MatrixOperations.SolveMethod1(matrixA, inverseC, vectorB);
                }
                else if (!isSquare)
                {
                    Console.WriteLine("Помилка: Розв'язання СЛАР через обернену матрицю можливе тільки для квадратних систем.");
                }
                else
                {
                    Console.WriteLine("Завдання 3. Пропущено, оскільки матриця B не була введена.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\n[ПОМИЛКА]: Введено некоректний символ. Будь ласка, використовуйте лише числа.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ПОМИЛКА]: {ex.Message}");
            }

            Console.WriteLine(new string('-', 40));
            Console.WriteLine("Програму завершено. Натисніть будь-яку клавішу...");
            Console.ReadKey();
        }
    }
}