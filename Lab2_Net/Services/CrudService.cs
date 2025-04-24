using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Lab2App.Models;

namespace Lab2App.Services
{
    public class CrudService<T> where T : class
    {
        private readonly string _filePath;
        private Dictionary<Guid, T> _data;

        public CrudService(string filePath)
        {
            _filePath = filePath;
            LoadData();
        }

        private void LoadData()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);

                // Десеріалізація JSON в словник
                try
                {
                    _data = JsonSerializer.Deserialize<Dictionary<Guid, T>>(json);
                    Console.WriteLine("Дані завантажено з файлу.");
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Помилка десеріалізації: {ex.Message}");
                    _data = new Dictionary<Guid, T>();
                }
            }
            else
            {
                _data = new Dictionary<Guid, T>();
            }
        }

        public void Save()
        {
            string json = JsonSerializer.Serialize(_data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
            Console.WriteLine("Дані збережено в файл.");
        }

        public void Add(T item)
        {
            if (item is Bus bus)
            {
                _data.Add(bus.Id, item);
            }
        }
    }
}
