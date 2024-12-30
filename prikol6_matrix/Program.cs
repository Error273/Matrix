using prikol6_matrix;


Matrix m1 = new Matrix(2, 3, true);
Matrix m2 = new Matrix(2, 3);
m2.matrix[0, 0] = 1;
m2.matrix[0, 1] = 2;
m2.matrix[1, 0] = 2;
m2.matrix[1, 1] = 4;
Console.WriteLine("Матрица 1:");
Console.WriteLine(m1);
Console.WriteLine("Матрица 2:");
Console.WriteLine(m2);
Console.WriteLine("M1+M2:");
Console.WriteLine(m1 + m2);

Console.WriteLine("M1-M2:");
Console.WriteLine(m1 - m2);

Console.WriteLine("M1*5:");
Console.WriteLine(m1 * 5);

Console.WriteLine("Ступенчатый вид m1");
Console.WriteLine(m1.step_view());
Console.WriteLine("Ранг m2:");
Console.WriteLine(m2.rank());

Console.WriteLine("Транспонированная m1:");
Console.WriteLine(m1.Transpose());

Matrix m3 = new Matrix(3, 2, true);
Matrix m4 = new Matrix(2, 3, true);
Console.WriteLine("Матрица 3:");
Console.WriteLine(m3);
Console.WriteLine("Матрица 4:");
Console.WriteLine(m4);
Console.WriteLine("M3*M4");
Console.WriteLine(m3*m4);

SquareMatrix m5 = new SquareMatrix(3, true);
SquareMatrix m6 = new SquareMatrix(3, true);
Console.WriteLine("M5:");
Console.WriteLine(m5);
Console.WriteLine("M6:");
Console.WriteLine(m6);
Console.WriteLine("определитель M5:");
Console.WriteLine(m5.determinant());
Console.WriteLine("Обратная m5:");
Console.WriteLine(m5.inverse());
Console.WriteLine("определитель M6:");
Console.WriteLine(m6.determinant());
Console.WriteLine("определитель M5*M6:");
Console.WriteLine((m5*m6).determinant());
