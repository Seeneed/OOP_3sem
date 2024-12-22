using System;
using System.Net.Cache;
using System.Reflection;

namespace laba11
{
    public static class Reflector
    {
        public static void WriteInFile(string text, string filePath = "info.txt")
        {
            using StreamWriter writer = new StreamWriter(filePath, true);
            writer.WriteLine(text);
            writer.Close();
        }
        public static void CleanFile(string filePath = "info.txt")
        {
            using StreamWriter writer = new StreamWriter(filePath, false);
            writer.Write("");
        }
        public static void GetAssembly(string nameClass)
        {
            Type? type = Type.GetType(nameClass);
            WriteInFile($"Тип сборки: {type.FullName}, сборка: {type.Assembly.FullName}");
        }
        public static void GetPublicConstructors(string nameClass)
        {
            Type?type = Type.GetType(nameClass);
            bool hasPublicConstructors = type.GetConstructors().Length > 0;
            foreach (var constructor in type.GetConstructors())
            {
                WriteInFile("Публичный конструктор: " + constructor.Name);
            }
        }
        public static IEnumerable<string> GetPublicMethods(string nameClass)
        {
            Type? type = Type.GetType(nameClass);
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            IEnumerable<string> result = methods.Select(method => method.Name);
            foreach (var method in result)
            {
                WriteInFile("Публичные методы: " + method);
            }
            return result;
        }
        public static IEnumerable<string> GetFieldsAndProperties(string nameClass)
        {
            Type? type = Type.GetType(nameClass);
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            IEnumerable<string> result = fields.Select(field => field.Name).Concat(properties.Select(property => property.Name));
            foreach(var item in result)
            {
                WriteInFile("Поле или свойство: " +  item);
            }
            return result;
        }
        public static IEnumerable<string> GetInterfaces(string nameClass)
        {
            Type? type = Type.GetType(nameClass);
            Type[] interfaces = type.GetInterfaces();
            IEnumerable<string> result = interfaces.Select(interfaces => interfaces.Name);
            foreach(var item in result)
            {
                WriteInFile("Реализованные интерфейсы: " + item);
            }
            return result;
        }
        public static void GetMethodsWithParameters(string nameClass, string parametr)
        {
            Type? type = Type.GetType(nameClass);
            MethodInfo[] methods = type.GetMethods();
            foreach(var method in methods)
            {
                foreach(var parametrInfo in method.GetParameters())
                {
                    if(parametrInfo.ParameterType.Name == parametr)
                    {
                        WriteInFile($"Методы, содержащие параметр {parametr}: " + method.Name);
                    }
                }
            }
        }
        public static object InvokeMethod(object obj, string methodName, object[] parameters)
        {
            Type?type = obj.GetType();
            MethodInfo method = type.GetMethod(methodName);
            return method.Invoke(obj, parameters);
        }
        public static object InvokeMethodFromFile(string nameClass, string methodName, string filePath)
        {
            Type?type = Type.GetType(nameClass);
            MethodInfo method = type.GetMethod(methodName);
            string[] fileLines = File.ReadAllLines(filePath);
            ParameterInfo[] parameters = method.GetParameters();
            object[] parametrValue = new object[parameters.Length];
            for(int i = 0; i < parameters.Length; i++)
            {
                string line = fileLines[i];
                Type parameterType = parameters[i].ParameterType;
                parametrValue[i] = Convert.ChangeType(line, parameterType);
            }
            object obj = Activator.CreateInstance(type);
            return method.Invoke(obj, parametrValue);
        }
        public static T Create<T>() where T : new()
        {
            T obj = new T();
            return obj;
        }
    }
    interface IRun
    {
        void Run();
    }
    interface ISleep
    {
        void Sleep();
    }
    public class Person : IRun, ISleep  
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
        public Person()
        {
            Name = "Денис";
            Age = 18;
        }
        public void Run()
        {
            Console.WriteLine("Человек бежит");
        }
        public void Sleep()
        {
            Console.WriteLine("Человек спит!");
        }
        public void Eat(string food)
        {
            Console.WriteLine($"Человек кушает {food}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Reflector.CleanFile();
            Reflector.GetAssembly("laba11.Person");
            Reflector.GetPublicConstructors("laba11.Person");
            Reflector.GetPublicMethods("laba11.Person");
            Reflector.GetFieldsAndProperties("laba11.Person");
            Reflector.GetInterfaces("laba11.Person");
            Reflector.GetMethodsWithParameters("laba11.Person", "Int32");
            Console.WriteLine("Данные успешно записаны в файл");
            Reflector.InvokeMethodFromFile("laba11.Person", "Eat", "parameters.txt");
            Person newPerson = Reflector.Create<Person>();
            newPerson.Name = "Даниил";
            newPerson.Age = 18;
            Console.WriteLine($"Создан новый объект: {newPerson.Name}, {newPerson.Age}");
        }
    }
}