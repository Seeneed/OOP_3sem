using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        TPLPrimeSearch();
        CancelTokenDemo();
        TasksWithResults();
        ContinuationTasks();
        ParallelLoops();
        ParallelInvoke();
        BlockingCollectionDemo();
        AsyncAwaitDemo().Wait();
    }
    static void TPLPrimeSearch()
    {
        Console.WriteLine("----- Поиск простых чисел -----");
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Task<List<int>> primeTask = Task.Run(() => FindPrimaryNumbers(1, 100));
        Console.WriteLine($"ID задачи: {primeTask.Id}");
        Console.WriteLine($"Статус задачи до завершения: {primeTask.Status}");
        primeTask.Wait();
        stopwatch.Stop();
        Console.WriteLine($"Статус задачи после выполнения: {primeTask.Status}");
        Console.WriteLine($"Количество найденных простых чисел в заданном диапазоне: {primeTask.Result.Count}");
        Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
    }
    static List<int> FindPrimaryNumbers(int start, int end)
    {
        bool[] isPrime = new bool[end + 1];
        for(int i = 2; i <= end; i++)
        {
            isPrime[i] = true;
        }
        for(int j = 2; j*j <=end; j++)
        {
            if (isPrime[j])
            {
                for(int k = j*j; k <= end; k += j)
                {
                    isPrime[k] = false;
                }
            }
        }
        List<int> primes = new List<int>();
        for(int i = start; i <= end; i++)
        {
            if (isPrime[i])
            {
                primes.Add(i);
            }
        }
        return primes;
    }
    static void CancelTokenDemo()
    {
        Console.WriteLine("----- Отмена задачи с помощью CancellationToken -----");
        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;
        Task<List<int>> primeTask = Task.Run(() => FindPrimeNumbersWithCancellation(1, 100, token), token);
        cts.CancelAfter(10);
        try
        {
            primeTask.Wait();
            Console.WriteLine($"Количество найденных простых чисел в заданном диапазоне: {primeTask.Result.Count}");
        }
        catch(AggregateException ex)
        {
            if(ex.InnerException is TaskCanceledException)
            {
                Console.WriteLine("Задача была прервана досрочно");
            }
            else
            {
                Console.WriteLine("Произошла ошибка: " + ex.InnerException.Message);
            }
        }
    }
    static List<int> FindPrimeNumbersWithCancellation(int start, int end, CancellationToken token)
    {
        bool[] isPrime = new bool[end + 1];
        for(int i = 2; i<=end; i++)
        {
            isPrime[i] = true;
        }
        for(int j = 2; j*j <= end; j++)
        {
            if (isPrime[j])
            {
                for(int k = j*j; k <= end; k += j)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Задача отменена!");
                        token.ThrowIfCancellationRequested();
                    }
                    isPrime[k] = false;
                }
            }
        }
        List<int> result = new List<int>();
        for(int i = start; i <= end; i++)
        {
            if (isPrime[i])
            {
                result.Add(i);
            }
        }
        return result;
    }
    static void TasksWithResults()
    {
        Console.WriteLine("----- Задача с возвратом результата -----");
        Task<int> task1 = Task.Run(() => CalculateFirst(10));
        Task<int> task2 = Task.Run(() => CalculateSecond(20));
        Task<int> task3 = Task.Run(() => CalculateThird(30));
        Task<int> finalTask = Task.WhenAll(task1, task2, task3).ContinueWith(t => t.Result.Sum());
        Console.WriteLine($"Итоговый результат: {finalTask.Result}");
    }
    static int CalculateFirst(int x) => x * 2;
    static int CalculateSecond(int x) => x * 3;
    static int CalculateThird(int x) => x * 4;
    static void ContinuationTasks()
    {
        Console.WriteLine("----- Задача продолжения -----");
        Task<int> firstTask = Task.Run(() => 52);
        Task continuationTask = firstTask.ContinueWith(t => Console.WriteLine($"Результат предыдущей задачи: {t.Result}"));

        Task<int> secondTask = Task.Run(() => 100);
        int result = secondTask.GetAwaiter().GetResult();
        Console.WriteLine($"Результат задачи (GetAwaiter): {result}");
    }
    static void ParallelLoops()
    {
        Console.WriteLine("----- Параллельные циклы -----");
        Stopwatch sequentialWatch = Stopwatch.StartNew();
        for(int i = 0; i < 500; i++)
        {
            Math.Sin(i);
        }
        sequentialWatch.Stop();
        Stopwatch parallelWatch = Stopwatch.StartNew();
        Parallel.For(0, 500, i =>
        {
            Math.Sin(i);
        });
        parallelWatch.Stop();
        Console.WriteLine($"Последовательный цикл: {sequentialWatch.ElapsedMilliseconds} мс");
        Console.WriteLine($"Параллельный цикл: {parallelWatch.ElapsedMilliseconds} мс");
    }
    static void ParallelInvoke()
    {
        Console.WriteLine("----- ParallelInvoke -----");
        Parallel.Invoke(
            () => Console.WriteLine("Операция 1"),
            () => Console.WriteLine("Операция 2"),
            () => Console.WriteLine("Операция 3")
        );
    }
    static void BlockingCollectionDemo()
    {
        Console.WriteLine("----- BlockingCollection -----");
        BlockingCollection<string> collection = new BlockingCollection<string>(5);
        var goods = new Task[5];
        var persons = new Task[10];
        for(int i = 0; i < 5; i++)
        {
            int goodsId = i;
            goods[i] = Task.Run(() =>
            {
                string product = $"Товар от поставщика {goodsId}";
                collection.Add(product);
                Console.WriteLine($"Добавлен {product}");
            });
        }
        for(int i = 0; i < 10;i++)
        {
            int personId = i;
            persons[i] = Task.Run(() =>
            {
                try
                {
                    string product = collection.Take();
                    Console.WriteLine($"Покупатель {personId} купил {product}");
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine($"Покупатель {personId} ушел ни с чем");
                }
            });
        }
        Task.WaitAll(goods);
        collection.CompleteAdding();
        Task.WaitAll(persons);
    }
    static async Task AsyncAwaitDemo()
    {
        Console.WriteLine("----- Async/Await -----");
        int result = await PerformAsyncOperation();
        Console.WriteLine($"Результат асинхронной операции: {result}");
    }
    static async Task<int> PerformAsyncOperation()
    {
        await Task.Delay(1000);
        return 52;
    }
}