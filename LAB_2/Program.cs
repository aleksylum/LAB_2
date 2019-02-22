using System;

///Лабораторная работа №2 Кравцоваой А. В.///

namespace LAB_2
{
	public class Matrix
	{
		internal double[,] data;
		public int Rows { get; }
		public int Columns { get; }
		public int? Size
		{
			get
			{
				if (IsSquared) return Rows;
				else { return null; }//ПРОВЕРИТЬ ВОЗВР ЗНАЧ!!!

			}
		}


		///КОНСТРУКТОРЫ:///
		//ПРОВЕРЯТЬ data на NULL при попытке создать объект
		public Matrix() : this(1, 1) { }

		public Matrix(int rc) : this(rc, rc) { }

		public Matrix(int r, int c)
		{
			try
			{
				if (r == 0 || c == 0) { data = null; } //{ throw new Exception("Невозможно создать матрицу с размерностью 0"); }
				Rows = Math.Abs(r);
				Columns = Math.Abs(c);
				data = new Double[Rows, Columns];
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				data = null;
			}
		}


		//констр из массива
		public Matrix(double[,] initData)
		{
			if (initData != null)
			{
				this.Rows = initData.GetLength(0);//возвр размерности
				this.Rows = initData.GetLength(1);

				this.data = (double[,])initData.Clone();
			}
			else { this.data = null; };
		}


		///СВОЙСТВА:///


		// Является ли матрица квадратной
		public bool IsSquared { get { return (Rows == Columns); } }

		// Является ли матрица нулевой
		public bool IsEmpty
		{
			get
			{
				foreach (var i in data)
				{
					if (i != 0) { return false; }
				}
				return true;
			}
		}
		//Является ли матрица единичной
		public bool IsUnity
		{
			get
			{
				if (IsSquared && !(IsEmpty))
				{
					for (int i = 0; i < this.Rows; ++i)
					{
						if (data[i, i] != 1) { return false; }
					}

					for (int i = 0; i < this.Rows; ++i)
					{
						for (int j = 0; j < this.Columns; ++j)
						{
							if (i != j && data[i, j] != 0) { return false; }
						}
					}
					return true;
				}

				else return false;
			}
		}

		// Является ли матрица диагональной
		public bool IsDiagonal
		{
			get
			{

				if (IsSquared)
				{
					if (IsUnity || IsEmpty) { return true; }

					for (int i = 0; i < Rows; ++i)//Math.Sqrt(data.Length)
					{
						for (int j = 0; j < Columns; ++j)
						{
							if (i != j && data[i, j] != 0) { return false; }
						}
					}
					return true;
				}
				else return false;

			}

		}

		// Является ли матрица симметричной
		public bool IsSymmetric
		{
			get
			{
				if (IsSquared)
				{
					if (IsUnity || IsEmpty) { return true; }

					for (int i = 0; i < Rows; ++i)
					{
						for (int j = 0; j < Columns; ++j)
						{

							if (i != j && data[i, j] != data[j, i]) { return false; }

						}
					}
					return true;
				}
				else return false;

			}
		}

		///ОПЕРАТОРЫ:///

		public static Matrix operator +(Matrix m1, Matrix m2)
		{
			if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
			{
				throw new Exception("\nСложение матриц невозможно");
			}

			int r = m1.Rows;
			int c = m1.Columns;
			Matrix temp = new Matrix(r, c);
			for (int i = 0; i < r; ++i)
			{
				for (int j = 0; j < c; ++j)
				{

					temp.data[i, j] = m1.data[i, j] + m2.data[i, j];

				}
			}
			return temp;
		}




		public static Matrix operator -(Matrix m1, Matrix m2)
		{
			if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
			{
				throw new Exception("\nВычитание матриц невозможно");
			}

			int r = m1.Rows;
			int c = m1.Columns;
			Matrix temp = new Matrix(r, c);
			for (int i = 0; i < r; ++i)
			{
				for (int j = 0; j < c; ++j)
				{

					temp.data[i, j] = m1.data[i, j] - m2.data[i, j];

				}
			}
			return temp;
		}




