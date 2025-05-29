using System.Collections;
using Zoo.Common;
using Zoo.Infrastructure.Repositories;

namespace Zoo.Infrastructure.Services;

public class DataCrudService<T> : ICrudServiceAsync<T> where T : class
{
    public IRepository<T> Repository { get; }

    public DataCrudService(IRepository<T> repository)
    {
        Repository = repository;
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        return ReadAllAsync().GetAwaiter().GetResult().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Task<bool> CreateAsync(T element)
    {
        return Repository.AddAsync(element);
    }

    public Task<T?> ReadAsync(Guid id)
    {
        return Repository.GetByIdAsync(id);
    }

    public Task<IEnumerable<T>> ReadAllAsync()
    {
        return Repository.GetAllAsync();
    }

    public Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
    {
        return Repository.GetAllAsync(page, amount);
    }

    public Task<bool> UpdateAsync(T element)
    {
        return Repository.UpdateAsync(element);
    }

    public Task<bool> RemoveAsync(T element)
    {
        return Repository.DeleteAsync(element);
    }
}