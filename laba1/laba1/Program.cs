using System;
using System.Globalization;
using System.Text;
using System.Transactions;

namespace FirstAplication
{
    class Lab1
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Введите bool");
            bool Bool = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine(Bool);
            Console.WriteLine("Введите byte");
            byte Byte = Convert.ToByte(Console.ReadLine());
            Console.WriteLine(Byte);
            Console.WriteLine("Введите sbyte");
            sbyte Sbyte = Convert.ToSByte(Console.ReadLine());
            Console.WriteLine(Sbyte);
            Console.WriteLine("Введите char");
            char Char = Convert.ToChar(Console.ReadLine());
            Console.WriteLine(Char);
            Console.WriteLine("Введите decimal");
            decimal Decimal = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine(Decimal);
            Console.WriteLine("Введите Double");
            double Double = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine(Double);
            Console.WriteLine("Введите float");
            float Float = Convert.ToSingle(Console.ReadLine());
            Console.WriteLine(Float);
            Console.WriteLine("Введите int");
            int Int = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(Int);
            Console.WriteLine("Введите uint");
            uint Uint = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine(Uint);
            Console.WriteLine("Введите nint");
            nint Nint = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(Nint);
            Console.WriteLine("Введите nuint");
            nuint Nuint = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine(Nuint);
            Console.WriteLine("Введите long");
            long Long = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine(Long);
            Console.WriteLine("Введите ulong");
            ulong Ulong = Convert.ToUInt64(Console.ReadLine());
            Console.WriteLine(Ulong);
            Console.WriteLine("Введите short");
            short Short = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine(Short);
            Console.WriteLine("Введите ushort");
            ushort Ushort = Convert.ToUInt16(Console.ReadLine());
            Console.WriteLine(Ushort);

            short s = 45;
            byte b = (byte)s;

            int a = 14;
            long l = (long)a;

            double d = 7236.123;
            int k = (int)d;

            char symbol = 'K';
            short s1 = (short)symbol;

            float f1 = 1.5465f;
            d = (double)f1;

            int i = 100;
            long l1 = i;

            float f2 = 23.12f;
            double d1 = f2;

            char c = 'D';
            int i1 = c;

            byte b1 = 45;
            int i2 = b1;

            short s2 = 30;
            float f3 = s2;

            int x = 228;
            object obj = x;

            object obj1 = 432;
            int y = (int)obj1;

            var i3 = 5;
            var str = "Hello";

            int? nullableInt = null;

            string str1 = "Растение";
            string str2 = "Свадьба";
            string str3 = "Холодильник";

            string concatStr = string.Concat(str1, str2, str3);
            Console.WriteLine(concatStr);
            string copyStr = string.Copy(str1);
            Console.WriteLine(copyStr);
            string subStr = str3.Substring(2, 4);
            Console.WriteLine(subStr);

            string phrase = "My name is Gustavo";
            string[] words = phrase.Split(' ');
            foreach (var word in words)
            {
                Console.WriteLine(word);
            }

            string insertStr = str1.Insert(2, str2);
            Console.WriteLine(insertStr);
            string str4 = "Unaversity";
            string replaceStr = str4.Replace("Unaversity", "University");
            Console.WriteLine(replaceStr);
            string myName = "Denis";
            string interpolStr = $"My name is {myName}";
            Console.WriteLine(interpolStr);

            string strNull = null;
            string strEmpty = "";
            if (string.IsNullOrEmpty(strNull))
            {
                Console.WriteLine("Строка пустая или инициализирована в Null");
            }
            if (string.IsNullOrEmpty(strEmpty))
            {
                Console.WriteLine("Строка пустая или инициализирована в Null");
            }
            Console.WriteLine("Длина пустой строки: " + strEmpty.Length);
            strNull += "текст";
            Console.WriteLine("Строка после конкантенации: " + strNull);

            StringBuilder strBuild = new StringBuilder("Некоторая строка");
            Console.WriteLine("Строка в начале: " + strBuild);
            strBuild.Remove(5, 4);
            Console.WriteLine("Строка после удаления: " + strBuild);
            strBuild.Insert(0, "текст в старт ");
            strBuild.Append(" текст в конец ");
            Console.WriteLine("Финальный вид строки: " + strBuild);