		public static Matrix operator *(Matrix m1, double d)
		{
			if (m1.IsEmpty)
			{             
				return new Matrix(m1.Rows,m1.Columns);
			}

			int r = m1.Rows;
			int c = m1.Columns;
			Matrix temp = new Matrix(r, c);
			for (int i = 0; i < r; ++i)
			{
				for (int j = 0; j < c; ++j)
				{

					temp.data[i, j] = m1.data[i, j] * d;

				}
			}
			return temp;
		}



		public static Matrix operator *(Matrix m1, Matrix m2)
		{
			if (m1.Columns != m2.Rows)
			{
				throw new Exception("\nПеремножение матриц невозможно");
			}


			int r = m1.Rows;
			int c = m2.Columns;
			Matrix temp = new Matrix(r, c);
			for (int i = 0; i < r; i++)
			{
				for (int j = 0; j < c; j++)
				{
					for (int k = 0; k < m1.Columns; k++)
					{
						temp.data[i, j] += m1.data[i, k] * m2.data[k, j];
					}
				}
			}
			return temp;
		}



		//операторы преобразования типов:
		public static explicit operator Matrix(double[,] arr)
		{
			if (arr == null) { return null; }
			return new Matrix(arr);
		}//ПРОВЕРИТЬ ВОЗВР ЗНАЧ!!!




		///МЕТОДЫ:///
		//транспонирование матрицы

		public Matrix Transpose()
		{

			if (this.IsEmpty) { return this; }

			int r = this.Columns;
			int c = this.Rows;
			Matrix temp = new Matrix(r, c);

			for (int i = 0; i < r; ++i)
			{
				for (int j = 0; j < c; ++j)
				{
					temp.data[i, j] = this.data[j, i];

				}

			}
			return temp;

		}


		//След матрицы — это сумма элементов главной диагонали матрицы
		public double Trace()
		{
			if (this.IsSquared)
			{
				if (this.IsEmpty) { return 0; }
				double d = 0;
				for (int i = 0; i < this.Rows; ++i)
				{
					d += this.data[i, i];
				}
				return d;
			}
			else { throw new Exception("\nНевозможно вычислить след матрицы"); }
		}




		public void PrintMatrix()
		{
			Console.WriteLine("\n\n");
			if (this.data != null)
			{
				for (int i = 0; i < this.Rows; ++i)
				{
					for (int j = 0; j < this.Columns; ++j)
					{
						Console.Write($"{this.data[i, j]:F2}  ");
					}
					Console.Write("\n");
				}
				Console.Write("\n\n");
			}
			else { Console.WriteLine("Матрицы не существует"); }
		}


		//Реализуйте переопределение метода ToString для преобразования матрицы в строку:
		public override string ToString()
		{

			string s = "";
			for (int i = 0; i < this.Rows; ++i)
			{
				for (int j = 0; j < this.Columns; ++j)
				{
					s += String.Format("{0:F4} ", data[i, j]);

				}
			}
			return s;
		}


		//  Статические методы для порождения единичной и нулевой матрицы определенного размера:
		public static Matrix GetUnity(int Size)
		{
			//try
		//	{
				//if (Size != 0)
			//	{
			Matrix temp = new Matrix(Size);
			if (temp.data != null)
			{
				for (int i = 0; i < Size; ++i)
				{
					temp.data[i, i] = 1;

				}
			}
					return temp;
			//	}
				
			//}
			//catch (Exception e)
			//{
				//Console.WriteLine(e.Message);
				//return null;
			//}
		}
		public static Matrix GetEmpty(int Size)
		{
			//try
			//{
				//if (Size != 0)
				//{
					return new Matrix(Size);
				//}
				//else { return null; }
		//	}catch (Exception e)
			//{
				//Console.WriteLine(e.Message);
			//	return null;
			//}
		}


		// Реализуйте статические методы для создания матрицы по строчке определенном формате.

