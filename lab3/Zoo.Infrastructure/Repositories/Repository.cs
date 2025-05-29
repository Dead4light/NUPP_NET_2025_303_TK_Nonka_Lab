using Microsoft.EntityFrameworkCore;

namespace Zoo.Infrastructure.Repositories;

/// <summary>
/// Реалізація репозиторію для будь-якої сутності
/// </summary>
public class Repository<T> : IRepository<T> where T : class
{
    private readonly ZooContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(ZooContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>(); // Отримуємо набір сутностей типу T
    }

    public async Task<T> GetByIdAsync(Guid id)
        => await _dbSet.FindAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public async Task<IEnumerable<T>> GetAllAsync(int page, int count)
        => await _dbSet.Skip(page * count).Take(count).ToListAsync();

    public async Task<bool> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }
}