            int[,] matrix = {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };
            for (int m = 0; m < matrix.GetLength(0); m++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[m, j] + " ");
                }
                Console.WriteLine();
            }

            string[] stringArray = new string[]
            { "яблоко", "груша", "банан", "слива", "черника" };
            Console.WriteLine("Массив: ");
            for(int n = 0; n < stringArray.Length; n++)
            {
                Console.WriteLine(n + ": " + stringArray[n]);
            }
            Console.WriteLine("Длина массива: " + stringArray.Length);
            Console.WriteLine("Введите номер позиции, который хотите заменить (0-4)");
            int position = Convert.ToInt32(Console.ReadLine());
            if(position >= 0 && position < stringArray.Length)
            {
                Console.Write("Введите новое значение: ");
                string newValue = Console.ReadLine();
                stringArray[position] = newValue;
                Console.WriteLine("Новый массив: ");
                for(int h = 0; h < stringArray.Length; h++)
                {
                    Console.Write(h + " : " + stringArray[h] + " ");
                }
            }
            else
            {
                Console.WriteLine("Такой позиции нет");
            }

            int[][] jaggedArray = new int[3][];
            jaggedArray[0] = new int[2];
            jaggedArray[1] = new int[3];
            jaggedArray[2] = new int[4];
            for (int q = 0; q < jaggedArray.Length; q++)
            {
                for (int j = 0; j < jaggedArray[q].Length; j++)
                {
                    Console.Write($"\nВведите значение для ступенчатого массива[{q}][{j}]: ");
                    jaggedArray[q][j] = Convert.ToInt32(Console.ReadLine());
                }
            }
            Console.WriteLine("Ступенчатый массив:");
            for (int q = 0; q < jaggedArray.Length; q++)
            {
                for (int j = 0; j < jaggedArray[q].Length; j++)
                {
                    Console.Write(jaggedArray[q][j] + " ");
                }
                Console.WriteLine();
            }

            var intArray = new int[] { 1, 2, 3 };
            for(int q = 0;q < intArray.Length; q++)
            {
                Console.WriteLine("Массив: " + intArray[q] + " ");
            }
            var stringArr = "Строка";
            Console.WriteLine("Неявно типизированная переменная для строки: " + stringArr);

            var firstTurple = (1, "школа", 'A', "оценка", 409238492834094829L);
            Console.WriteLine("Кортеж: " + firstTurple);
            Console.WriteLine(firstTurple.Item1);
            Console.WriteLine(firstTurple.Item3);
            Console.WriteLine(firstTurple.Item4);

            (int number, string place, char letter, string description, long id) = firstTurple;
            Console.WriteLine(number);
            Console.WriteLine(place);
            Console.WriteLine(letter);
            Console.WriteLine(description);
            Console.WriteLine(id);
            var(number_two, place_two, letter_two, description_two, id_two) = firstTurple;
            Console.WriteLine(number_two);
            Console.WriteLine(place_two);
            Console.WriteLine(letter_two);
            Console.WriteLine(description_two);
            Console.WriteLine(id_two);
            var (number_three, _, letter_three, _, id_three) = firstTurple;
            Console.WriteLine(number_three);
            Console.WriteLine(letter_three);
            Console.WriteLine(id_three);

            var secongTurple = (1, 5, 6, 7, "a");
            var thirdTurple = (1, 6, 5, 7, "a");
            if(secongTurple == thirdTurple)
            {
                Console.WriteLine("Кортежи равны между собой");
            }
            else
            {
                Console.WriteLine("Кортежи не равны");
            }

            int[] numbers = { 1, 2, 3, 4, 5 };
            string text = "школа";
            (int max, int min, int sum, char firstLetter) localFunction(int[] nums, string str)
            {
                int max = nums[0];
                int min = nums[0];
                int sum = 0;
                foreach (int num in nums)
                {
                    if (num > max) max = num;
                    if (num < min) min = num;
                    sum += num;
                }
                char firstLetter = str[0];
                return (max, min, sum, firstLetter);
            }
            var result = localFunction(numbers, text);
            Console.WriteLine($"Максимальное число: {result.max}");
            Console.WriteLine($"Минимальное число: {result.min}");
            Console.WriteLine($"Сумма чисел: {result.sum}");
            Console.WriteLine($"Первая буква: {result.firstLetter}");

            void CheckedFunction()
            {
                try
                {
                    checked
                    {
                        int maxInt = int.MaxValue;
                        Console.WriteLine(maxInt + 1);
                    }
                }
                catch (OverflowException e)
                {
                    Console.WriteLine("Checked: " + e.Message);
                }
            }
            void UncheckedFunction()
            {
                unchecked
                {
                    int maxInt = int.MaxValue;
                    Console.WriteLine(maxInt + 1);
                }
            }
            CheckedFunction();
            UncheckedFunction();
        }
    }
}