		public static bool TryParse(string s, out Matrix m)//возвр матрицу по ссылке
		{
			string[] rowStr = s.Split(new string[] { ", " }, StringSplitOptions.None);//отдельные rows
			string[] numStr = rowStr[0].Trim().Split(' ');//отдельные double 1й строки
			int r = rowStr.Length;
			int c = numStr.Length;
			m = new Matrix(r, c);
			if (r < 1 || c < 1) return false;
			for (int j = 0; j < c; j++)//для 1 строки
			{
				if (!double.TryParse(numStr[j], out double tmp))//по ссылке возвр double
				{
					return false;//->не удалось распарсить матрицу
				}
				m.data[0, j] = tmp;
			}
			for (int i = 1; i < r; i++)
			{
				numStr = rowStr[i].Trim().Split(' ');//убираем пробелы по краям, разделяем
				if (numStr.Length != c) return false;
				for (int j = 0; j < c; j++)
				{
					{
						if (!double.TryParse(numStr[j], out double tmp))
						{
							return false;
						}
						m.data[i, j] = tmp;
					}
				}
			}
			return true;
		}


		public static Matrix Parse(string s)//возвр матрицу
		{
			Matrix m;
			if (TryParse(s, out m)) { return m; }
			else { throw new FormatException("Неверный формат для матрицы\n"); }
		}
	}

	//////////////PROGRAM////////////////


	class Program
	{
		static void Main(string[] args)
		{
			StartMenu();
		}



