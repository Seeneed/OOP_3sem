using System;

namespace laba8
{
    public delegate void Up();
    public delegate void Activate(int voltage);
    public class Boss
    {
        public event Up? Upgrade;
        public event Activate? TurnOn;
        public void StartDevice()
        {
            Console.WriteLine("Оборудование включено...");
            Upgrade?.Invoke();
        }
        public void ActivateDevice(int voltage)
        {
            Console.WriteLine($"Оборудование включено на напряжении {voltage}В...");
            TurnOn?.Invoke(voltage);
        }
    }
    public abstract class Device
    {
        public string nameDevice { get; set; }
        public int healthDevice { get; set; } = 100;
        public bool isWork { get; set; } = true;
        public int upgradeLevel { get; set; } = 0;
        public Device(string name)
        {
            this.nameDevice = name;
        }
        public abstract void OnUpgrade();
        public abstract void OnTurn(int voltage);
    }
    public class Phone : Device
    {
        public Phone(string name) : base(name) { }
        public override void OnUpgrade()
        {
            if (isWork)
            {
                upgradeLevel++;
                Console.WriteLine($"Оборудование {nameDevice} улучшено до уровня {upgradeLevel}. Здоровье: {healthDevice}%.");
            }
            else
            {
                Console.WriteLine($"Оборудование {nameDevice} сломано. Здоровье: {healthDevice}%.");
            }
        }
        public override void OnTurn(int voltage)
        {
            if (isWork)
            {
                if (voltage > 220)
                {
                    Console.WriteLine($"Перенапряжение! Оборудование {nameDevice} сломано!");
                    healthDevice = 0;
                    isWork = false;
                }
                else if (voltage < 50)
                {
                    Console.WriteLine($"Недостаточно напряжения для включения оборудования {nameDevice}.");
                }
                else
                {
                    Console.WriteLine($"Оборудование {nameDevice} включено при напряжении {voltage}В. Здоровье: {healthDevice}%.");
                }
            }
            else
            {
                Console.WriteLine($"Оборудование {nameDevice} сломано. Здоровье: {healthDevice}%.");
            }
        }
    }
    public class Laptop : Device
    {
        public Laptop(string name) : base(name) { }
        public override void OnUpgrade()
        {
            if (isWork)
            {
                upgradeLevel++;
                Console.WriteLine($"Оборудование {nameDevice} улучшено до уровня {upgradeLevel}. Здоровье: {healthDevice}%.");
            }
            else
            {
                Console.WriteLine($"Оборудование {nameDevice} сломано. Здоровье: {healthDevice}%.");
            }
        }
        public override void OnTurn(int voltage)
        {
            if (isWork)
            {
                if (voltage > 240)
                {
                    Console.WriteLine($"Перенапряжение! Оборудование {nameDevice} сломано!");
                    healthDevice = 0;
                    isWork = false;
                }
                else if (voltage < 100)
                {
                    Console.WriteLine($"Недостаточно напряжения для включения оборудования {nameDevice}.");
                }
                else
                {
                    Console.WriteLine($"Оборудование {nameDevice} включено при напряжении {voltage}В. Здоровье: {healthDevice}%.");
                }
            }
            else
            {
                Console.WriteLine($"Оборудование {nameDevice} сломано. Здоровье: {healthDevice}%.");
            }
        }
    }
    public class ChangeStr
    {
        public Action<string> str1;
        public Action<string, char> str2;
        public ChangeStr()
        {
            str1 += delPunct;
            str1 += delSpaces;
            str2 = addSymbols;
            str1 += changeUpper;
        }
        public void delPunct(string str)
        {
            str = str.Replace(":", "")
                .Replace(",", "")
                .Replace(".", "")
                .Replace(";", "");
            Console.WriteLine("Удаление знаков препинания: " + str);
        }
        public void delSpaces(string str) 
        {
            str = str.Replace(" ", "");
            Console.WriteLine("Строка без пробелов: " + str);
        }
        public void addSymbols(string str, char symbol)
        {
            str += symbol;
            Console.WriteLine("Строка с добавленным символом: " + str);
        }
        public void changeUpper(string str)
        {
            str = str.ToUpper();
            Console.WriteLine("Строка в верхнем регистре: " + str);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Boss boss = new Boss();
            Phone phone = new Phone("Samsung Galaxy S24 Ultra");
            Laptop laptop = new Laptop("Asus Vivobook");
            boss.Upgrade += phone.OnUpgrade;
            boss.Upgrade += laptop.OnUpgrade;
            boss.TurnOn += phone.OnTurn;
            boss.TurnOn += laptop.OnTurn;
            boss.StartDevice();
            boss.ActivateDevice(100);
            boss.ActivateDevice(300);
            boss.StartDevice();
            Console.WriteLine();
            ChangeStr changeStr = new ChangeStr();
            Console.WriteLine("Введите строку: ");
            string inputString = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введите символ: ");
            char symbol = Convert.ToChar(Console.ReadLine());
            changeStr.changeUpper(inputString);
            changeStr.addSymbols(inputString, symbol);
        }
    }
}
