using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Zoo.Common
{
    public class AsyncCrudService<T> : ICrudServiceAsync<T> where T : class
    {
        private readonly ConcurrentDictionary<Guid, T> _items = new();
        private readonly string _filePath;
        private readonly SemaphoreSlim _semaphore = new(1, 1);

        public AsyncCrudService(string filePath)
        {
            _filePath = filePath;
        }

        private Guid GetId(T element)
        {
            var prop = typeof(T).GetProperty("Id");
            return prop != null ? (Guid)prop.GetValue(element) : Guid.Empty;
        }

        public async Task<bool> CreateAsync(T element)
        {
            var id = GetId(element);
            if (id == Guid.Empty) return false;

            if (!_items.TryAdd(id, element))
                return false;

            return await Task.FromResult(true);
        }

        public async Task<T?> ReadAsync(Guid id)
        {
            _items.TryGetValue(id, out var element);
            return await Task.FromResult(element);
        }

        public async Task<IEnumerable<T>> ReadAllAsync()
        {
            return await Task.FromResult(_items.Values);
        }

        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            var items = _items.Values.Skip((page - 1) * amount).Take(amount);
            return await Task.FromResult(items);
        }

        public async Task<bool> UpdateAsync(T element)
        {
            var id = GetId(element);
            if (!_items.ContainsKey(id))
                return await Task.FromResult(false);

            _items[id] = element;
            return await Task.FromResult(true);
        }

        public async Task<bool> RemoveAsync(T element)
        {
            var id = GetId(element);
            return await Task.FromResult(_items.TryRemove(id, out _));
        }

        public async Task<bool> SaveAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(_items.Values, options);
                await File.WriteAllTextAsync(_filePath, json);
                return true;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public IEnumerator<T> GetEnumerator() => _items.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
