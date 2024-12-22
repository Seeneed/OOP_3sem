using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel.Design;

namespace laba14
{
    class Program
    {
        static Mutex consoleMutex = new Mutex();
        static ManualResetEvent pauseEvent = new ManualResetEvent(true);
        static object lockObject = new object();
        static bool evenTurn = true;
        static bool firstPhaseComplete = false;

        static void Main()
        {
            DisplayRunningProcesses();
            WorkWithDomains();
            PrimeNumbersWithMutex();
            EvenOddNumbersWithMutex();
            TaskWithTimer();
        }
        static void DisplayRunningProcesses()
        {
            var processes = Process.GetProcesses();
            using(StreamWriter writer = new StreamWriter("task1.txt"))
            {
                foreach(var process in processes)
                {
                    try
                    {
                        writer.WriteLine($"ID: {process.Id}, Имя: {process.ProcessName}");
                        writer.WriteLine($"Приорите: {process.BasePriority}");
                        writer.WriteLine($"Время начала: {process.StartTime}");
                        writer.WriteLine($"Время использования процесса: {process.TotalProcessorTime}");
                        writer.WriteLine($"Состояние: {process.Responding}");
                        writer.WriteLine();
                    }
                    catch( Exception ex )
                    {
                        writer.WriteLine($"ID: {process.Id}, Имя: {process.ProcessName} - Ошибка: {ex.Message}");
                        writer.WriteLine();
                    }
                }
            }
        }
        static void WorkWithDomains()
        {
            AppDomain domain = AppDomain.CurrentDomain;
            using (StreamWriter writer = new StreamWriter("task2.txt"))
            {
                writer.WriteLine($"Имя домена: {domain.FriendlyName}");
                writer.WriteLine($"Базовый директорий: {domain.BaseDirectory}");
                writer.WriteLine("Выгруженные сборки: ");
                foreach(var assembly in domain.GetAssemblies())
                {
                    writer.WriteLine($"{assembly.FullName}");
                }
            }
        }
        static void PrimeNumbersWithMutex()
        {
            Console.Write("Введите число n: ");
            int n = int.Parse( Console.ReadLine() );
            Thread thread = new Thread(() =>
            {
                using (StreamWriter writer = new StreamWriter("task3.txt"))
                {
                    for (int i = 2; i <= n; i++)
                    {
                        pauseEvent.WaitOne();
                        if (isPrime(i))
                        {
                            consoleMutex.WaitOne();
                            try
                            {
                                Console.WriteLine($"Значение: {i}");
                                writer.WriteLine(i);
                                Console.WriteLine($"Поток: {Thread.CurrentThread.Name}");
                                Console.WriteLine($"Идентификатор: {Thread.CurrentThread.ManagedThreadId}");
                                Console.WriteLine($"Приоритет: {Thread.CurrentThread.Priority}");
                                Console.WriteLine($"Состояние: {Thread.CurrentThread.ThreadState}");
                            }
                            finally
                            {
                                consoleMutex.ReleaseMutex();
                            }
                        }
                        Thread.Sleep(50);
                    }
                }
            });
            thread.Start();
            Thread.Sleep(1000);
            pauseEvent.Reset();
            Console.WriteLine("Поток приостановлен");
            Thread.Sleep(1000);
            pauseEvent.Set();
            Console.WriteLine("Поток возобновлен");
            thread.Join();
        }
        static bool isPrime(int n)
        {
            if(n < 2)
                return false;
            for(int i = 2; i <= Math.Sqrt(n); i++)
            {
                if (n % i == 0)
                    return false;
            }
            return true;
        }
        static void EvenOddNumbersWithMutex()
        {
            StreamWriter writer = new StreamWriter("task4.txt");

            Console.Write("Введите число n: ");
            int n = int.Parse(Console.ReadLine());

            Thread thread = new Thread(() =>
            {
                for (int i = 2; i < n; i++)
                {
                    lock (lockObject)
                    {
                        while (!evenTurn && !firstPhaseComplete)
                        {
                            Monitor.Wait(lockObject);
                        }
                        Console.WriteLine($"Значение: {i}");
                        writer.WriteLine($"Значение: {i}");
                        evenTurn = false;
                        Monitor.Pulse(lockObject);
                    }
                    Thread.Sleep(100);
                }
                lock (lockObject)
                {
                    firstPhaseComplete = true;
                    Monitor.PulseAll(lockObject);
                }
            });

            Thread oddThread = new Thread(() =>
            {
                for (int i = 1; i < n; i += 2)
                {
                    lock (lockObject)
                    {
                        while (evenTurn && !firstPhaseComplete)
                        {
                            Monitor.Wait(lockObject);
                        }
                        Console.WriteLine($"Нечетное: {i}");
                        writer.WriteLine($"Нечетное: {i}");
                        evenTurn = true;
                        Monitor.Pulse(lockObject);
                    }
                    Thread.Sleep(50);
                }
            });

            thread.Priority = ThreadPriority.Highest;
            oddThread.Priority = ThreadPriority.Lowest;

            thread.Start();
            oddThread.Start();

            thread.Join();
            oddThread.Join();

            writer.Close();
        }
        static void TaskWithTimer()
        {
            Timer timer = new Timer(PrintCurrentTime, null, 0, 1000);
            Console.WriteLine("Нажмите Enter для остановки таймера");
            Console.ReadLine();
            timer.Dispose();
        }
        static void PrintCurrentTime(object state)
        {
            consoleMutex.WaitOne();
            try
            {
                Console.WriteLine($"Текущее время: {DateTime.Now}");
            }
            finally
            {
                consoleMutex.ReleaseMutex();
            }
        }
    }
}