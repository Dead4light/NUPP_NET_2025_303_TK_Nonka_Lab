using System;
using Lab2App.Models;
using Lab2App.Services;

namespace Lab2App
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"D:\net\Lab2_Net\\buses_test.json";

            var crudService = new CrudService<Bus>(filePath);

            // Створення нового автобуса
            var newBus = new Bus { Id = Guid.NewGuid(), Model = "Mercedes-Benz", Capacity = 50 };

            // Додавання нового автобуса
            crudService.Add(newBus);

            // Збереження даних
            crudService.Save();

            Console.WriteLine("Операція завершена.");
        }
    }
}
