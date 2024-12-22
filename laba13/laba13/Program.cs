using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace laba13
{
    public interface ISerialize
    {
        void Serialize<T>(T obj, string filePath);
        T Deserealize<T>(string filePath);
    }

    public class BinarySerializer : ISerialize
    {
        public void Serialize<T>(T obj, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
#pragma warning disable SYSLIB0011
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, obj);
#pragma warning restore SYSLIB0011
            }
        }

        public T Deserealize<T>(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
#pragma warning disable SYSLIB0011
                BinaryFormatter bf = new BinaryFormatter();
                return (T)bf.Deserialize(fs);
#pragma warning restore SYSLIB0011
            }
        }
    }

    public class SoapSerializer : ISerialize
    {
        public void Serialize<T>(T obj, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
#pragma warning disable SYSLIB0011
                SoapFormatter sf = new SoapFormatter();
                sf.Serialize(fs, obj);
#pragma warning restore SYSLIB0011
            }
        }

        public T Deserealize<T>(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
#pragma warning disable SYSLIB0011
                SoapFormatter sf = new SoapFormatter();
                return (T)sf.Deserialize(fs);
#pragma warning restore SYSLIB0011
            }
        }
    }

    public class JsonSerealizer : ISerialize
    {
        public void Serialize<T>(T obj, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                JsonSerializer.Serialize(fs, obj, options);
            }
        }

        public T Deserealize<T>(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                return JsonSerializer.Deserialize<T>(fs);
            }
        }
    }

    public class XmlSerealizer : ISerialize
    {
        public void Serialize<T>(T obj, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                xs.Serialize(fs, obj);
            }
        }

        public T Deserealize<T>(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                return (T)xs.Deserialize(fs);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Exam exam1 = new Exam("Теория вероятности и математическая статистика", 2, "Денис");

            string binaryFile = "binary.dat";
            string soapFile = "soap.xml";
            string jsonFile = "json.json";
            string xmlFile = "xml.xml";

            ISerialize binarySerialization = new BinarySerializer();
            ISerialize soapSerialization = new SoapSerializer();
            ISerialize jsonSerialization = new JsonSerealizer();
            ISerialize xmlSerialization = new XmlSerealizer();

            try
            {
                binarySerialization.Serialize(exam1, binaryFile);
                Console.WriteLine("Binary сериализация выполнена успешно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка Binary сериализации: {ex.Message}");
            }

            try
            {
                soapSerialization.Serialize(exam1, soapFile);
                Console.WriteLine("SOAP сериализация выполнена успешно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка SOAP сериализации: {ex.Message}");
            }

            try
            {
                jsonSerialization.Serialize(exam1, jsonFile);
                Console.WriteLine("JSON сериализация выполнена успешно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка JSON сериализации: {ex.Message}");
            }

            try
            {
                xmlSerialization.Serialize(exam1, xmlFile);
                Console.WriteLine("XML сериализация выполнена успешно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка XML сериализации: {ex.Message}");
            }

            try
            {
                var deserializedBinary = binarySerialization.Deserealize<Exam>(binaryFile);
                Console.WriteLine($"Binary десериализация: {deserializedBinary}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка Binary десериализации: {ex.Message}");
            }

            try
            {
                var deserializedSoap = soapSerialization.Deserealize<Exam>(soapFile);
                Console.WriteLine($"SOAP десериализация: {deserializedSoap}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка SOAP десериализации: {ex.Message}");
            }

            try
            {
                var deserializedJson = jsonSerialization.Deserealize<Exam>(jsonFile);
                Console.WriteLine($"JSON десериализация: {deserializedJson}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка JSON десериализации: {ex.Message}");
            }

            try
            {
                var deserializedXml = xmlSerialization.Deserealize<Exam>(xmlFile);
                Console.WriteLine($"XML десериализация: {deserializedXml}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка XML десериализации: {ex.Message}");
            }

            Exam exam2 = new Exam("КПО", 2, "Даниил");
            Exam exam3 = new Exam("АиСД", 3, "Кирилл");
            Exam exam4 = new Exam("КСиС", 2, "Влад");
            Exam[] exams = new Exam[] { exam2, exam3, exam4 };
            string binaryFilePath = "binaryFile.dat";
            string soapFilePath = "soapFile.xml";
            string xmlFilePath = "xmlFile.xml";
            string jsonFilePath = "jsonFile.json";

            try
            {
                binarySerialization.Serialize(exams, binaryFilePath);
                Console.WriteLine("Binary сериализация выполнена успешно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка Binary сериализации: {ex.Message}");
            }

            try
            {
                soapSerialization.Serialize(exams, soapFilePath);
                Console.WriteLine("SOAP сериализация выполнена успешно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка SOAP сериализации: {ex.Message}");
            }

            try
            {
                jsonSerialization.Serialize(exams, jsonFilePath);
                Console.WriteLine("JSON сериализация выполнена успешно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка JSON сериализации: {ex.Message}");
            }

            try
            {
                xmlSerialization.Serialize(exams, xmlFilePath);
                Console.WriteLine("XML сериализация выполнена успешно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка XML сериализации: {ex.Message}");
            }

            try
            {
                var deserializedBinaryArray = binarySerialization.Deserealize<Exam[]>(binaryFilePath);
                Console.WriteLine("Binary десериализация выполнена успешно.");
                foreach (var exam in deserializedBinaryArray)
                {
                    Console.WriteLine(exam);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка Binary десериализации: {ex.Message}");
            }

            try
            {
                var deserializedSoapArray = soapSerialization.Deserealize<Exam[]>(soapFilePath);
                Console.WriteLine("SOAP десериализация выполнена успешно.");
                foreach (var exam in deserializedSoapArray)
                {
                    Console.WriteLine(exam);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка SOAP десериализации: {ex.Message}");
            }

            try
            {
                var deserializedJsonArray = jsonSerialization.Deserealize<Exam[]>(jsonFilePath);
                Console.WriteLine("JSON десериализация выполнена успешно.");
                foreach (var exam in deserializedJsonArray)
                {
                    Console.WriteLine(exam);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка JSON десериализации: {ex.Message}");
            }

            try
            {
                var deserializedXmlArray = xmlSerialization.Deserealize<Exam[]>(xmlFilePath);
                Console.WriteLine("XML десериализация выполнена успешно.");
                foreach (var exam in deserializedXmlArray)
                {
                    Console.WriteLine(exam);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка XML десериализации: {ex.Message}");
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilePath);

            var students = doc.SelectNodes("/ArrayOfExam/Exam/studentName");
            Console.WriteLine("Студенты: ");
            foreach(XmlNode student in students)
            {
                Console.WriteLine(student.InnerText);
            }

            var subjects = doc.SelectNodes("/ArrayOfExam/Exam[numberOfQuestions > 2]/nameSubject");
            Console.WriteLine("Предметы с количеством вопросов больше 2: ");
            foreach(XmlNode subject in subjects)
            {
                Console.WriteLine(subject.InnerText);
            }

            XDocument document = new XDocument(
                new XElement("Exams",
                    new XElement("Exam",
                    new XElement("StudentName", "Денис"),
                    new XElement("Subject", "Теория вероятности"),
                    new XElement("NumberOfQuestions", 2)
                    ),
                new XElement("Exam",
                    new XElement("StudentName", "Даниил"),
                    new XElement("Subject", "КПО"),
                    new XElement("NumberOfQuestions", 3)
                    ),
                new XElement("Exam",
                    new XElement("StudentName", "Кирилл"),
                    new XElement("Subject", "АиСД"),
                    new XElement("NumberOfQuestions", 4)
                    ),
                new XElement("Exam",
                    new XElement("StudentName", "Влад"),
                    new XElement("Subject", "КСиС"),
                    new XElement("NumberOfQuestions", 2)
                    )
                )
            );

            string filePath = "Exams.xml";
            document.Save(filePath);
            Console.WriteLine($"XML-документ сохранен: {filePath}");

            Console.WriteLine("Список всех студентов: ");
            var countStudents = document.Descendants("Exam").Select(x => x.Element("StudentName").Value);
            foreach (var student in countStudents)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine("Список предметов на экзамене: ");
            var countSubjects = document.Descendants("Exam").Select(s => s.Element("Subject").Value);
            foreach (var subject in countSubjects)
            {
                Console.WriteLine(subject);
            }
        }
    }
}
