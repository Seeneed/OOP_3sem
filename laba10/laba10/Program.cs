using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Security.Cryptography;

namespace laba10
{
    interface IHouse
    {
        public void Add(House item);
        public void Remove(House item);
        public void Clear();
        public void GetInfo();
        public int Count();
    }
    public class House : IHouse
    {
        List <House> houses = new List<House>();
        public int id;
        public int numberFlat;
        public float areaFlat;
        public int floorFlat;
        public int roomsFlat;
        public string street;
        public string typeBuilding;
        public string date;
        public int Number
        {
            get { return numberFlat; }
            set
            {
                if (value < 0)
                {
                    numberFlat = 0;
                }
                else
                {
                    numberFlat = value;
                }
            }
        }
        public float Area
        {
            get { return areaFlat; }
            set { areaFlat = value; }
        }
        public int Floor
        {
            get { return floorFlat; }
            set { floorFlat = value; }
        }
        public int Rooms
        {
            get { return roomsFlat; }
            set { roomsFlat = value; }
        }
        public string Street
        {
            get { return street; }
            set { street = value; }
        }
        public string TypeBuilding
        {
            get { return typeBuilding; }
            set { typeBuilding = value; }
        }
        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        public House(int _id, int _number, float _square, int _floor, int _rooms, string _street, string _type, string _date)
        {
            this.id = _id;
            this.numberFlat = _number;
            this.areaFlat = _square;
            if (_floor < 1)
                this.floorFlat = 0;
            else
                this.floorFlat = _floor;
            this.roomsFlat = _rooms;
            if (_street.Length < 2)
                this.street = "Нет информации";
            else
                this.street = _street;
            this.typeBuilding = _type;
            this.date = _date;
        }
        public void Add(House item)
        {
            if(!houses.Contains(item))
            {
                houses.Add(item);
            }
            Console.WriteLine("Данный объект уже есть в списке");
        }
        public void GetInfo()
        {
            Console.WriteLine("Информация о квартире:");
            Console.WriteLine($"ID: {this.id}");
            Console.WriteLine($"Номер квартиры: {this.numberFlat}");
            Console.WriteLine($"Площадь: {this.areaFlat} кв.м.");
            Console.WriteLine($"Этаж: {this.floorFlat}");
            Console.WriteLine($"Количество комнат: {this.roomsFlat}");
            Console.WriteLine($"Улица: {this.street}");
            Console.WriteLine($"Тип здания: {this.typeBuilding}");
            Console.WriteLine($"Год постройки: {this.date}");
        }
        public void Remove(House item)
        {
            if(houses.Contains(item))
            {
                houses.Remove(item);
            }
            Console.WriteLine("Данного объекта нет в списке");
        }
        public void Clear()
        {
            houses.Clear();
        }
        public int Count()
        {
            return houses.Count;
        }
    }
    public class Person
    {
        public string Name { get; set; }
        public int flatId { get; set; }
        public int age { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] monthes = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            var selectedNumbersOne = monthes.Where(m => m.Length == 4);
            foreach (string month in selectedNumbersOne)
            {
                Console.WriteLine(month);
            }
            Console.WriteLine();
            var selectedSummerMonthes = monthes.Where(m => m == "June" || m == "July" || m == "August");
            foreach (string month in selectedSummerMonthes)
            {
                Console.WriteLine(month);
            }
            Console.WriteLine();
            var selectedWintedMonthes = monthes.Where(m => m == "January" || m == "February" || m == "December");
            foreach (string month in selectedWintedMonthes)
            {
                Console.WriteLine(month);
            }
            Console.WriteLine();
            var selectedOrderByOnAlphabet = monthes.OrderBy(m => m);
            foreach (string month in selectedOrderByOnAlphabet)
            {
                Console.WriteLine(month);
            }
            Console.WriteLine();
            var selectedMonthWithLetter = monthes.Where(m => m.Contains("u"));
            foreach (string month in selectedMonthWithLetter)
            {
                Console.WriteLine(month);
            }
            Console.WriteLine();
            var selectedMonthesWithCertainLength = monthes.Where(m => m.Length < 4);
            foreach (string month in selectedMonthesWithCertainLength)
            {
                Console.WriteLine(month);
            }
            Console.WriteLine();

