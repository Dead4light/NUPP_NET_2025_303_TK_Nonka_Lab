using Zoo.Common;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        // 1. Створення асинхронного сервісу для Bus
        var service = new AsyncCrudService<Bus>("buses.json");

        // 2. Паралельне створення 1000 об'єктів
        var tasks = new Task[1000];
        for (int i = 0; i < 1000; i++)
        {
            tasks[i] = Task.Run(async () =>
            {
                var bus = Bus.CreateNew();
                await service.CreateAsync(bus);
            });
        }

        // Чекаємо завершення всіх завдань
        await Task.WhenAll(tasks);

        // 3. Порахувати мінімальні, максимальні та середні значення для Capacity і Mileage
        var buses = await service.ReadAllAsync();

        var minCapacity = buses.Min(b => b.Capacity);
        var maxCapacity = buses.Max(b => b.Capacity);
        var avgCapacity = buses.Average(b => b.Capacity);

        var minMileage = buses.Min(b => b.Mileage);
        var maxMileage = buses.Max(b => b.Mileage);
        var avgMileage = buses.Average(b => b.Mileage);

        // 4. Вивести результати
        Console.WriteLine($"Capacity: Min={minCapacity}, Max={maxCapacity}, Avg={avgCapacity}");
        Console.WriteLine($"Mileage: Min={minMileage}, Max={maxMileage}, Avg={avgMileage}");

        // 5. Зберегти дані у файл
        await service.SaveAsync();
        Console.WriteLine("Buses data saved to file.");

        // 6. Демонстрація примітивів синхронізації
        // Lock Example
        for (int i = 0; i < 5; i++)
        {
            new Thread(SyncExamples.LockExample).Start();  // Викликаємо метод без лямбда-виразу
        }

        // Semaphore Example
        SyncExamples.SemaphoreExample();
    }
}
