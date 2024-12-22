using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;

namespace laba2
{
    public partial class House
    {
        public readonly int id;
        private int numberFlat;
        private float areaFlat;
        private int floorFlat;
        private int roomsFlat;
        private string street;
        private string typeBuilding;
        private string date;
        public static int _id;
        const string country = "Беларусь";
        public int Number
        {
            get { return numberFlat; }
            set {
                if(value < 0)
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
        static House()
        {
            Console.WriteLine("Создан первый статический конструктор House");
        }
        //public House() : this(0, 0, 0, 0, "Нет информации", "Нет информации", "Нет информации")
        //{ }
        public House(int _number) : this(_number, 0, 0, 0, "нет информации", "нет информации", "нет информации")
        { }
        public House(int _number, float _square) : this(_number, _square, 0, 0, "нет информации", "нет информации", "нет информации")
        { }
        public House(int _number, float _square, int _floor) : this(_number, _square, _floor, 0, "нет информации", "нет информации", "нет информации")
        { }
        public House(int _number, float _square, int _floor, int _rooms) : this(_number, _square, _floor, _rooms, "нет информации", "нет информации", "нет информации")
        { }
        public House(int _number, float _square, int _floor, int _rooms, string _street) : this(_number, _square, _floor, _rooms, _street, "нет информации", "нет информации")
        { }                                                    
        public House(int _number, float _square, int _floor, int _rooms, string _street, string _type) : this(_number, _square, _floor, _rooms, _street, _type, "нет информации")
        { }                                                   
        public House(int _number, float _square, int _floor, int _rooms, string _street, string _type, string _date)
        {
            _id++;
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
        public void OldBuildings(string year1, string year2, out string result)
        {
            result = Convert.ToString(Convert.ToInt32(year1) - Convert.ToInt32(year2));
        }
        public void changeCount(ref int value)
        {
            value = id;
        }
        private House() { }
        public static string name = "Денис";
        public void staticInfo()
        {
            Console.WriteLine(name);
            Console.WriteLine(_id);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            House[] house = new House[5];
            house[0] = new House(28, 50.2f, 3, 2, "Советская", "Панельный дом", "2015");
            house[1] = new House(57, 75.2f, 3, 3, "Советская", "Панельный дом", "2014");
            house[2] = new House(215, 43.4f, 1, 3, "Ленина", "Блочный дом", "2010");
            house[3] = new House(64, 54.4f, 13, 3, "Октябрьская", "Панельный дом", "2020");
            house[4] = new House(128, 45.5f, 10, 2, "Свердлова", "Кирпичный дом", "2018");

            for (int i = 0; i < 5; i++)
            {
                if (house[i].Rooms == 2)
                {
                    Console.WriteLine($"Квартира №{i + 1}:");
                    house[i].GetInfo();
                }
                if (house[i].Rooms == 3 && (house[i].Floor > 0 && house[i].Floor < 4))
                {
                    Console.WriteLine($"Квартира №{i + 1}:");
                    house[i].GetInfo();
                }
            }
            string result = null;
            house[0].OldBuildings("2024", house[0].Date, out result);
            Console.WriteLine($"Разница между годом постройки и текущим годом 1-го дома: {result} лет");

            int x = 0;
            house[1].changeCount(ref x);
            Console.WriteLine($"Текущий ID после изменения: {x}");

            Console.WriteLine($"Имя: {House.name}");
            Console.WriteLine($"Хэш-код объекта house[2]: {house[2].GetHashCode()}");
            Console.WriteLine($"Метод ToString для объекта: {house[3].ToString()}");

            bool equal = house[3].Equals(house[4]);
            Console.WriteLine($"Метод Equals: {equal}");

            var house5 = new { N = 1, S = 40.2f, F = 1, R = 2, Str = "Партизанская", T = "Панельный дом", D = "2012" };
            Console.WriteLine($"Анонимный объект: Номер квартиры - {house5.N}, Площадь - {house5.S} кв.м., Этаж - {house5.F}, Количество комнат - {house5.R}, Улица - {house5.Str}, Тип - {house5.T}, Год постройки - {house5.D}");
        }
    }
    partial class House
    {
        public int Value
        {
            get { return this.roomsFlat; }
        }
        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            House h = obj as House;
            if(h as House == null)
            {
                return false;
            }
            return h.id == this.id && h.numberFlat == this.numberFlat && h.floorFlat == this.floorFlat;
        }
        public override int GetHashCode()
        {
            return this.id;
        }
        public override string ToString()
        {
            return "Id: " + this.id + " номер квартиры: " + this.numberFlat;
        }
    }
}