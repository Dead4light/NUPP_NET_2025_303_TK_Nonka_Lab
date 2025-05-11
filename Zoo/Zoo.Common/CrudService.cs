using System;
using System.Collections.Generic;
using System.Linq;

namespace Zoo.Common
{
    public class CrudService<T> : ICrudService<T> where T : class
    {
        private readonly List<T> _items = new List<T>();

        public void Create(T element)
        {
            _items.Add(element);
        }

        public T Read(Guid id)
        {
            return _items.FirstOrDefault(item => GetId(item) == id);
        }

        public IEnumerable<T> ReadAll()
        {
            return _items;
        }

        public void Update(T element)
        {
            var id = GetId(element);
            var index = _items.FindIndex(item => GetId(item) == id);
            if (index != -1)
            {
                _items[index] = element;
            }
        }

        public void Remove(T element)
        {
            _items.Remove(element);
        }

        private Guid GetId(T element)
        {
            var prop = typeof(T).GetProperty("Id");
            return prop != null ? (Guid)prop.GetValue(element) : Guid.Empty;
        }
    }
}
