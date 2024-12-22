using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace laba9
{
    interface IList<T>
    {
        public int Add(T value);
        public void Clear();
        public bool Contains(T value);
        public int IndexOf(T value);
        public void Insert(int index, T value);
        public void RemoveAt(int index);
        public void Remove(T value);
        public void Print();
        public int Count();
    }
    public class Software
    {
        public string nameSoftware { get; set; }
        public int dateOfRelease { get; set; }
        public Software(string name, int date)
        {
            this.dateOfRelease = date;
            this.nameSoftware = name;
        }
        public override string ToString()
        {
            return $"Название программного обеспечения: {nameSoftware}, дата выхода программного обеспечения: {dateOfRelease}";
        }
    }
    public class SimpleList<T> : IList<T>
    {
        public SortedList<int, T> list = new SortedList<int, T>();
        public int Add(T value)
        {
            if (!Contains(value))
            {
                list.Add(list.Count, value);
                return list.Count - 1;
            }
            return -1;
        }
        public void Clear() 
        {
            list.Clear();
        }
        public bool Contains(T value) 
        {
            return list.ContainsValue(value);
        }
        public int IndexOf(T value) 
        {
            foreach(var index in list)
            {
                if(index.Value.Equals(value))
                {
                    return index.Key;
                }
            }
            return -1;
        }
        public void Print()
        {
            foreach(var item in list.Values)
            {
                Console.WriteLine(item);
            }
        }
        public void Insert(int index, T value)
        {
            if(!Contains(value))
            {
                list.Add(index, value);
            }
            else
            {
                Console.WriteLine("Элемент с таким индексом уже существует");
            }
        }
        public void RemoveAt(int index)
        {
            if(list.ContainsKey(index))
            {
                list.Remove(index);
            }
            else
            {
                Console.WriteLine($"Элемент с индексом {index} не найден");
            }

        }
        public void Remove(T value)
        {
            var item = IndexOf(value);
            if(item < 0)
            {
                Console.WriteLine($"{item} не найден");
            }
            else
            {
                list.Remove(item);
            }
        }
        public int Count()
        {
            return list.Count;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            IList<Software> list = new SimpleList<Software>();
            Software software1 = new Software("Microsoft Visual Studio", 1997);
            Software software2 = new Software("Android", 2008);
            list.Add(software1);
            list.Add(software2);
            Console.WriteLine("Список, содержащий информацию о ПО: ");
            list.Print();
            list.Remove(software1);
            Console.WriteLine("Список после удаления элемента: ");
            list.Print();
            Software software3 = new Software("IOS", 2007);
            list.Insert(0, software3);
            Console.WriteLine("Список после добавления элемента по индексу: ");
            list.Print();
            Console.WriteLine($"Количество элементов в списке: {list.Count()}");
            Console.WriteLine();

            var firstCollection = new List<int> { 1, 2, 3 };
            Console.WriteLine("Элементы обобщенной коллекции: ");
            foreach(int item in firstCollection)
            {
                Console.Write(item + " ");
            }
            firstCollection.Add(4);
            firstCollection.Insert(4, 5);
            Console.WriteLine();
            Console.WriteLine("Коллекция после добавления элементов: ");
            foreach (int item in firstCollection)
            {
                Console.Write(item + " ");
            }
            firstCollection.RemoveRange(0, 3);
            Console.WriteLine();
            Console.WriteLine("Коллекция после удаления последовательных элементов: ");
            foreach (int item in firstCollection)
            {
                Console.Write(item + " ");
            }
            var secondCollection = new Dictionary<int, int>();
            for(int i = 0; i < firstCollection.Count; i++)
            {
                secondCollection[i] = firstCollection[i];
            }
            Console.WriteLine();
            Console.WriteLine("Элементы второй коллекции: ");
            foreach(var item in secondCollection)
            {
                Console.Write($"{item.Key} {item.Value} ");
            }
            Console.WriteLine();
            Console.WriteLine("Искомое значение второй коллекции: ");
            bool found = false;
            foreach(var item in secondCollection)
            {
                if(item.Value == 5)
                {
                    Console.WriteLine("Значение найдено: " + item.Value);
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Console.WriteLine("Такого значения нет в коллекции");
            }
            Console.WriteLine();

            Console.WriteLine("Наблюдаемая коллекция: ");
            var observableCollection = new ObservableCollection<Software>();
            observableCollection.CollectionChanged += CollectionChangedSoftware;
            Software software4 = new Software("Microsoft Excel", 1988);
            Software software5 = new Software("Microsoft Word", 1983);
            observableCollection.Add(software4);
            observableCollection.Add(software5);
            observableCollection.Remove(software4);
            void CollectionChangedSoftware(object sender, NotifyCollectionChangedEventArgs e)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        if (e.NewItems?[0] is Software newSoftware)
                        {
                            Console.WriteLine($"Добавлен новый объект: {newSoftware.nameSoftware}");
                        }
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        if (e.OldItems?[0] is Software oldSoftware)
                        {
                            Console.WriteLine($"Удален объект: {oldSoftware.nameSoftware}");
                        }
                        break;
                }
            }
        }
    }
}