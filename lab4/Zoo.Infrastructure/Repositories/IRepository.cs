namespace Zoo.Infrastructure.Repositories;

/// <summary>
/// Універсальний інтерфейс для роботи з будь-якою сутністю
/// </summary>
public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);                      // Отримати одну сутність за id
    Task<IEnumerable<T>> GetAllAsync();                  // Отримати всі сутності
    Task<IEnumerable<T>> GetAllAsync(int page, int count); // Отримати всі сутності
    Task<bool> AddAsync(T entity);                             // Додати нову сутність
    Task<bool> UpdateAsync(T entity);                          // Оновити сутність
    Task<bool> DeleteAsync(T entity);                          // Видалити сутність
}
