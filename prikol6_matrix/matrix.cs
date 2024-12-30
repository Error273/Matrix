namespace prikol6_matrix
{
    public class Matrix
    {
        int n, m; // n строк m столбцов
        double[,] _matrix;

        public double[,] matrix { get => _matrix; set { _matrix = value; } }
        public int N { get => n; private set { n = value; } }
        public int M { get => m; private set { m = value; } }

        public Matrix(int n, int m)
        {
            this.N = n;
            this.M = m;
            matrix = new double[n, m];
        }

        public Matrix(int n, int m, bool randomize) : this(n, m)
        {
            if (randomize)
            {
                Random random = new Random();
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        matrix[i, j] = random.Next(-10, 10);
                    }
                }
            }
        }

        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    res += matrix[i, j] + "\t";
                }
                res += "\n";
            }
            return res;
        }


        public static Matrix operator +(Matrix A, Matrix B)
        {
            if (A.n == B.n && A.m == B.m)
            {
                Matrix C = new Matrix(A.n, A.m);
                for (int i = 0; i < C.n; i++)
                {
                    for (int j = 0; j < C.m; j++)
                    {
                        C.matrix[i, j] = A.matrix[i, j] + B.matrix[i, j];
                    }
                }
                return C;
            }
            return new Matrix(0, 0);
        }

        public static Matrix operator -(Matrix A, Matrix B)
        {
            if (A.n == B.n && A.m == B.m)
            {
                Matrix C = new Matrix(A.n, A.m);
                for (int i = 0; i < C.n; i++)
                {
                    for (int j = 0; j < C.m; j++)
                    {
                        C.matrix[i, j] = A.matrix[i, j] - B.matrix[i, j];
                    }
                }
                return C;
            }
            return new Matrix(0, 0);
        }

        public static Matrix operator *(Matrix A, double b)
        {
            Matrix C = new Matrix(A.n, A.m);
            for (int i = 0; i < C.n; i++)
            {
                for (int j = 0; j < C.m; j++)
                {
                    C.matrix[i, j] = A.matrix[i, j] * b;
                }
            }
            return C;
        }

        public static Matrix operator *(double b, Matrix A)
        {
            return A * b;
        }
        public static Matrix operator /(Matrix A, double b)
        {
            if (b != 0)
            {
                return A * (1 / b);
            }
            return new Matrix(0, 0);
        }

        public static Matrix operator *(Matrix A, Matrix B)
        {
            if (A.n == B.m)
            {
                Matrix C = new Matrix(A.n, B.m);
                for (int i = 0; i < C.n; i++)
                {
                    for (int j = 0; j < C.m; j++)
                    {
                        double s = 0;
                        for (int r = 0; r < A.m; r++)
                        {
                            s += A.matrix[i, r] * B.matrix[r, j];
                        }
                        C.matrix[i, j] = s;
                    }
                }
                return C;
            }
            return new Matrix(0, 0);
        }

        public Matrix Copy()
        {
            Matrix A = new Matrix(n, m);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    A.matrix[i, j] = matrix[i, j];
                }
            }
            return A;
        }

        public Matrix triangle_view()
        {
            Matrix res = this.Copy();
            bool is_zero = true;
            for (int i = 0; i < n; i++)
            {
                // ищем ненулевую стркоу и ставим на первое место
                for (int p = 0; p < n; p++)
                {
                    if (res.matrix[p, 0] != 0)
                    {
                        res.F3(0, p);
                        is_zero = false;
                        break;
                    }
                }
                if (is_zero)
                {
                    return res;
                }
                for (int j = i + 1; j < n; j++)
                {
                    res.F2(j, i, -res.matrix[j, i] / res.matrix[i, i]);
                }

            }
            return res;
        }

        public Matrix step_view()
        {
            Matrix res = this.triangle_view();
            for (int i = 0; i < n; i++)
            {
                if (res.matrix[i, i] != 0)
                {
                    res.F1(i, 1 / res.matrix[i, i]); // делим всю строку на диагональный элемент
                }
            }
            return res;
        }

        public int rank()
        {
            Matrix step = this.step_view();
            int res = n;
            int zero_counter = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (step.matrix[i, j] == 0)
                    {
                        zero_counter++;
                    }
                }
                if (zero_counter == m)
                {
                    res -= 1;
                }
                zero_counter = 0;
            }
            return res;
        }

        public Matrix Transpose()
        {
            Matrix res = new Matrix(m, n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    res.matrix[j, i] = matrix[i, j];
                }
            }
            return res;
        }

        public void F3(int i1, int i2) // ЭП 3 типа, меняет строки в матрице местами
        {
            for (int j = 0; j < m; j++)
            {
                var t = matrix[i1, j];
                matrix[i1, j] = matrix[i2, j];
                matrix[i2, j] = t;
            }
        }

        public void F2(int i1, int i2, double k) // ЭП 2 типа, умножает строку i2 на k и прибавляет к i1
        {
            for (int j = 0; j < m; j++)
            {
                matrix[i1, j] += matrix[i2, j] * k;
            }
        }

        public void F1(int i, double k) // ЭП 1 типа, умножает всю строку на k
        {
            for (int j = 0; j < m; j++)
            {
                matrix[i, j] *= k;
            }
        }
    }


    public class SquareMatrix : Matrix
    {
        public SquareMatrix(int n) : base(n, n) { }
        public SquareMatrix(int n, bool randomize) : base(n, n, randomize) { }

        public SquareMatrix(double[,] matrix, int n) : this(n)
        {
            this.matrix = (double[,])matrix.Clone();
        }
        public static SquareMatrix operator +(SquareMatrix A, SquareMatrix B)
        {
            Matrix res = (Matrix)A + (Matrix)B;
            return new SquareMatrix(res.matrix, A.N);
        }

        public static SquareMatrix operator -(SquareMatrix A, SquareMatrix B)
        {
            Matrix res = (Matrix)A - (Matrix)B;
            return new SquareMatrix(res.matrix, A.N);
        }

        public static SquareMatrix operator *(SquareMatrix A, double b)
        {
            Matrix res = (Matrix)A * b;
            return new SquareMatrix(res.matrix, A.N);
        }

        public static SquareMatrix operator *(double b, SquareMatrix A)
        {
            Matrix res = (Matrix)A * b;
            return new SquareMatrix(res.matrix, A.N);
        }

        public static SquareMatrix operator /(SquareMatrix A, double b)
        {
            Matrix res = (Matrix)A / b;
            return new SquareMatrix(res.matrix, A.N);
        }

        public static SquareMatrix operator *(SquareMatrix A, SquareMatrix B)
        {
            Matrix res = (Matrix)A * (Matrix)B;
            return new SquareMatrix(res.matrix, A.N);
        }

        public SquareMatrix triangle_view()
        {
            Matrix res = (Matrix)this;
            res = res.triangle_view();
            return new SquareMatrix(res.matrix, this.N);
        }

        public SquareMatrix step_view()
        {
            Matrix res = (Matrix)this;
            res = res.step_view();
            return new SquareMatrix(res.matrix, this.N);
        }

        public SquareMatrix Transpose()
        {
            Matrix res = (Matrix)this;
            res = res.Transpose();
            return new SquareMatrix(res.matrix, this.N);
        }

        public double trace()
        {
            double res = 0;
            for (int i = 0; i < N; i++)
            {
                res += matrix[i, i];
            }
            return res;
        }

        public double determinant()
        {
            double res = 1;
            Matrix step = this.triangle_view();
            for (int i = 0; i < N; i++)
            {
                res *= step.matrix[i, i];
            }
            return res;
        }

        public Matrix inverse()
        {
            double det = this.determinant();
            Matrix calc = new Matrix(N, 2 * N); // матрциа для вычисления
            SquareMatrix res = new SquareMatrix(N); // результат
            if (det != 0) // проверка на определитель
            {
                for (int i = 0; i < N; i++) // заполняем calc, слева исходная матрица, справа единичная
                {
                    for (int j = 0; j < 2 * N; j++)
                    {
                        if (j < N)
                        {
                            calc.matrix[i, j] = matrix[i, j];
                        }
                        else
                        {
                            calc.matrix[i, j] = (i == (j - N)) ? 1 : 0;
                        }
                    }
                }
                calc = calc.step_view(); // приводим к ступенчатому виду левую часть
                for (int i = N - 1; i >= 1; i--) // приводим к единичной левую часть
                {
                    for (int j = i - 1; j >= 0; j--)
                    {
                        calc.F2(j, i, -calc.matrix[j, i]);
                    }
                }
                // считываем правую часть
                for (int i = 0; i < N; i++)
                {
                    for (int j = N; j < 2 * N; j++)
                    {
                        res.matrix[i, j - N] = calc.matrix[i, j];
                    }
                }
                
            }
            return res;
        }
    }
}