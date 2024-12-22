using System;

namespace laba3
{
    public class Set
    {
        private List<int> elements;
        public class Production
        {
            public int Id;
            public string nameOrganization;
            public Production(int id, string organizatgion)
            {
                this.Id = id;
                this.nameOrganization = organizatgion;
            }
            public override string ToString()
            {
                return $"Название организации: {nameOrganization}, ID: {Id}";
            }
        }
        public class Developer
        {
            public int Id;
            public string nameCompany;
            public string fullname;
            public Developer(int id, string company, string fio)
            {
                this.Id = id;
                this.nameCompany = company;
                this.fullname = fio;
            }
            public override string ToString()
            {
                return $"ФИО разработчика: {fullname}, Компания: {nameCompany}, ID: {Id}";
            }
        }
        public Production Prod { get; set; }
        public Developer Dev {  get; set; }
        public Set()
        {
            elements = new List<int>();
        }
        public void addElements(int el)
        {
            bool foundElem = false;
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i] == el)
                {
                    foundElem = true;
                    break; 
                }
            }
            if (!foundElem) 
            {
                elements.Add(el);
            }
        }
        public static Set operator ++(Set set)
        {
            Random random = new Random();
            int randomElement = random.Next(1, 99);
            set.addElements(randomElement);
            return set;
        }
        public static Set operator +(Set set1, Set set2)
        {
            Set result = new Set();
            for(int i = 0; i < set1.elements.Count; i++)
            {
                result.addElements(set1.elements[i]);
            }
            for(int i = 0; i < set2.elements.Count; i++)
            {
                result.addElements(set2.elements[i]);
            }
            return result;
        }
        public static bool operator <=(Set set1, Set set2)
        {
            for(int i = 0;i < set1.elements.Count;i++)
            {
                bool found = false;
                for(int j = 0;j < set2.elements.Count;j++)
                {
                    if (set1.elements[i] == set2.elements[j])
                    {
                        found = true;
                    }
                }
                if(!found)
                    return false;
            }
            return true;
        }
        public static bool operator >=(Set set1, Set set2)
        {
            return set2 <= set1;
        }

        public static implicit operator int(Set set)
        {
            return set.elements.Count;
        }
        public int this[int index]
        {
            get
            {
                if(index < 0 || index >= elements.Count)
                {
                    Console.WriteLine("Ошибка");
                    return 0;
                }
                return elements[index];
            }
        }
        public override string ToString()
        {
            string result= "{";
            for(int i=0; i < elements.Count; i++)
            {
                result += elements[i].ToString();
                if(i < elements.Count - 1) 
                    result += ",";
            }
            result += "}";
            return result;
        }
        public bool isOrdered()
        {
            for(int i = 0;  i < elements.Count - 1; i++)
            {
                if (elements[i + 1] < elements[i])
                {
                    return false;
                }
            }
            return true;
        }
        public static string encrypt(string input)
        {
            string result = "";
            for(int i = input.Length - 1; i >= 0; i--)
            {
                result += input[i];
            }
            return result;
        }
        public int Sum()
        {
            int sum = 0;
            foreach(int item in elements)
            {
                sum += item;
            }
            return sum;
        }
        public int Max()
        {
            if(elements.Count == 0)
            {
                Console.WriteLine("Множество пусто");
                return 0;
            }
            else 
            {
                int max = elements[0];
                foreach (int item in elements)
                {
                    if (item > max)
                    {
                        max = item;
                    }
                }
                return max;
            }
        }
        public int Min()
        {
            if( elements.Count == 0)
            {
                Console.WriteLine("Множество пусто");
                return 0;
            }
            else
            {
                int min = elements[0];
                foreach(int item in elements)
                {
                    if(item < min)
                    {
                        min = item;
                    }
                }
                return min;
            }
        }
    }
    public static class StatisticOperation
    {
        public static int Sum(Set set)
        {
            return set.Sum();
        }
        public static int different(Set set)
        {
            return set.Max() - set.Min();
        }
        public static int Count(Set set)
        {
            return set;
        }
        public static string Encrypt(this string str)
        {
            return Set.encrypt(str);
        }
        public static bool IsOrdered(this Set set)
        {
            return set.IsOrdered();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Set set1 = new Set();
            Set set2 = new Set();
            set1.addElements(2);
            set1.addElements(5);
            set1.addElements(6);
            set2.addElements(9);
            set2.addElements(14);
            Console.WriteLine($"Элементы первого множества: {set1.ToString()}");
            Console.WriteLine($"Элементы второго множества: {set2.ToString()}");
            Set unionSet = set1 + set2;
            Console.WriteLine($"Объединенное множество: {unionSet}");
            unionSet++;
            Console.WriteLine($"Множество после добавления случайного элемента: {unionSet}");
            Console.WriteLine($"Сравнение множеств с помощью оператора <=: {set1 <= set2}");
            Console.WriteLine($"Сравнение множеств с помощью оператора >=: {set1 >= set2}");
            Console.WriteLine($"Мощность множества: {(int)unionSet}");
            Console.WriteLine($"Сумма элементов множества: {unionSet.Sum()}");
            Console.WriteLine($"Максимальный элемент множества: {unionSet.Max()}");
            Console.WriteLine($"Минимальный элемент множества: {unionSet.Min()}");
            int different = unionSet.Max() - unionSet.Min();
            Console.WriteLine($"Разность между максимальным и минимальным элементами множества: {different}");
            Console.Write("Введите индекс элемента множества: ");
            int index = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Элемент множества под индексом {index}: {unionSet[index]}");
            Console.Write("Введите строку, которую хотите зашифровать: ");
            string str = Convert.ToString(Console.ReadLine());
            Console.WriteLine($"Зашифрованная строка: {str.Encrypt()}");
            Console.WriteLine($"Проверка на упорядоченность массива {unionSet}: {unionSet.isOrdered()}");
            Set.Production prod = new Set.Production(7, "Microsoft");
            Set.Developer dev = new Set.Developer(10, "Google", "Мамонько Денис Александрович");
            set1.Prod = prod;
            set2.Dev = dev;
            Console.WriteLine(prod.ToString());
            Console.WriteLine(dev.ToString());
        }
    }
}