		static void StartMenu()
		{
			while (true)
			{
				Console.WriteLine(@"
Работа с матрицами:
-------------------------------

1 - Операции над матрицами

0 - Выход
-------------------------------");
				char c = Console.ReadKey(true).KeyChar;
				switch (c)
				{
					case '1':
						Operation_Menu();
						break;
					case '0':
						return;
					default:
						break;
				}
			}
		}

		static void Operation_Menu()
		{
			while (true)
			{

				Console.WriteLine(@"
Операции над матрицами:
-------------------------------
1 - Просмотреть свойства матрицы
2 - Сложить матрицы
3 - Вычесть матрицы
4 - Умножить матрицу на число
5 - Перемножить матрицы
6 - Транспонировать матрицу
7 - Вычислить след матрицы
8 - Создать нулевую матрицу заданного размера
9 - Создать единичную матрицу заданного размера
0 - Назад
-------------------------------");
				Matrix m;
				switch (Console.ReadKey(true).KeyChar)
				{

					case '1':
						MatrixProperties();
						Console.ReadKey();
						break;
					case '2':
						m = PlusMatrix();
						m.PrintMatrix();
						SomethingElseAboutMatrix(m);
						break;
					case '3':
						m = MinusMatrix();
						m.PrintMatrix();
						SomethingElseAboutMatrix(m);
						break;
					case '4':
						m = MultiplyMatrixByNumber();
						m.PrintMatrix();
						SomethingElseAboutMatrix(m);
						break;
					case '5':
						m = MultiplyMatrix();
						m.PrintMatrix();
						SomethingElseAboutMatrix(m);
						break;
					case '6':
						m = TransposeMatrix();
						m.PrintMatrix();
						SomethingElseAboutMatrix(m);
						break;
					case '7':
						double? d = TraceMatrix();
						if (d != null) { Console.WriteLine("\nРезультат трассировки: " + d); }
						else { Console.WriteLine("\nТрассировка невозможна"); }
						Console.ReadKey();
						break;
					case '8':
						m = GetEmptyMatrix();
						if (m.data != null)
						{
							m.PrintMatrix();
							SomethingElseAboutMatrix(m);
						}
						break;
					case '9':
						m = GetUnityMatrix();
						if (m.data != null)
						{
							m.PrintMatrix();
							SomethingElseAboutMatrix(m);
						}
						break;
					case '0':
						return;
					default:
						break;
				}

			}
		}


		static void AllAboutMatrix(Matrix m)
		{
			Console.WriteLine("Матрица квадратная? " + ((m.IsSquared) ? "ДА" : "НЕТ"));
			Console.WriteLine("Матрица нулевая? " + ((m.IsEmpty) ? "ДА" : "НЕТ"));
			Console.WriteLine("Матрица единичная? " + ((m.IsUnity) ? "ДА" : "НЕТ"));
			Console.WriteLine("Матрица диагональная? " + ((m.IsDiagonal) ? "ДА" : "НЕТ"));
			Console.WriteLine("Матрица симметричная? " + ((m.IsSymmetric) ? "ДА" : "НЕТ"));
			Console.WriteLine("Строковое представление:" + m.ToString());
			if (m.Size != null)
			{
				Console.WriteLine($"Размер квадратной матрицы: {m.Size}");
			}
			else
			{
				Console.WriteLine($"Размер матрицы:{m.Rows} х {m.Columns}");

			}

		}

		static void SomethingElseAboutMatrix(Matrix m)
		{
			Console.WriteLine("\nХотите узнать свойства результирующей матрицы? \nY/N");
			switch (char.ToLower(Console.ReadKey(true).KeyChar))
			{
				case 'y':
					AllAboutMatrix(m);
					Console.ReadKey();
					return;
			}
		}

		static Matrix EnterNewMatrix()
		{
			Console.WriteLine("Введите матрицу в формате  1 1 1, 2 2 2, 3 3 3...");
			string s = Console.ReadLine();
			try
			{
				return Matrix.Parse(s);
			}
			catch (FormatException e)
			{
				Console.WriteLine(e.Message);
				return null;
			}

		}


		static void MatrixProperties()
		{
			while (true)
			{
				Matrix m = EnterNewMatrix();
				if (m != null)
				{
					AllAboutMatrix(m);
					return;
				}
			}
		}



		static Matrix PlusMatrix()
		{
			while (true)
			{
				Matrix m1 = EnterNewMatrix();
				Matrix m2 = EnterNewMatrix();
				if (m1 != null && m2 != null)
				{

					try
					{
						return m1 + m2;
					}
					catch (Exception e) { Console.WriteLine(e.Message); }
				}

			}
		}


		static Matrix MinusMatrix()
		{
			while (true)
			{
				Matrix m1 = EnterNewMatrix();
				Matrix m2 = EnterNewMatrix();
				if (m1 != null && m2 != null)
				{
					try
					{
						return m1 - m2;
					}
					catch (Exception e) { Console.WriteLine(e.Message); }
				}
			}
		}


		static Matrix MultiplyMatrixByNumber()
		{
			while (true)
			{
				Matrix m1 = EnterNewMatrix();
				double d = 0;
				if (m1 != null)
				{
					while (true)
					{
						Console.WriteLine("Введите корректный множитель:");
						if (Double.TryParse(Console.ReadLine(), out d)) { break; }
					}
				}
				return m1 * d;
			}

		}


		static Matrix MultiplyMatrix()
		{
			while (true)
			{
				Matrix m1 = EnterNewMatrix();
				Matrix m2 = EnterNewMatrix();
				if (m1 != null && m2 != null)
				{
					try
					{
						return m1 * m2;
					}
					catch (Exception e) { Console.WriteLine(e.Message); }
				}
			}
		}


		static Matrix TransposeMatrix()
		{

			while (true)
			{
				Matrix m = EnterNewMatrix();

				if (m != null)
				{
					m.PrintMatrix();
					return m.Transpose();
				}

			}

		}

		static double? TraceMatrix()
		{

			while (true)
			{
				Matrix m = EnterNewMatrix();
				if (m != null)
				{
					try
					{
						return m.Trace();
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
						return null;
					}
				}
			}
		}

		static Matrix GetEmptyMatrix()
		{

			Console.WriteLine("\nВведите размерность нулевой матрицы:");
			int i = 0;
			while (true)
			{

				if (int.TryParse(Console.ReadLine(), out i) && i != 0) { break; }
				Console.WriteLine("Неверный размер матрицы. Попробуйте еще раз:");
			}
			return Matrix.GetEmpty(i);
		}

		static Matrix GetUnityMatrix()
		{

			{
				Console.WriteLine("\nВведите размер матрицы:");

				int i = 0;

				while (true)

				{
					if (int.TryParse(Console.ReadLine(), out i) && i != 0) { break; }
					Console.WriteLine("Неверный размер матрицы. Попробуйте еще раз:");
				}

				return Matrix.GetUnity(i);///
			}
		}
	}
}