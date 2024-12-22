using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text.Json;

namespace laba7
{
    interface IActions<T>
    {
        public void Add(T element);
        public void Remove(T element);
        public void Show();
        public void FindByPredicate(Predicate<T> predicate);
    }
    public class CollectionType<T> : IActions<T> where T : new()
    {
        public List<T> elements = new List<T> ();
        public void Add(T element)
        {
            if(!elements.Contains(element))
            {
                elements.Add(element);
            }
        }
        public void Remove(T element)
        {
            if(elements.Contains (element))
            {
                elements.Remove (element);
            }
            else
            {
                Console.WriteLine("Элемент в коллекции не найден");
            }
        }
        public void Show()
        {
            foreach(var element in elements)
            {
                Console.WriteLine(element);
            }
        }
        public void FindByPredicate(Predicate<T> predicate)
        {
            bool foundElem = false;
            for(int i = 0; i < elements.Count;i++)
            {
                if (predicate(elements[i]))
                {
                    Console.WriteLine(elements[i]);
                    foundElem = true;
                }
            }
        }
        public void SaveToFile(string fileName)
        {
            try
            {
                using StreamWriter sw = new StreamWriter(fileName);
                var str = JsonSerializer.Serialize(elements);
                sw.WriteLine(str);
            }
            finally
            {
                Console.WriteLine("Коллекция успешно сохранена в файл");
            }
        }
        public void ReadFromFile(string fileName)
        {
            try
            {
                using StreamReader sr = new StreamReader(fileName);
                var str = sr.ReadToEnd();
                elements = JsonSerializer.Deserialize<List<T>>(str);
                Show();
            }
            finally
            {
                Console.WriteLine("Коллекция успешно прочитана из файла");
            }
        }
    }
    public class Person
    {
        public string name { get; set; }
        public int age { get; set; }
        public int id { get; set; }

        public Person() { }
        public Person(string name, int age, int id)
        {
            this.name = name;
            this.age = age;
            this.id = id;
        }
        public override string ToString()
        {
            return $"Имя человека: {name}, Возраст человека: {age}, ID человека: {id}";
        }
        public override bool Equals(object obj)
        {
            if (obj is Person other)
            {
                return other.name == name && other.age == age && other.id == id;
            }
            return false;
        }
    }
    class Program
    {
        static void Main(string[] args) 
        {
            CollectionType<int> intCollection = new CollectionType<int>();
            intCollection.Add(1);
            intCollection.Add(2);
            intCollection.Add(3);
            Console.WriteLine("Элементы коллекции: ");
            intCollection.Show();
            intCollection.Remove(1);
            Console.WriteLine("Коллекция после удаления: ");
            intCollection.Show();
            Console.WriteLine();

            Person person1 = new Person("Денис", 18, 14);
            Person person2 = new Person("Даниил", 18, 16);
            Person person3 = new Person("Кирилл", 18, 26);

            CollectionType<Person> personType = new CollectionType<Person>();
            personType.Add(person1);
            personType.Add(person2);
            personType.Add(person3);
            Console.WriteLine("Элементы коллекции: ");
            personType.Show();
            Console.WriteLine("Поиск по предикату: ");
            personType.FindByPredicate(p => p.id == 14);
            personType.Remove(person1);
            Console.WriteLine("Коллекция после удаления: ");
            personType.Show();
            personType.SaveToFile("List.json");
            Console.WriteLine("Чтение коллекции из файла: ");
            personType.ReadFromFile("List.json");
        }
    }
}