            List<House> houses = new List<House>();
            House house1 = new House(1, 28, 50.2f, 3, 2, "Советская", "Панельный дом", "2015");
            House house2 = new House(2, 57, 75.2f, 3, 3, "Советская", "Панельный дом", "2014");
            House house3 = new House(3, 215, 43.4f, 1, 3, "Ленина", "Блочный дом", "2010");
            House house4 = new House(4, 64, 54.4f, 13, 3, "Октябрьская", "Панельный дом", "2020");
            House house5 = new House(5, 128, 45.5f, 10, 2, "Свердлова", "Кирпичный дом", "2018");
            House house6 = new House(6, 3, 23.5f, 1, 1, "Кирова", "Деревянный дом", "2010");
            House house7 = new House(7, 13, 34.6f, 4, 2, "Мира", "Панельный дом", "2016");
            House house8 = new House(8, 65, 41.5f, 8, 4, "Партизанская", "Кирпичный дом", "2019");
            House house9 = new House(9, 7, 25.5f, 2, 1, "Ленина", "Деревянный дом", "2010");
            House house10 = new House(10, 15, 38.6f, 6, 2, "Набережная", "Блочный дом", "2014");
            houses.Add(house1);
            houses.Add(house2);
            houses.Add(house3);
            houses.Add(house4);
            houses.Add(house5);
            houses.Add(house6);
            houses.Add(house7);
            houses.Add(house8);
            houses.Add(house9);
            houses.Add(house10);
            var selectedFlatWithCertainRoom = houses.Where(h => h.roomsFlat == 2);
            foreach (var house in selectedFlatWithCertainRoom)
            {
                Console.WriteLine($"ID: {house.id}, Номер квартиры: {house.numberFlat}, Количество комнат: {house.roomsFlat}");
            }
            Console.WriteLine();
            var selectedFlats = houses.Where(h => h.street == "Советская").Where(h => h.typeBuilding == "Панельный дом");
            foreach (var house in selectedFlats)
            {
                Console.WriteLine($"ID: {house.id}, Номер квартиры: {house.numberFlat}, Улица: {house.street}, Тип здания: {house.typeBuilding}");
            }
            Console.WriteLine();
            int selectedFlatsOnTheCertainStreet = houses.Count(h => h.street == "Ленина");
            Console.WriteLine($"Количество квартир, которые расположены на улице Ленина: {selectedFlatsOnTheCertainStreet}");
            Console.WriteLine();
            var selectedFlatsOnTheFloor = houses.Where(h => h.roomsFlat == 3 && (h.floorFlat > 1 && h.floorFlat < 5));
            foreach (var house in selectedFlatsOnTheFloor)
            {
                Console.WriteLine($"ID: {house.id}, Номер квартиры: {house.numberFlat}, Этаж: {house.floorFlat}, Количество комнат: {house.roomsFlat}");
            }
            Console.WriteLine();
            var selectedUniversalRequest = houses.Where(h => h.roomsFlat == 3 && h.floorFlat >= 3)
                .OrderByDescending(h => h.areaFlat)
                .Select(h => new { h.street, h.areaFlat, h.floorFlat })
                .GroupBy(h => h.street)
                .Select(g => new
                {
                    Street = g.Key,
                    MaxArea = g.Max(h => h.areaFlat),
                    MaxFloor = g.Max(h => h.floorFlat)
                })
                .Where(g => g.MaxArea > 50)
                .ToList();
            foreach (var house in selectedUniversalRequest)
            {
                Console.WriteLine($"Улица: {house.Street}, Максимальная площадь: {house.MaxArea}, Максимальный этаж: {house.MaxFloor}");
            }
            Console.WriteLine();

            List<Person> persons = new List<Person>
            {
                new Person {Name = "Александр", flatId = 1, age = 20},
                new Person {Name = "Сергей", flatId = 2, age = 22},
                new Person {Name = "Денис", flatId = 3, age = 21},
                new Person {Name = "Даниил", flatId = 4, age = 19},
                new Person {Name = "Кирилл", flatId = 5, age = 18},
                new Person {Name = "Владислав", flatId = 6, age = 22},
                new Person {Name = "Алексей", flatId = 7, age = 24},
                new Person {Name = "Семён", flatId = 8, age = 23},
                new Person {Name = "Тимофей", flatId = 9, age = 24},
                new Person {Name = "Марат", flatId = 10, age = 25},
            };
            var liver = from h in houses
                        join p in persons on h.id equals p.flatId
                        select new { Name = p.Name, PersonId = p.flatId, FlatId = h.id, h.street};
            foreach ( var l in liver )
            {
                Console.WriteLine($"Житель квартиры: {l.Name}, {l.PersonId} - ID квартиры: {l.FlatId}, Улица на которой живет житель: {l.street}");
            }
        }
    }
}